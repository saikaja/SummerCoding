using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using SY.OnlineApp.Data;
using SY.OnlineApp.Repos.Repositories;
using SY.OnlineApp.Services.InteractiveServices;
using SY.OnlineApp.Services.BusinessServices;
using System.Text.Json.Serialization;
using SY.OnlineApp.Services.Integrated_Type_Services;
using SY.OnlineApp.Services.Integrated_Status_Services;
using SY.OnlineApp.Data.Repositories;
using SY.OnlineApp.Services.Business_Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
});

builder.Services.AddScoped<ITypeUtilRepo, TypeUtilRepo>();
builder.Services.AddScoped<ITypeInformationRepo, TypeInformationRepo>();
builder.Services.AddScoped<IBusinessRepo, BusinessRepo>();
builder.Services.AddScoped<IInteractiveTypeUtilService, InteractiveTypeUtilService>();
builder.Services.AddScoped<IInteractiveITypeInformationService, InteractiveTypeInformationService>();
builder.Services.AddScoped<IIntegratedTypeRepo, IntegratedTypeRepo>();
builder.Services.AddScoped<IIntegratedTypeService, IntegratedTypeService>();
builder.Services.AddScoped<IIntegratedStatusRepo, IntegratedStatusRepo>();
builder.Services.AddScoped<IIntegratedStatusService, IntegratedStatusService>();
builder.Services.AddScoped<ILiabilityRepo, LiabilityRepo>();
builder.Services.AddScoped<ILiabilityService, LiabilityService>();

builder.Services.AddHttpClient<ILiabilityService, LiabilityService>(client =>
{
    client.BaseAddress = new Uri("https://localhost:7200/");
});

builder.Services.AddHttpClient<IBusinessTypeInformationService, BusinessTypeInformationService>(client =>
{
    client.BaseAddress = new Uri("https://localhost:7200/");
});

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

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();  
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("AllowAngular");

app.UseAuthorization();

app.MapControllers();

app.Run();
