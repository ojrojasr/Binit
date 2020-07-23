using Domain.Logic.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using WebAPI.DTOs.CallToActionDTOs;
using WebAPI.DTOs.ProductDTOs;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CallToActionController : ControllerBase
    {

        #region Properties

        private readonly IProductService productService;

        #endregion

        #region Constructor

        public CallToActionController(IProductService productService)
        {
            this.productService = productService;
        }

        #endregion

        #region Endpoints
        [HttpGet("see-products")]
        [Authorize]
        public ActionResult<CallToActionDTO<object>> seeProducts()
        {
            return new CallToActionDTO<object>("/products");
        }

        [HttpGet("see-new-product")]
        [Authorize]
        public ActionResult<CallToActionDTO<ProductDTO>> seeNewProduct()
        {
            var product = new ProductDTO()
            {
                Name = "New product",
                Description = "Created server-side"
            };

            return new CallToActionDTO<ProductDTO>("/product", product);
        }

        [HttpGet("see-some-product")]
        [Authorize]
        public async Task<CallToActionDTO<object>> seeSomeProduct()
        {
            var productId = await this.productService.GetFirstId();

            return new CallToActionDTO<object>("/product/" + productId);
        }

        #endregion
    }
}