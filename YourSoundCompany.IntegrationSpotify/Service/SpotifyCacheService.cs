
using YourSoundCompany.CacheService.Service;

namespace YourSoundCompany.IntegrationSpotify.Service
{
    public class SpotifyCacheService : ISpotifyCacheService
    {
        private readonly ICacheService _cacheService;
        public SpotifyCacheService(ICacheService cacheService) 
        { 
            _cacheService = cacheService;
        }
        private string _KeyTokenSpotify(string email) => $"key_token_spotify_{email}";
        private string _KeyRefreshTokenSpotify(string email) => $"key_refresh_token_spotify_{email}";
        private string _KeyCodeVerify(string email) => $"key_code_verify_spotify_{email}";


        public async Task SaveSpotify(string email, string token, string refreshToken)
        {
            await _cacheService.Set(_KeyTokenSpotify(email), token, TimeSpan.FromHours(1));
            await _cacheService.Set(_KeyRefreshTokenSpotify(email), refreshToken, TimeSpan.FromHours(1));
        }

        public async Task<string?> GetTokenSpotify(string email)
        {
            return await _cacheService.Get<string>(_KeyTokenSpotify(email));
        }
        public async Task<string?> GetRefreshTokenSpotify(string email)
        {
            return await _cacheService.Get<string>(_KeyRefreshTokenSpotify(email));
        }

        public async Task SaveCodeVerify(string email, string codeVerify)
        {
            await _cacheService.Set(_KeyCodeVerify(email), codeVerify, TimeSpan.FromHours(1));
        }
        public async Task<string?> GetCodeVerify(string email)
        {
            return await _cacheService.Get<string>(_KeyCodeVerify(email));
        }
        public async Task RemoveCodeVerifyInCache(string email)
        {
            await _cacheService.Remove(_KeyCodeVerify(email));
        }
    }
}
