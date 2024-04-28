using Microsoft.Extensions.Configuration;
using System.Security.Cryptography;
using System.Text;


namespace YourSoundCompany.IntegrationSpotify.Service
{
    public class SpotifyService : ISpotifyService
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;

        private const string possibleChars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";

        public SpotifyService
            (
            HttpClient httpClient, 
            IConfiguration configuration
            ) 
        {
            _httpClient = httpClient;
            _configuration = configuration;
        }

        private static  string GenerateRandomString()
        {
            var length = new Random().Next(10,100);
            StringBuilder stringBuilder = new StringBuilder(length);
            for (int i = 0; i < length; i++)
            {
                stringBuilder.Append(possibleChars[length]);
            }
            return stringBuilder.ToString();
        }

        private byte[] sha256(string plain)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] data = Encoding.UTF8.GetBytes(plain);
                return sha256.ComputeHash(data);
            }
        }

        private static string Base64Encode(byte[] input)
        {
            string base64String = Convert.ToBase64String(input);
            base64String = base64String.Replace("=", "").Replace("+", "-").Replace("/", "_");
            return base64String;
        }
        private string GenerateCodeChallenge(string codeVerifier)
        {
            byte[] hashed = sha256(codeVerifier);
            string codeChallenge = Base64Encode(hashed);
            return codeChallenge;
        }

        public async Task<string> GetCodeUrl()
        {
            var  ClientId = "5cf9fd323df749a5ac21f489d0347a47";
            var RedirectUri = "https://localhost:3000";
            var scope = "user-library-read user-read-private user-read-email";
            var codeVerifier = "your_code_verifier_here"; // Substitua pelo seu código verificador
            var codeChallenge = GenerateCodeChallenge(codeVerifier);

            using (HttpClient httpClient = new HttpClient())
            {
                // Construção dos parâmetros da requisição
                var parameters = new Dictionary<string, string>
            {
                { "response_type", "code" },
                { "client_id", ClientId },
                { "scope", scope },
                { "code_challenge_method", "S256" },
                { "code_challenge", codeChallenge },
                { "redirect_uri", RedirectUri }
            };

                // Construção da URL de autorização
                var authUrl = new UriBuilder("https://accounts.spotify.com/authorize");
                authUrl.Query = string.Join("&", parameters.Select(kv => $"{kv.Key}={Uri.EscapeDataString(kv.Value)}"));

                // Realização da requisição HTTP
                HttpResponseMessage response = await httpClient.GetAsync(authUrl.Uri);

                // Verificação da resposta
                if (!response.IsSuccessStatusCode)
                {
                    throw new Exception(response.RequestMessage.ToString());
                }
                return response.RequestMessage.RequestUri.ToString();

            }

        }

        public Task GenerateAuthorizationUrl()
        {
            throw new NotImplementedException();
        }
    }
}
