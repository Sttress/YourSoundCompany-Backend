using Microsoft.AspNetCore.Mvc;
using SystemStock.Business.Model;
using SystemStock.Business.Model.Category;
using SystemStock.Business.Service;

namespace SystemStock.Api.Controllers
{
    public class CategoryController : BaseController
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService) 
        { 
            _categoryService = categoryService;
        }

        [HttpPost("")]
        public async Task<BaseResponse<CategoryModel>> Create([FromBody]CategoryModel model)
        {
            return await _categoryService.Create(model);
        }

        [HttpPut("Active")]
        public async Task Active([FromQuery]long Id)
        {
            await _categoryService.Active(Id);
        }

        [HttpGet("List")]
        public async Task<BaseResponse<List<CategoryModel>>> List()
        {
            return await _categoryService.GetList();
        }
    }
}
