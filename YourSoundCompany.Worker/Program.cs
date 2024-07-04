using Hangfire;
using Hangfire.PostgreSql;
using Microsoft.Extensions.Configuration;
using YourSoundCompany.Worker;
using YourSoundCompany.Worker.Jobs;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

ConfigureDependencyInjection.ConfigureDI(builder.Services, builder.Configuration);

string connectionString = builder.Configuration.GetConnectionString("HangFire");

builder.Services.AddHangfire(config =>
{
    config.SetDataCompatibilityLevel(CompatibilityLevel.Version_180);
    config.UseSimpleAssemblyNameTypeSerializer();
    config.UseRecommendedSerializerSettings();
    config.UsePostgreSqlStorage(connectionString);

});

builder.Services.AddHangfireServer();

var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHangfireDashboard("/dashboard");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

RecurringJob.AddOrUpdate<UserSyncWorkerEnqueue>("UserSyncWorkerEnqueue", job => job.Execute(), Cron.Hourly);


app.Run();

