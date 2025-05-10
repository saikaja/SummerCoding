using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using SY.OnlineApp.Data;
using SY.OnlineApp.Repos.Repositories;
using SY.OnlineApp.Services.InteractiveServices;
using System.Text.Json.Serialization;

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

builder.Services.AddHttpClient<IInteractiveITypeInformationService, InteractiveTypeInformationService>(client =>
{
    client.BaseAddress = new Uri("https://localhost:7200/");
});

// Register BusinessDbContext and link migration assembly
builder.Services.AddDbContext<BusinessDbContext>(options =>
{
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DbConnection"),
        b => b.MigrationsAssembly("SY.OnlineApp.Data")
    );
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
