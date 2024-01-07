using Microsoft.AspNetCore.Mvc;
using PagedList;
using SystemStock.Business.Model;
using SystemStock.Business.Model.Product;
using SystemStock.Business.Service;

namespace SystemStock.Api.Controllers
{
    public class ProductController : BaseController
    {
        private readonly IProductService _productService;
        public ProductController(IProductService productService) 
        {
            _productService = productService;
        }

        [HttpPost("")]
        public async Task<BaseResponse<ProductModel>> CreateUpdate([FromBody] ProductModel model)
        {
            return await _productService.CreateUpdate(model);
        }

        [HttpPut("Active")]
        public async Task Active([FromQuery] long productId)
        {
            await _productService.Active(productId);
        }

        [HttpGet("GetById")]
        public async Task<BaseResponse<ProductModel>> GetById([FromQuery] long productId)
        {
            return await _productService.GetById(productId);
        }

        [HttpGet("GetList")]
        public async Task<IPagedList<ProductModel>> GetByList()
        {
            return await _productService.GetList();
        }
    }
}
