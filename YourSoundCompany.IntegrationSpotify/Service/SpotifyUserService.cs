using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;
using System.Net.Http.Headers;
using System.Reflection;
using YourSoundCompany.IntegrationSpotify.Model.User;


namespace YourSoundCompany.IntegrationSpotify.Service
{
    public class SpotifyUserService : ISpotifyUserService
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;


        public SpotifyUserService
            (
            HttpClient httpClient, 
            IConfiguration configuration
            ) 
        {
            _httpClient = httpClient;
            _configuration = configuration;
        }

       public async Task<Artist> GetArtist()
       {
            try
            {

                //var user = await _userService.GetCurrentUser();

                //if (user is null)
                //{
                //    result.Message.Add("Usuário logado não pode ser encontrado");
                //    return result;
                //}

                //var token = await GetTokenSpotify(user.Email);

                //if (string.IsNullOrEmpty(token))
                //{
                //    result.Message.Add("Usuário logado não pode ser encontrado");
                //    return result;
                //}

                //var timeRangeTerm = GetTimeRangeTerm(model.TimeRange);

                //var request = new HttpRequestMessage(HttpMethod.Get, $"me/top/artists?time_range={timeRangeTerm}&limit=50&offset=10");
                //request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);

                //HttpResponseMessage response = await _httpClient.SendAsync(request);

                //if (!response.IsSuccessStatusCode)
                //{
                //    throw new Exception(response.RequestMessage.ToString());
                //}

                //string responseBody = await response.Content.ReadAsStringAsync();

                //JObject jsonResponse = JObject.Parse(responseBody);
                //var spotifyModel = jsonResponse.ToObject<SpotifyArtistModel>();

                //result.Data = spotifyModel;

                //return result;

                return new Artist();
            }
            catch (Exception ex)
            {
                throw new Exception("Falha ao obter dashboard", ex);
            }
        }
    }
}
