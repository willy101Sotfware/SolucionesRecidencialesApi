

using SolucionesResidenciales.Application.Common.Exceptions;
using System.Text.Json;

namespace SolucionesRecidencialesApi.Middleware;

public class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionHandlingMiddleware> _logger;

    public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(context, ex);
        }
    }

    private async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        var response = context.Response;
        response.ContentType = "application/json";

        object result;

        switch (exception)
        {
            case ValidationException validationException:
                response.StatusCode = StatusCodes.Status400BadRequest;
                result = new { errors = validationException.Errors };
                break;
            case NotFoundException notFoundException:
                response.StatusCode = StatusCodes.Status404NotFound;
                result = new { message = exception.Message };
                break;
            default:
                _logger.LogError(exception, "Error no manejado");
                response.StatusCode = StatusCodes.Status500InternalServerError;
                result = new { message = "Ha ocurrido un error interno del servidor." };
                break;
        }

        var jsonResult = JsonSerializer.Serialize(result);
        await response.WriteAsync(jsonResult);
    }
}
