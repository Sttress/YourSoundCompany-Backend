using Microsoft.AspNetCore.Http;
using System.Text;
using System.Web;


namespace SystemStock.SessionService.Service
{
    public class SessionService : ISessionService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public SessionService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public string SetSession
        {
            get => _httpContextAccessor.HttpContext.Session.Keys.FirstOrDefault(e => e.);
            set => _httpContextAccessor.HttpContext.Session.Set("Id", Encoding.UTF8.GetBytes(value));
        }
    }
}
