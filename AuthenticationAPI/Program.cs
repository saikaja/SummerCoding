using Microsoft.EntityFrameworkCore;
using SY.OnlineApp.Data;
using SY.OnlineApp.Repos.Repositories.Interfaces;
using SY.OnlineApp.Repos.Repositories;
using SY.OnlineApp.Services.Interfaces;
using SY.OnlineApp.Services.Services;
using SY.OnlineApp.Services.Business_Services;
using SY.OnlineApp.Services.Business_Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add controllers & Swagger
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Load API Base URL
var apiBaseUrl = builder.Configuration["ApiSettings:BaseUrl"];
if (string.IsNullOrEmpty(apiBaseUrl))
    throw new InvalidOperationException("API Base URL is not configured.");

// Add Repositories
builder.Services.AddScoped<IRegisterRepo, RegisterRepo>();
builder.Services.AddScoped<ILastLoginRepo, LastLoginRepo>();
builder.Services.AddScoped<IOneTimePassCodeRepo, OneTimePassCodeRepo>();

// Add Services
builder.Services.AddScoped<IRegisterService, RegisterService>();
builder.Services.AddScoped<ILastLoginService, LastLoginService>();
builder.Services.AddScoped<IOneTimePassCodeService, OneTimePassCodeService>();

// Add DbContexts with Migration Assembly Binding
builder.Services.AddDbContext<BusinessDbContext>(options =>
{
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("BusinessDbConnection"),
        b => b.MigrationsAssembly("SY.OnlineApp.Data")
    );
});

builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("IntegratedDbConnection"),
        b => b.MigrationsAssembly("SY.OnlineApp.InteractiveAPI")
    );
});

// Add CORS Policy for Angular or other clients
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngular",
        policy => policy.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader());
});

var app = builder.Build();

// Configure HTTP Pipeline
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
