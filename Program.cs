using MediatR;
using Microsoft.EntityFrameworkCore;
using SolucionesResidenciales.Application.Common.Behaviors;
using SolucionesResidenciales.Application.Common.Interfaces;
using SolucionesResidenciales.Application;
using System.Text.Json.Serialization;
using SolucionesRecidencialesApi.Middleware;



var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<SolucionesResidenciales.Infrastructure.Persistence.ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

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

app.UseHttpsRedirection();

app.UseAuthorization();

// ----------------------------------------

app.UseMiddleware<ExceptionHandlingMiddleware>();

app.MapControllers();

app.Run();
