using Microsoft.EntityFrameworkCore;
using SY.OnlineApp.Data;
using SY.OnlineApp.Repos.Repositories.Interfaces;
using SY.OnlineApp.Repos.Repositories;
using SY.OnlineApp.Services.Interfaces;
using SY.OnlineApp.Services.Services;
using SY.OnlineApp.Models.Dtos;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var apiBaseUrl = builder.Configuration["ApiSettings:BaseUrl"];
if (string.IsNullOrEmpty(apiBaseUrl))
    throw new InvalidOperationException("API Base URL is not configured.");

builder.Services.AddScoped<IRegisterService, RegisterService>();
builder.Services.AddScoped<IRegisterRepo, RegisterRepo>();

// Register BusinessDbContext and link migration assembly
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

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngular",
      policy => policy.AllowAnyOrigin()
                      .AllowAnyMethod()
                      .AllowAnyHeader());
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
