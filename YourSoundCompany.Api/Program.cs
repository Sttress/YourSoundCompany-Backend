
using Microsoft.OpenApi.Models;
using YourSoundCompnay.Api;
using YourSoundCompnay.Api.Filter;
using YourSoundCompnay.Business.Model.Base;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers(opt =>
{
    opt.Filters.Add<SessionFilter>();
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

var descriptor = builder.Configuration.GetSection("JWTDescriptor").Get<JwtConfigureModel>();
JwtConfiguration.ConfigureServices(builder.Services, descriptor);

builder.Services.AddCors(opt =>
{
    opt.AddDefaultPolicy(cors =>
    {
        cors.AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader();
    });
});

builder.Services.AddAuthorization();

SwaggerConfiguration.ConfigureSwaggerService(builder.Services);

ConfigureDependencyInjection.ConfigureDI(builder.Services, builder.Configuration);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "YourSoundCompnay"));
}

app.UseCors();

app.UseAuthentication();
app.UseAuthorization();


app.MapControllers();

app.Run();
