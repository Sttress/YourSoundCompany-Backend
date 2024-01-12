using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PagedList;
using SystemStock.Business.Model;
using SystemStock.Business.Model.Store;
using SystemStock.Business.Model.StoreProduct;
using SystemStock.Business.Service;

namespace SystemStock.Api.Controllers
{

    public class StoreController : BaseController
    {
        private readonly IStoreService _storeService;
        private readonly IStoreProductService _storeProductService;

        public StoreController(IStoreService storeService,IStoreProductService storeProductService) 
        { 
            _storeService = storeService;
            _storeProductService = storeProductService;
        }

        [HttpPost("")]
        public async Task<BaseResponse<List<StoreModel>>> CreateUpdate([FromBody] StoreModel model)
        {
            return await _storeService.CreateUpdate(model);
        }

        [HttpGet("GetByUser")]
        public async Task<BaseResponse<IPagedList<StoreModel>>> GetByUser([FromQuery] long Id)
        {
            return await _storeService.GetByUser(Id);
        }

        //[HttpPost("SaveProductListForStore")] 
        //public async Task<BaseResponse<StoreProductRequestModel>> SaveProductListForStore([FromBody] StoreProductRequestModel model)
        //{
        //    return await 
        //}

        [HttpGet("GetByStore")]
        public async Task<IPagedList<StoreProductModel>> GetProductsByStore([FromQuery] long StoreId ) 
        {
            return await _storeProductService.GetByStore(StoreId);
        }
    }
}
