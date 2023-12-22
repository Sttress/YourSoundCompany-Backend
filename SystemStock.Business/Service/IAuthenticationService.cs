

using SystemStock.Business.Model.User;
using SystemStock.Business.Model;

namespace SystemStock.Business.Service
{
    public interface IAuthenticationService
    {
        Task<BaseResponse<UserLoginResponseModel>> Login(UserLoginModel model);

    }
}
