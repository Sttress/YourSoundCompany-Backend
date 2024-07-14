using Hangfire;
using Hangfire.MemoryStorage;
using System.Reflection;
using System.Text.Json.Serialization;
using System.Text.Json;
using YourSoundCompany.Common;
using YourSoundCompany.Worker.Jobs;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.SetBasePath(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location));
builder.Configuration.AddJsonFile($"appsettings.json", false, false);
builder.Configuration.AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", true, false);

builder.Services.AddControllers()
    .AddJsonOptions(opt =>
    {
        opt.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
        opt.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
        opt.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
        opt.JsonSerializerOptions.AllowTrailingCommas = true;
    }); 

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddHangfire(config =>
{
    config.SetDataCompatibilityLevel(CompatibilityLevel.Version_180);
    config.UseSimpleAssemblyNameTypeSerializer();
    config.UseRecommendedSerializerSettings();
    config.UseMemoryStorage();

});

builder.Services.AddHangfireServer();

ConfigureDependencyInjection.ConfigureDI(builder.Services, builder.Configuration);

var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHangfireDashboard("/dashboard");

app.UseHttpsRedirection();

app.MapControllers();

RecurringJob.AddOrUpdate<UserSyncWorkerEnqueue>("UserSyncWorkerEnqueue", job => job.Execute(), Cron.Hourly);


app.Run();

