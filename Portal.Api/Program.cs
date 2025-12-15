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
builder.Services.AddAuthorization();

// EF Core PostgreSQL
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

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

// 🔴 ENV KONTROLÜ KALDIRILDI
app.UseSwagger();
app.UseSwaggerUI();

// ⚠️ Render HTTPS’yi dışarıda yapar
app.UseHttpsRedirection();  // istersen kapatabilirsin

app.UseAuthorization();
app.MapControllers();
app.Run();
