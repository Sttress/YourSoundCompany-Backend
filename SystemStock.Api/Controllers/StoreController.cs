using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PagedList;
using SystemStock.Business.Model;
using SystemStock.Business.Model.Store;
using SystemStock.Business.Service;

namespace SystemStock.Api.Controllers
{

    public class StoreController : BaseController
    {
        private readonly IStoreService _storeService;

        public StoreController(IStoreService storeService) 
        { 
            _storeService = storeService;
        }

        [HttpPost("")]
        public async Task<BaseResponse<List<StoreModel>>> CreateUpdate([FromBody] StoreModel model)
        {
            return await _storeService.CreateUpdate(model);
        }

        [HttpGet("GetByUesr")]
        public async Task<BaseResponse<IPagedList<StoreModel>>> GetByUser([FromQuery] long Id)
        {
            return await _storeService.GetByUser(Id);
        }
    }
}
