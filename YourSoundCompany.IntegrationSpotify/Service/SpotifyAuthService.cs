
using Microsoft.Extensions.Configuration;
using System.Text.RegularExpressions;
using System.Text;
using System.Security.Cryptography;
using Newtonsoft.Json.Linq;

namespace YourSoundCompany.IntegrationSpotify.Service
{
    public class SpotifyAuthService : ISpotifyAuthService
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;
        private readonly ISpotifyCacheService _spotifyCacheService;
        public SpotifyAuthService
            (
                HttpClient httpClient,
                IConfiguration configuration,
                ISpotifyCacheService spotifyCacheService
            )
        {
            _httpClient = httpClient;
            _configuration = configuration;
            _spotifyCacheService = spotifyCacheService;
        }

        private const string possibleChars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";

        public async Task<string> GetCodeUrl(string email)
        {
            var ClientId = _configuration["Spotify:ClientId"];
            var RedirectUri = _configuration["Spotify:RedirectUri"];
            var scope = "user-library-read user-read-private user-read-email user-top-read";
            var codeVerify = GenerateNonce();
            var codeChallenge = GenerateCodeChallenge(codeVerify);


             await _spotifyCacheService.SaveCodeVerify(email, codeVerify);

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

        public async Task GetAuthorization(string email, string code)
        {
            var redirectUri = _configuration["Spotify:RedirectUri"];
            var clientId = _configuration["Spotify:ClientId"];
            var clientSecret = _configuration["Spotify:ClientSecret"];


            var codeVerify = await _spotifyCacheService.GetCodeVerify(email);

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
            var jsonResponse = JObject.Parse(responseString);

            var token = jsonResponse.SelectToken("access_token").ToString();
            var refreshToken = jsonResponse.SelectToken("refresh_token").ToString();

            await _spotifyCacheService.SaveSpotify(email, token, refreshToken);

        }
    }
}
