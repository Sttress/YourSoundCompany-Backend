

using YourSoundCompnay.Business.Model.User;
using YourSoundCompnay.Business.Model;
using YourSoundCompany.Business.Model.Authentication;

namespace YourSoundCompnay.Business
{
    public interface IAuthenticationService
    {
        Task<BaseResponse<UserLoginResponseModel>> Login(UserLoginModel model);
        Task<BaseResponse<UserLoginResponseModel>> RefreshToken(AuthModel model);
    }
}
