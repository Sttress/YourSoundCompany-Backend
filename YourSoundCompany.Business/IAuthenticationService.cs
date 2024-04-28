

using YourSoundCompnay.Business.Model.User;
using YourSoundCompnay.Business.Model;

namespace YourSoundCompnay.Business
{
    public interface IAuthenticationService
    {
        Task<BaseResponse<UserLoginResponseModel>> Login(UserLoginModel model);

    }
}
