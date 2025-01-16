using MediatR;
using Microsoft.EntityFrameworkCore;
using SolucionesResidenciales.Application.Common.Behaviors;
using SolucionesResidenciales.Application.Common.Interfaces;
using SolucionesResidenciales.Application;
using System.Text.Json.Serialization;
using SolucionesRecidencialesApi.Middleware;
using Microsoft.Extensions.Configuration;
using SolucionesResidenciales.Application.Common.Mappings;
using SolucionesResidenciales.Infrastructure.Persistence;
using SolucionesResidenciales.Infrastructure.Repository;



var builder = WebApplication.CreateBuilder(args);

// Configurar servicios
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

builder.Services.AddAutoMapper(typeof(MappingProfile).Assembly);


// Add services to the container.
// Agregar controladores y configurar opciones de serialización JSON
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles; // Evita ciclos en las referencias
        options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull; // Ignora propiedades nulas
    });


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configurar CORS para permitir cualquier origen, método y encabezado
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", builder =>
    {
        builder.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader();
    });
});



// Registrar servicios de la aplicación (mediadores, validadores, etc.)
//builder.Services.AddApplication();
//builder.Services.AddInfrastructure(builder.Configuration);

// Configurar validación automática usando FluentValidation
//builder.Services.AddValidatorsFromAssembly(typeof(IApplicationDbContext).Assembly);
//builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// En el pipeline de la aplicación
app.UseCors("AllowAll");

app.UseHttpsRedirection();

app.UseAuthorization();

// ----------------------------------------

app.UseMiddleware<ExceptionHandlingMiddleware>();

app.MapControllers();

app.Run();
