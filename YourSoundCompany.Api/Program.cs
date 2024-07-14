
using System.Text.Json.Serialization;
using System.Text.Json;
using YourSoundCompany.Common;
using YourSoundCompnay.Api;
using YourSoundCompnay.Api.Filter;
using YourSoundCompnay.Business.Model.Base;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.SetBasePath(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location));
builder.Configuration.AddJsonFile($"appsettings.json", false, false);
builder.Configuration.AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", true, false);

builder.Services.AddControllers(opt =>
{
    opt.Filters.Add<SessionFilter>();
})
    .AddJsonOptions(opt =>
{
    opt.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
    opt.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
    opt.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    opt.JsonSerializerOptions.AllowTrailingCommas = true;
    opt.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
}); 
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

builder.Services.AddRouting(options =>
{
    options.LowercaseUrls = true;
});

SwaggerConfiguration.ConfigureSwaggerService(builder.Services);

ConfigureDependencyInjection.ConfigureDI(builder.Services, builder.Configuration);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

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
