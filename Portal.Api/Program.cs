using Microsoft.EntityFrameworkCore;
using Mapster;
using MapsterMapper;
using FluentValidation;
using FluentValidation.AspNetCore;
using Portal.Api.Models;
using Portal.Api.Mappings;
using Portal.Api.Validators;

var builder = WebApplication.CreateBuilder(args);

// ✅ Controllers + FluentValidation
builder.Services.AddControllers()
    .AddFluentValidation(fv =>
    {
        fv.RegisterValidatorsFromAssemblyContaining<UcHarfKelimeValidator>();
        fv.DisableDataAnnotationsValidation = true;
    });

// ✅ EF Core PostgreSQL
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// ✅ Mapster
var config = TypeAdapterConfig.GlobalSettings;
config.Scan(typeof(MapsterConfig).Assembly);
builder.Services.AddSingleton(config);
builder.Services.AddScoped<IMapper, ServiceMapper>();

// ✅ Swagger
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
