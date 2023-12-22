using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SystemStock.Business.Model.User;
using SystemStock.Business.Model;

namespace SystemStock.Business.Service
{
    public interface IUserService
    {
        Task<BaseResponse<UserResponseModel>> CreateUpdate(UserCreateModel model);
        Task<BaseResponse<UserResponseModel>> GetById(long id);
        Task<bool> VerifyUserById(long id);
    }
}
