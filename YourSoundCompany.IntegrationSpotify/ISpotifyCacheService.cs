using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YourSoundCompany.IntegrationSpotify
{
    public interface ISpotifyCacheService
    {
        Task RemoveCodeVerifyInCache(string email);
        Task<string?> GetCodeVerify(string email);
        Task<string?> GetRefreshTokenSpotify(string email);
        Task<string?> GetTokenSpotify(string email);
        Task SaveCodeVerify(string email, string codeVerify);
        Task SaveSpotify(string email, string token, string refreshToken);
    }
}
