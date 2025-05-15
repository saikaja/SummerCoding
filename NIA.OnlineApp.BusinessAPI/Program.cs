using Microsoft.EntityFrameworkCore;
using SY.OnlineApp.Data;
using SY.OnlineApp.Repos.Repositories;
using SY.OnlineApp.Repos.Repositories.Interfaces;
using SY.OnlineApp.Services.BusinessServices;
using SY.OnlineApp.Services.InteractiveServices;

// Create builder
var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Read Configuration Settings
var interactiveApiBaseUrl = builder.Configuration.GetValue<string>("ApiSettings:InteractiveApiBaseUrl");
var allowedOrigin = builder.Configuration.GetValue<string>("CorsSettings:AllowedOrigin");

// Register HTTP Client with Configured Base URL
builder.Services.AddHttpClient<IInteractiveITypeInformationService, InteractiveTypeInformationService>(client =>
{
    client.BaseAddress = new Uri(interactiveApiBaseUrl);
});

// Register DbContext with Configured Connection String
builder.Services.AddDbContext<BusinessDbContext>(options =>
{
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DbConnection"),
        b => b.MigrationsAssembly("SY.OnlineApp.Data")
    );
});

// Register Custom Database Logger
builder.Logging.AddProvider(new DatabaseLoggerProvider(builder.Services.BuildServiceProvider()));

// Register Repositories
builder.Services.AddScoped<IBusinessRepo, BusinessRepo>();

// Register Services
builder.Services.AddScoped<IBusinessTypeInformationService, BusinessTypeInformationService>();

// Configure CORS with Configured Allowed Origin
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngular", policy =>
    {
        policy.WithOrigins(allowedOrigin)
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

// Build app
var app = builder.Build();

// Middleware Pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowAngular");
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
