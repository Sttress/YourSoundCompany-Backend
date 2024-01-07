using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SystemStock.Business.Model.Category;
using SystemStock.Business.Model;

namespace SystemStock.Business.Service
{
    public interface ICategoryService
    {
        Task<BaseResponse<CategoryModel>> Create(CategoryModel model);
        Task Active(long categoryId);
        Task<BaseResponse<List<CategoryModel>>> GetList();
    }
}
