using YourSoundCompnay.Business.Model;
using YourSoundCompany.Business.Model.Authentication.DTO;

namespace YourSoundCompnay.Business
{
    public interface IAuthenticationService
    {
        Task<BaseResponse<AuthLoginResponseDTO>> Login(AuthLoginDTO model);
        Task<BaseResponse<AuthLoginResponseDTO>> RefreshToken(AuthDTO model);
    }
}
