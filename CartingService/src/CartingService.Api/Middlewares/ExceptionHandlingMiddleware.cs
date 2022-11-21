using CartingService.Core.Exceptions;
using CartingService.Infrastructure.Exceptions;

namespace CartingService.Api.Middlewares;

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
        catch (Exception exception)
        {
            await HandleException(context, exception);
        }
    }

    private Task HandleException(HttpContext context, Exception exception)
    {
        var message = exception.Message;
        _logger.LogError(exception, "{Message}", message);

        var statusCode = exception switch
        {
            // Core exceptions
            CartNotFoundException => StatusCodes.Status404NotFound,
            RemoveNonAddedItemException => StatusCodes.Status404NotFound,
            ItemAlreadyAddedException => StatusCodes.Status400BadRequest,
            NonValidItemPriceException => StatusCodes.Status400BadRequest,
            NonValidItemQuantityException => StatusCodes.Status400BadRequest,
            
            // Infrastructure exception
            DatabaseException => StatusCodes.Status500InternalServerError,
            
            _ => StatusCodes.Status500InternalServerError
        };
        
        context.Response.StatusCode = statusCode;
        context.Response.ContentType = "application/json";

        return context.Response.WriteAsJsonAsync(value: new { info = message });
    }
}