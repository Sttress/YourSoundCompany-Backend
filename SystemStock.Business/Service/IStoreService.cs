

using SystemStock.Business.Model.Store;
using SystemStock.Business.Model;
using PagedList;

namespace SystemStock.Business.Service
{
    public interface IStoreService
    {
        Task<BaseResponse<List<StoreModel>>> CreateUpdate(StoreModel model);
        Task<BaseResponse<IPagedList<StoreModel>>> GetByUser(long Id);
    }
}
