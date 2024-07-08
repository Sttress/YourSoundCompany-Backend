using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YourSoundCompnay.Business.Model.User;
using YourSoundCompnay.Business.Model;
using YourSoundCompany.Business.Model.User.DTO;

namespace YourSoundCompnay.Business
{
    public interface IUserService
    {
        Task<BaseResponse<UserResponseDTO>> Create(UserCreateDTO model);
        Task<BaseResponse<UserResponseDTO>> Update(UserUpdateDTO model);
        Task<BaseResponse<UserResponseDTO>> GetById(long? id);
        Task<UserResponseDTO?> GetCurrentUser();
        Task<BaseResponse<UserResponseDTO>> VerifyEmailCode(UserVerificationEmailDTO model);
        Task<List<UserModel>> GetByEmail(string email);
        Task<BaseResponse<UserResponseDTO>> RecoveryPassword(string email);
        Task<BaseResponse<UserResponseDTO>> RecoveryPasswordVerified(UserRecoveryPasswordVerifiedDTO model);
    }
}
