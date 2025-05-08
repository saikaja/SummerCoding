using Microsoft.EntityFrameworkCore;
using System;
using NIA.OnlineApp.BusinessAPI.Services;
using NIA.OnlineApp.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddHttpClient<ITypeInformationService, TypeInformationService>(client =>
{
    client.BaseAddress = new Uri("https://localhost:7200/");
});

builder.Services.AddDbContext<BusinessDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DbConnection"),
        b => b.MigrationsAssembly("NIA.OnlineApp.Data"));
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
