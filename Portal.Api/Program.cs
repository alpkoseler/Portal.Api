using Mapster;
using MapsterMapper;
using Microsoft.EntityFrameworkCore;
using Portal.Api.Mappings;
using Portal.Api.Models;
using Portal.Api.Services.Implementations;
using Portal.Api.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Controllers
builder.Services.AddControllers();
builder.Services.AddAuthorization(); // Auth will be added when secured endpoints are introduced

// EF Core PostgreSQL
var connectionString =
    builder.Configuration.GetConnectionString("DefaultConnection")
    ?? throw new InvalidOperationException("Database connection string is not configured.");

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(connectionString));

// Services
builder.Services.AddScoped<IKelimeService, KelimeService>();
builder.Services.AddScoped<IKelimeKategoriService, KelimeKategoriService>();
builder.Services.AddScoped<IKategoriServices, KategoriService>();

// Mapster
var config = TypeAdapterConfig.GlobalSettings;
config.Scan(typeof(MapsterConfig).Assembly);
builder.Services.AddSingleton(config);
builder.Services.AddScoped<IMapper, ServiceMapper>();

// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "Portal API v1");
});

app.UseHttpsRedirection();

app.UseAuthorization();
app.MapControllers();
app.Run();
