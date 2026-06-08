using LearningApp.api.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace LearningApp.api.MiddleWare;

public class ErrorHandlingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ErrorHandlingMiddleware> _logger;
    
    public ErrorHandlingMiddleware(RequestDelegate next, ILogger<ErrorHandlingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }
    
    public async Task InvokeAsync(HttpContext httpContext)
    {
        try
        {
            await _next(httpContext);
        }
        catch (NotFoundException e)
        {
            await WriteProblem(httpContext, StatusCodes.Status404NotFound, e.Message);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Unhandled exception");
            
            await WriteProblem(httpContext, StatusCodes.Status500InternalServerError, "Internal Server Error");
        }
    }

    private static Task WriteProblem(HttpContext httpContext, int statusCode, string eMessage)
    {
        httpContext.Response.StatusCode = statusCode;
        
        return httpContext.Response.WriteAsJsonAsync(new ProblemDetails
        {
            Status = statusCode,
            Detail = eMessage
        });
    }
}