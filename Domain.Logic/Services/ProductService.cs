using Binit.Framework;
using Binit.Framework.ExceptionHandling.Types;
using Binit.Framework.Helpers;
using Binit.Framework.Interfaces.DAL;
using Binit.Framework.Interfaces.ExceptionHandling;
using DAL;
using Domain.Entities.Model;
using Domain.Entities.Model.Views;
using Domain.Logic.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lang = Binit.Framework.Localization.LocalizationConstants.DomainLogic.Services.ProductService;

namespace Domain.Logic.Services
{
    /// <summary>
    /// Product specific services.
    /// </summary>
    public class ProductService : Service<Product>, IProductService
    {
        private readonly IService<Feature> featureService;
        private readonly IViewService<ProductFeaturesView> productFeaturesViewService;

        public ProductService(IExceptionManager exceptionManager, ILogger logger, IOperationContext operationContext,
            IUnitOfWork unitOfWork, IStringLocalizer<SharedResources> localizer, IService<Feature> featureService,
            IViewService<ProductFeaturesView> productFeaturesViewService)
            : base(exceptionManager, logger, operationContext, unitOfWork, localizer)
        {
            this.featureService = featureService;
            this.productFeaturesViewService = productFeaturesViewService;
        }

        /// <summary>
        /// Gets the complete list of products and the feature count by a Database View
        /// </summary>
        /// <returns></returns>
        public IQueryable<ProductFeaturesView> GetThroughView()
        {
            return productFeaturesViewService.GetAll();
        }

        /// <summary>
        /// Gets a product by Id including all of its relationships.
        /// </summary>
        public Product GetFull(Guid id)
        {
            var product = base.GetAll()
                .Where(p => p.Id == id)
                .Include(p => p.Category)
                .Include(p => p.Features)
                .Include(p => p.Editors).ThenInclude(e => e.Editor)
                .FirstOrDefault();

            if (product == null || product.Deleted)
                throw base.exceptionManager.Handle(new NotFoundException(this.localizer[Lang.GetFullNotFoundEx]));

            return product;
        }

        /// <summary>
        /// Asynchronously gets a product by Id including all of its relationships.
        /// </summary>
        public async Task<Product> GetFullAsync(Guid id, bool asNoTracking = false)
        {

            var product = await base.GetAll(asNoTracking)
                .Where(p => p.Id == id)
                .Include(p => p.Category)
                .Include(p => p.Features)
                .Include(p => p.Editors).ThenInclude(e => e.Editor)
                .FirstOrDefaultAsync();

            if (product == null)
                throw base.exceptionManager.Handle(new NotFoundException(this.localizer[Lang.GetFullAsyncNotFoundEx]));

            return product;

        }

        /// <summary>
        /// Product DeleteAsync override.
        /// Deletes the product and all its dependant relationships.
        /// </summary>
        public async override Task DeleteAsync(Guid id)
        {
            // Gets product with all its relationships included.
            var product = await this.GetFullAsync(id, asNoTracking: true);

            // Delete product features.
            foreach (var feature in product.Features)
                await featureService.DeleteAsync(feature);

            // Delete product editors.
            product.Editors.Clear();

            // Delete product.
            await base.DeleteAsync(product);
        }

        /// <summary>
        /// Product CreateAsync override.
        /// Creates the product and all its dependant relationships.
        /// </summary>
        public async override Task CreateAsync(Product product)
        {
            var newFeatures = product.Features;
            product.Features = null; // To prevent EF from creating them automatically.

            // Create product.
            await base.CreateAsync(product);

            // Create new features.
            foreach (var feature in newFeatures)
            {
                feature.Product = product;
                await featureService.CreateAsync(feature);
            }
        }

        /// <summary>
        /// Product UpdateAsync override.
        /// Updates the product and all its dependant relationships.
        /// </summary>
        public async override Task UpdateAsync(Product product)
        {
            // Get current product from db.
            // Only include relationships that need to be auto-updated by EF Core.
            var dbProduct = await base.GetAll()
                .Where(p => p.Id == product.Id)
                .Include(p => p.Category)
                .Include(p => p.Editors)
                .FirstOrDefaultAsync();

            // Update features using the Feature service.
            await this.UpdateFeatures(product.Id, product.Features);

            // Set values from memory product to db tracked product.
            product.CopyTo(dbProduct);

            // Update product.
            await base.UpdateAsync(dbProduct);
        }

        /// <summary>
        /// Update features throw the Feature Service comparing the product features currently on the db and the in memory product features.
        /// </summary>
        private async Task UpdateFeatures(Guid productId, List<Feature> newFeatures)
        {
            var dbFeatures = this.featureService.GetAll().Where(f => f.Product.Id == productId).AsNoTracking().ToList();

            // Create any features that exist in newFeatures but not in dbFeatures.
            foreach (var feature in newFeatures.Except(dbFeatures, new EntityComparer<Feature>()))
                await featureService.CreateAsync(feature);

            // Update any features that exist both in newFeatures and in dbFeatures
            foreach (var feature in newFeatures.Intersect(dbFeatures, new EntityComparer<Feature>()))
                await featureService.UpdateAsync(feature);

            // Remove any features that exist in dbFeatures but not in newFeatures.
            foreach (var feature in dbFeatures.Except(newFeatures, new EntityComparer<Feature>()))
                await featureService.DeleteAsync(feature);
        }

        public async Task<Guid> GetFirstId()
        {
            var firstProductId = await this.GetAll().Select(p => p.Id).FirstAsync();
            return firstProductId;
        }
    }
}