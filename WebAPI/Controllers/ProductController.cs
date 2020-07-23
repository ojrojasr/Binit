using Binit.Framework.Constants.Authentication;
using Domain.Entities.Model;
using Domain.Entities.Model.Views;
using Domain.Logic.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Attributes;
using WebAPI.DTOs.ProductDTOs;

namespace WebAPI.Controllers
{
    [AuthorizeAnyRoles(Roles.BackofficeSuperAdministrator, Roles.BackofficeProductAdministrator, Roles.BackofficeProductUser)]
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {

        #region Properties

        private readonly IProductService productService;

        #endregion

        #region Constructor

        public ProductController(IProductService productService)
        {
            this.productService = productService;
        }

        #endregion

        #region Endpoints

        /// <summary>
        /// Get a list with all the products
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductDTO>>> Get()
        {
            var products = await this.productService.GetAll().ToListAsync();

            return products.Select(p => new ProductDTO(p)).ToList();
        }

        /// <summary>
        /// Get a list with all the products and a feature count
        /// </summary>
        [HttpGet("GetByView")]
        public ActionResult<IEnumerable<ProductFeaturesView>> GetByView()
        {
            var products = this.productService.GetThroughView();

            return products.ToList();
        }

        /// <summary>
        /// Get a specific product by id
        /// </summary>
        /// <param name="id">Id of the product you want to get</param>        
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductDTO>> Get(string id)
        {
            var product = await productService.GetFullAsync(new Guid(id));
            return Ok(new ProductDTO(product));
        }

        /// <summary>
        /// Creates a new product.
        /// Only available for Backoffice Administrators
        /// </summary>
        [AuthorizeAnyRoles(Roles.BackofficeSuperAdministrator, Roles.BackofficeProductAdministrator)]
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] ProductDTO productDTO)
        {
            Product product = productDTO.ToEntity();
            await this.productService.CreateAsync(product);

            return Ok();
        }

        /// <summary>
        /// Updates an existing product.
        /// Only available for Backoffice Administrators
        /// </summary>
        /// <param name="id">Id of the product you want to update</param>
        /// <param name="productDTO">product you want to update</param>
        [AuthorizeAnyRoles(Roles.BackofficeSuperAdministrator, Roles.BackofficeProductAdministrator)]
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(string id, [FromBody] ProductDTO productDTO)
        {
            productDTO.Id = id;

            await productService.UpdateAsync(productDTO.ToEntity());

            return Ok();
        }

        /// <summary>
        /// Deletes an existing product.
        /// Only available for Backoffice Administrators
        /// </summary>
        /// <param name="id">Id of the product you want to delete</param>
        [AuthorizeAnyRoles(Roles.BackofficeSuperAdministrator, Roles.BackofficeProductAdministrator)]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            await productService.DeleteAsync(new Guid(id));

            return Ok();
        }

        #endregion
    }
}
