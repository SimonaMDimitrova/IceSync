using IceSync.Configurations;
using IceSync.Data;
using IceSync.ExternalAPIs;
using IceSync.Services;

using System.Text.Json.Serialization;

using Hangfire;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.GetSection(nameof(ConnectionStrings)).Bind(ConnectionStrings.Instance);
builder.Configuration.GetSection(nameof(UniversalLoaderCredentials)).Bind(UniversalLoaderCredentials.Instance);
builder.Configuration.GetSection(nameof(ExternalAPIs)).Bind(ExternalAPIs.Instance);

builder.Services.AddSignalR();

// Configure json
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.PropertyNamingPolicy = null; // Use PascalCase
        options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull; // Optional
    });

// Add DB
builder.Services.AddDbContext<IceSyncContext>(
    options => options.UseSqlServer(ConnectionStrings.Instance.IceSyncConnectionString));

// Add services to the container.
builder.Services.AddHttpClient();

builder.Services.AddSingleton<IUniversalLoaderClient, UniversalLoaderClient>();
builder.Services.AddTransient<IBackgroundJobService, BackgroundJobService>();
builder.Services.AddTransient<IWorkflowsService, WorkflowsService>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add Hangfire
builder.Services.AddHangfire(config => config
                .SetDataCompatibilityLevel(CompatibilityLevel.Version_180)
                .UseSimpleAssemblyNameTypeSerializer()
                .UseRecommendedSerializerSettings()
                .UseSqlServerStorage(ConnectionStrings.Instance.IceSyncConnectionString));

builder.Services.AddHangfireServer();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigin", policy =>
    {
        policy.WithOrigins("http://localhost:3000") // Specify your frontend's URL
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapControllers();

app.UseCors("AllowSpecificOrigin");

using (var serviceScope = app.Services.CreateScope())
{
    var dbContext = serviceScope.ServiceProvider.GetRequiredService<IceSyncContext>();
    dbContext.Database.Migrate();
}

// Add Hangfire jobs
using (var scope = app.Services.CreateScope())
{
    var recurringJobManager = scope.ServiceProvider.GetRequiredService<IRecurringJobManager>();
    var backgroundJobClient = scope.ServiceProvider.GetRequiredService<IBackgroundJobClient>();

    var jobService = scope.ServiceProvider.GetRequiredService<IBackgroundJobService>();
    backgroundJobClient.Enqueue(() => jobService.SyncWorkflowsAsync());

    recurringJobManager.AddOrUpdate("sync workflows", () => jobService.SyncWorkflowsAsync(), "*/30 * * * *");
}

app.Run();