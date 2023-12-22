using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;
using Swashbuckle.AspNetCore.SwaggerUI;

namespace SystemStock.Api
{
    public static class SwaggerConfiguration
    {

        public static void ConfigureSwaggerService(IServiceCollection services, Action<SwaggerGenOptions> func = null)
        {
            Action<SwaggerGenOptions> func2 = func;
            services.AddSwaggerGen(delegate (SwaggerGenOptions c)
            {
                c.IgnoreObsoleteProperties();
                c.IgnoreObsoleteActions();
                OpenApiSecurityScheme openApiSecurityScheme = new OpenApiSecurityScheme
                {
                    Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.Http,
                    Scheme = "Bearer",
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                    }
                };
                c.AddSecurityDefinition("Bearer", openApiSecurityScheme);
                c.AddSecurityRequirement(new OpenApiSecurityRequirement {
                {
                    openApiSecurityScheme,
                    Array.Empty<string>()
                } });
                if (func2 != null)
                {
                    func2(c);
                }
            });
        }

        public static void ConfigureSwaggerApp(IApplicationBuilder app, Action<SwaggerUIOptions> funcUI = null, Action<SwaggerOptions> func = null)
        {
            Action<SwaggerOptions> func2 = func;
            Action<SwaggerUIOptions> funcUI2 = funcUI;
            app.UseSwagger(delegate (SwaggerOptions c)
            {
                if (func2 != null)
                {
                    func2(c);
                }
            });
            app.UseSwaggerUI(delegate (SwaggerUIOptions c)
            {
                c.DocExpansion(DocExpansion.None);
                if (funcUI2 != null)
                {
                    funcUI2(c);
                }
            });
        }
    }
}
