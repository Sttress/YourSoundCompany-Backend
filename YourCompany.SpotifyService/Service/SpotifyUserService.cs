
using Newtonsoft.Json;
using YourCompany.SpotifyService.Model;
using YourCompany.SpotifyService.Model.Response;

namespace YourCompany.SpotifyService.Service
{
    public class SpotifyUserService : ISpotifyUserService
    {
        private readonly HttpClient _httpClient;

        public SpotifyUserService(IHttpClientFactory httpClient) 
        {
            _httpClient = httpClient.CreateClient();
        }

        public async Task<List<SpotifyArtistsModel>> GetTopArtistUserItems()
        {
            var result = new List<SpotifyArtistsModel>();
            var url = "me/top/artists";

            while (!string.IsNullOrEmpty(url))
            {
                var response = await _httpClient.GetAsync(url);
                response.EnsureSuccessStatusCode();

                var responseString = await response.Content.ReadAsStringAsync();
                var data = JsonConvert.DeserializeObject<UserTopItemsSpotifyResponse<SpotifyArtistsModel>>(responseString);

                if (data != null && data.Items != null)
                {
                    result.AddRange(data.Items);
                }

                url = data.Next;
            }

            return result;
        }

        public async Task<List<SpotifyTracksModel>> GetTopTracksUserItems()
        {
            var result = new List<SpotifyTracksModel>();
            var url = "me/top/tracks";

            while (!string.IsNullOrEmpty(url))
            {
                var response = await _httpClient.GetAsync(url);
                response.EnsureSuccessStatusCode();

                var responseString = await response.Content.ReadAsStringAsync();
                var data = JsonConvert.DeserializeObject<UserTopItemsSpotifyResponse<SpotifyTracksModel>>(responseString);

                if (data != null && data.Items != null)
                {
                    result.AddRange(data.Items);
                }

                url = data.Next;
            }

            return result;
        }
    }
}
