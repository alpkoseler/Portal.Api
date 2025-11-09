using Microsoft.EntityFrameworkCore;
using Mapster;
using MapsterMapper;
using Portal.Api.Models;
using Portal.Api.Mappings;
using Portal.Api.Services.Interfaces;
using Portal.Api.Services.Implementations;

var builder = WebApplication.CreateBuilder(args);

// Controllers
builder.Services.AddControllers();

builder.Services.AddAuthorization();

// EF Core PostgreSQL
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

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

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
