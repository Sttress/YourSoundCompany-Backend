using Microsoft.Extensions.Configuration;
using System.Text;

namespace YourCompany.SpotifyService.Service
{
    public class HttpClientFactory:IHttpClientFactory
    {
        private readonly IConfiguration _configuration;

        public HttpClientFactory(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public HttpClient CreateClient()
        {
            var clientId = _configuration["Spotify:ClientId"];
            var clientSecret = _configuration["Spotify:ClientSecret"];
            var baseUrl = _configuration["Spotify:SpotifyConsultUri"];

            var client = new HttpClient
            {
                BaseAddress = new Uri(baseUrl)
            };

            var authHeader = Convert.ToBase64String(Encoding.UTF8.GetBytes($"{clientId}:{clientSecret}"));
            client.DefaultRequestHeaders.Add("Authorization", $"Basic {authHeader}");
            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/x-www-form-urlencoded"));

            return client;
        }
    }
}
