using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using SY.OnlineApp.Data;
using SY.OnlineApp.Data.Entities;
using SY.OnlineApp.Repos.Repositories;
using SY.OnlineApp.Repos.Repositories.Interfaces;
using SY.OnlineApp.Services.BusinessServices;
using SY.OnlineApp.Services.InteractiveServices;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DbConnection"), 
        b => b.MigrationsAssembly("SY.OnlineApp.InteractiveAPI"));
    
}
);

builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
});

builder.Services.AddScoped<ITypeUtilRepo, TypeUtilRepo>();
builder.Services.AddScoped<ITypeInformationRepo, TypeInformationRepo>();
builder.Services.AddScoped<IInteractiveTypeUtilService, InteractiveTypeUtilService>();
builder.Services.AddScoped<IInteractiveITypeInformationService, InteractiveTypeInformationService>();
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
