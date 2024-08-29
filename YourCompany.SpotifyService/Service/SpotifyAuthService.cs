using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using YourCompany.SpotifyService.Model;
using YourSoundCompany.CacheService.Service;

namespace YourCompany.SpotifyService.Service
{
    public class SpotifyAuthService : ISpotifyAuthService
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;
        private readonly ICacheService _cacheService;

        private const string possibleChars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
        private string _KeyAuthSpotify(string email) => $"key_auth_spotify_{email}";
        private string _KeyCodeVerify(string email) => $"key_code_verify_spotify_{email}";

        public SpotifyAuthService
            (
                HttpClient httpClient,
                IConfiguration configuration, 
                ICacheService cacheService
            ) 
        {
            _httpClient = httpClient;
            _configuration = configuration;
            _cacheService = cacheService;   
        }

        public async Task<string> GetUrlAuthorization(string email)
        {
            var ClientId = _configuration["Spotify:ClientId"];
            var RedirectUri = _configuration["Spotify:RedirectUri"];
            var scope = "user-library-read user-read-private user-read-email user-top-read";
            var codeVerify = GenerateNonce();
            var codeChallenge = GenerateCodeChallenge(codeVerify);


            await SaveCodeVerify(email, codeVerify);

            var parameters = new Dictionary<string, string>
            {
                { "response_type", "code" },
                { "client_id", ClientId },
                { "scope", scope },
                { "code_challenge_method", "S256" },
                { "code_challenge", codeChallenge },
                { "redirect_uri", RedirectUri }

            };

            var authUrl = new UriBuilder("https://accounts.spotify.com/authorize");
            authUrl.Query = string.Join("&", parameters.Select(kv => $"{kv.Key}={Uri.EscapeDataString(kv.Value)}"));


            try
            {
                HttpResponseMessage response = await _httpClient.GetAsync(authUrl.Uri);

                if (!response.IsSuccessStatusCode)
                {
                    throw new Exception(response.RequestMessage.ToString());
                }
                return response.RequestMessage.RequestUri.AbsoluteUri.ToString();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task GetToken(string email, string code)
        {
            var redirectUri = _configuration["Spotify:RedirectUri"];
            var clientId = _configuration["Spotify:ClientId"];
            var clientSecret = _configuration["Spotify:ClientSecret"];


            var codeVerify = await GetCodeVerify(email);

            var authOptions = new Dictionary<string, string>
            {
                { "client_id", clientId },
                { "code", code },
                { "code_verifier", codeVerify },
                { "redirect_uri", redirectUri },
                { "grant_type", "authorization_code" }
            };

            var authHeader = Convert.ToBase64String(Encoding.UTF8.GetBytes($"{clientId}:{clientSecret}"));

            var request = new HttpRequestMessage(HttpMethod.Post, "https://accounts.spotify.com/api/token")
            {
                Content = new FormUrlEncodedContent(authOptions)
            };
            request.Headers.Add("Authorization", $"Basic {authHeader}");
            request.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/x-www-form-urlencoded");

            HttpResponseMessage response = await _httpClient.SendAsync(request);

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception(response.RequestMessage.ToString());

            }
            var responseString = await response.Content.ReadAsStringAsync();
            var tokenResponse = JsonConvert.DeserializeObject<SpotifyTokenResponse>(responseString);

            if (tokenResponse is not null)
                await SaveSpotify(email, tokenResponse);
        }

        private async Task RefreshToken(string email)
        {
            var clientId = _configuration["Spotify:ClientId"];
            var clientSecret = _configuration["Spotify:ClientSecret"];

            var refreshToken = (await GetAuthSpotify(email)).RefreshToken;

            var authOptions = new Dictionary<string, string>
            {
                { "client_id", clientId },
                { "refresh_token", refreshToken },
                { "grant_type", "refresh_token" }
            };

            var authHeader = Convert.ToBase64String(Encoding.UTF8.GetBytes($"{clientId}:{clientSecret}"));

            var request = new HttpRequestMessage(HttpMethod.Post, "https://accounts.spotify.com/api/token")
            {
                Content = new FormUrlEncodedContent(authOptions)
            };
            request.Headers.Add("Authorization", $"Basic {authHeader}");
            request.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/x-www-form-urlencoded");

            HttpResponseMessage response = await _httpClient.SendAsync(request);

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception(response.RequestMessage.ToString());

            }
            var responseString = await response.Content.ReadAsStringAsync();
            var tokenResponse = JsonConvert.DeserializeObject<SpotifyTokenResponse>(responseString);

            if (tokenResponse is not null)
                await SaveSpotify(email, tokenResponse);
        }

        private static string GenerateNonce()
        {
            var random = new Random();
            var nonce = new char[128];
            for (int i = 0; i < nonce.Length; i++)
            {
                nonce[i] = possibleChars[random.Next(possibleChars.Length)];
            }

            return new string(nonce);
        }

        private static string GenerateCodeChallenge(string codeVerifier)
        {
            using var sha256 = SHA256.Create();
            var hash = sha256.ComputeHash(Encoding.UTF8.GetBytes(codeVerifier));
            var b64Hash = Convert.ToBase64String(hash);
            var code = Regex.Replace(b64Hash, "\\+", "-");
            code = Regex.Replace(code, "\\/", "_");
            code = Regex.Replace(code, "=+$", "");
            return code;
        }


        public async Task SaveSpotify(string email, SpotifyTokenResponse tokenResponse)
        {
            await _cacheService.Set(_KeyAuthSpotify(email), tokenResponse, TimeSpan.FromHours(1));
        }

        public async Task<SpotifyTokenResponse?> GetAuthSpotify(string email)
        {
            return await _cacheService.Get<SpotifyTokenResponse>(_KeyAuthSpotify(email));
        }
        public async Task RemoveAuthSpotify(string email)
        {
            await _cacheService.Remove(_KeyAuthSpotify(email));
        }

        public async Task SaveCodeVerify(string email, string codeVerify)
        {
            await _cacheService.Set(_KeyCodeVerify(email), codeVerify, TimeSpan.FromHours(1));
        }
        public async Task<string?> GetCodeVerify(string email)
        {
            return await _cacheService.Get<string>(_KeyCodeVerify(email));
        }
        public async Task RemoveCodeVerify(string email)
        {
            await _cacheService.Remove(_KeyCodeVerify(email));
        }
    }
}
