using Microsoft.EntityFrameworkCore;
using NIA.OnlineApp.BusinessAPI.Services;
using NIA.OnlineApp.Data;
using NIA.OnlineApp.Data.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Swagger/OpenAPI
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Register HTTP client to talk to InteractiveAPI
builder.Services.AddHttpClient<ITypeInformationService, TypeInformationService>(client =>
{
    client.BaseAddress = new Uri("https://localhost:7200/");
});

// Register BusinessDbContext and link migration assembly
builder.Services.AddDbContext<BusinessDbContext>(options =>
{
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DbConnection"),
        b => b.MigrationsAssembly("NIA.OnlineApp.Data")
    );
});

// Register Repositories
builder.Services.AddScoped<IBusinessRepo, BusinessRepo>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngular",
        policy => policy.WithOrigins("http://localhost:4200")
                        .AllowAnyMethod()
                        .AllowAnyHeader());
});

// Build app
var app = builder.Build();

// Middleware
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
