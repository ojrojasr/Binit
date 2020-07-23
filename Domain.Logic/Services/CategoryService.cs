using Binit.Framework;
using Binit.Framework.ExceptionHandling.Types;
using Binit.Framework.Interfaces.DAL;
using Binit.Framework.Interfaces.ExceptionHandling;
using DAL;
using Domain.Entities.Model;
using Domain.Entities.Model.SPs;
using Domain.Logic.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lang = Binit.Framework.Localization.LocalizationConstants.DomainLogic.Services.CategoryService;

namespace Domain.Logic.Services
{
    public class CategoryService : Service<Category>, ICategoryService
    {
        private readonly IProductService productService;
        private readonly ISPService<SPCreateCategory> spCategory;

        public CategoryService(IExceptionManager exceptionManager, ILogger logger, IOperationContext operationContext,
        IUnitOfWork unitOfWork, IStringLocalizer<SharedResources> localizer, IProductService productService, ISPService<SPCreateCategory> spCategory)
            : base(exceptionManager, logger, operationContext, unitOfWork, localizer)
        {
            this.productService = productService;
            this.spCategory = spCategory;
        }

        public List<SPReturnCategory> CreateCategoryBySP()
        {
            var parameters = new Dictionary<string, object>();
            parameters.Add("@UserId", operationContext.GetUserId());
            parameters.Add("@Name", $"Category{DateTime.Now.ToString("mmss")}");
            parameters.Add("@Description", $"Category Description: {DateTime.Now.ToString("mmss")}");
            // Only executes The SP
            spCategory.ExecuteSP(new SPCreateCategory { Params = parameters });
            // Executes the SP and retrieves the data 
            return spCategory.ExecuteSP<SPReturnCategory>(new SPCreateCategory { Params = parameters }).ToList();
        }

        /// <summary>
        /// Removes category.
        /// May throw UserException if the category is related to an existent product.
        /// </summary>
        public override void Delete(Category entity)
        {
            // Check if the category is related to a product.
            var isRelatedToProduct = productService.GetAll().Where(p => p.CategoryId == entity.Id).Any();
            if (isRelatedToProduct)
                throw base.exceptionManager.Handle(new UserException(this.localizer[Lang.DeleteWithRelatedProductEx]));

            // If it's not related, delete.
            base.Delete(entity);
        }

        /// <summary>
        /// Asynchronously removes category.
        /// May throw UserException if the category is related to an existent product.
        /// </summary>
        public async override Task DeleteAsync(Category entity)
        {
            // Check if the category is related to a product.
            var isRelatedToProduct = await productService.GetAll().Where(p => p.CategoryId == entity.Id).AnyAsync();
            if (isRelatedToProduct)
                throw base.exceptionManager.Handle(new UserException(this.localizer[Lang.DeleteAsyncWithRelatedProductEx]));

            // If it's not related, delete.
            await base.DeleteAsync(entity);
        }
    }
}