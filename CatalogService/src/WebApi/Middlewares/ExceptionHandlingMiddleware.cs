using Application.Exceptions;
using Core.Exceptions;

namespace WebApi.Middlewares;

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
            NameTooLongException => StatusCodes.Status400BadRequest,
            NonValidItemAmountException => StatusCodes.Status400BadRequest,
            NonValidItemPriceException => StatusCodes.Status400BadRequest,
            
            // Application exceptions
            CategoryWithTheSameNameAlreadyExists => StatusCodes.Status400BadRequest,
            ItemCategoryNotFoundException => StatusCodes.Status400BadRequest,
            ItemWithTheSameNameAlreadyExists => StatusCodes.Status400BadRequest,
            ParentCategoryNotFoundException => StatusCodes.Status400BadRequest,
            CategoryNotFoundException => StatusCodes.Status404NotFound,
            ItemNotFoundException => StatusCodes.Status404NotFound,
            RootCategoryDeleteException => StatusCodes.Status400BadRequest,
            
            _ => StatusCodes.Status500InternalServerError
        };
        
        context.Response.StatusCode = statusCode;
        context.Response.ContentType = "text/plain";

        return context.Response.WriteAsync(message);
    }
}