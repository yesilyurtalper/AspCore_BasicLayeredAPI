
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
        HttpStatusCode statusCode = HttpStatusCode.InternalServerError;
        ResponseDto<string> problem = new();

        switch (ex)
        {
            case BadRequestException:
                statusCode = HttpStatusCode.BadRequest;
                break;
            case NotFoundException:
                statusCode = HttpStatusCode.NotFound;
                break;
        }

        httpContext.Response.StatusCode = (int)statusCode;
        problem.ResultCode = httpContext.Response.StatusCode.ToString();
        problem.ErrorMessages = new List<string> { ex.ToString() };
        problem.Message = ex.Message;

        _logger.LogError("{@problem}",problem);

        await httpContext.Response.WriteAsJsonAsync(problem);
    }
}
