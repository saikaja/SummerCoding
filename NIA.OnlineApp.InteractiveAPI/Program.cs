using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using NIA.OnlineApp.Data;
using NIA.OnlineApp.Data.Entities;
using NIA.OnlineApp.Data.Repositories;
using NIA.OnlineApp.InteractiveAPI.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DbConnection"), 
        b => b.MigrationsAssembly("NIA.OnlineApp.InteractiveAPI"));
    
}
);

builder.Services.AddScoped<ITypeUtilRepo, TypeUtilRepo>();
builder.Services.AddScoped<ITypeInformationRepo, TypeInformationRepo>();
builder.Services.AddScoped<ITypeUtilService, TypeUtilService>();
builder.Services.AddScoped<ITypeInformationService, TypeInformationService>();
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
