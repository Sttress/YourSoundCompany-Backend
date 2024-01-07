
using SystemStock.Business.Model.Product;
using SystemStock.Business.Model;
using PagedList;

namespace SystemStock.Business.Service
{
    public interface IProductService
    {
        Task<BaseResponse<ProductModel>> CreateUpdate(ProductModel model);
        Task Active(long Id);
        Task<BaseResponse<ProductModel>> GetById(long Id);
        Task<IPagedList<ProductModel>> GetList();
    }
}
