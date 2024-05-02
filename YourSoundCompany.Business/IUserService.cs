using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YourSoundCompnay.Business.Model.User;
using YourSoundCompnay.Business.Model;
using YourSoundCompany.Business.Model.User;

namespace YourSoundCompnay.Business
{
    public interface IUserService
    {
        Task<BaseResponse<UserResponseModel>> CreateUpdate(UserCreateModel model);
        Task<BaseResponse<UserResponseModel>> GetById(long? id);
        Task<bool> VerifyUserById(long id);
        Task<UserResponseModel?> GetCurrentUser();
        Task<BaseResponse<UserResponseModel>> VerifyEmailCode(UserVerificationEmail model);
    }
}
