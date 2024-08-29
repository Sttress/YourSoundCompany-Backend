using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Security.Claims;
using YourSoundCompnay.SesseionService;

namespace YourSoundCompnay.Api.Filter
{
    public class SessionFilter : IAsyncResourceFilter
    {
        private readonly ISessionService _sessionService;

        public SessionFilter(ISessionService sessionService)
        {
            _sessionService = sessionService;
        }

        public async Task OnResourceExecutionAsync(ResourceExecutingContext context, ResourceExecutionDelegate next)
        {
            var endpoint = context.HttpContext.GetEndpoint();
            var metadata = endpoint?.Metadata;


            var allowAnonymous = metadata?.GetMetadata<IAllowAnonymous>() != null;

            if (allowAnonymous)
            {
                await next();
                return;
            }

            var claims = context.HttpContext.User.Claims.ToList();

            _sessionService.UserId = long.Parse(claims?.FirstOrDefault(e => e.Type == ClaimTypes.Sid)?.Value);
            _sessionService.Email = claims?.FirstOrDefault(e => e.Type == ClaimTypes.Email)?.Value;
            await next();

        }

    }
}
