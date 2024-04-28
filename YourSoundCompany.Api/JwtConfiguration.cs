using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authentication;
using Microsoft.IdentityModel.Tokens;
using YourSoundCompnay.Business.Model.Base;
using Microsoft.Extensions.DependencyInjection;
using System.Text;

namespace YourSoundCompnay.Api
{
    public static class JwtConfiguration
    {
        public static void ConfigureServices(IServiceCollection services, JwtConfigureModel jwtDescriptor)
        {
            JwtConfigureModel jwtDescriptor2 = jwtDescriptor;
            services.AddAuthentication(delegate (AuthenticationOptions x)
            {
                x.DefaultAuthenticateScheme = "Bearer";
                x.DefaultChallengeScheme = "Bearer";
            }).AddJwtBearer(delegate (JwtBearerOptions x)
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtDescriptor2.SecretKey)),
                    ValidateIssuer = true,
                    ValidIssuer = jwtDescriptor2.Issuer,
                    ValidateAudience = true,
                    ValidAudience = jwtDescriptor2.Audience,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.FromSeconds(30.0)
                };
            });
        }
    }
}
