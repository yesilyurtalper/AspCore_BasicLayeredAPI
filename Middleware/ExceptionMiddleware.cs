
using BasicLayeredService.API.DTOs;
using BasicLayeredService.API.Exceptions;
using System.Net;

namespace BasicLayeredService.API.Middleware;

public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionMiddleware> _logger;

    public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
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
        catch (Exception ex)
        {
            await HandleExceptionAsync(httpContext, ex);
        }
    }

    private async Task HandleExceptionAsync(HttpContext httpContext, Exception ex)
    {
        ErrorDto problem = new();

        switch (ex)
        {
            case BadRequestException:
                httpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                problem.ErrorMessages = new List<string> { ex.Message };
                break;
            case NotFoundException:
                httpContext.Response.StatusCode = (int)HttpStatusCode.NotFound;
                problem.ErrorMessages = new List<string> { ex.Message };
                break;
            case NotAllowedException:
                httpContext.Response.StatusCode = (int)HttpStatusCode.Forbidden;
                problem.ErrorMessages = new List<string> { ex.Message };
                break;
            default:
                httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                problem.ErrorMessages = new List<string> { ex.ToString() };
                break;
        }

        problem.ResultCode = httpContext.Response.StatusCode.ToString();
        problem.Message = ex.Message;

        _logger.LogError("{@problem}",problem);

        await httpContext.Response.WriteAsJsonAsync(problem);
    }
}
