using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SystemStock.Business.Model;
using SystemStock.Business.Model.StoreProduct;

namespace SystemStock.Business.Service
{
    public interface IStoreProductService
    {
        Task<IPagedList<StoreProductModel>> GetByStore(long StoreId);
        Task<BaseResponse<List<StoreProductModel>>> SaveProductListForStore(StoreProductRequestModel model);
    }
}
