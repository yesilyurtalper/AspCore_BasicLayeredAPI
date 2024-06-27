using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using BasicLayeredService.API.DTOs;
using BasicLayeredService.API.Models;

namespace BasicLayeredService.API.Filters;

[AttributeUsage(AttributeTargets.Method)]
public class ResultLoggerFilter : Attribute, IAsyncResultFilter
{
    private readonly ILogger<ResultLoggerFilter> _logger;

    public ResultLoggerFilter(ILogger<ResultLoggerFilter> logger)
    {
        _logger = logger;
    }

    public async Task OnResultExecutionAsync(ResultExecutingContext context, ResultExecutionDelegate next)
    {
        await next();

        object? result = "no content";
        if (context.Result is ObjectResult objectResult)
            result = objectResult.Value;

        var log = new
        {
            RequestMethod = context.HttpContext.Request.Method,
            RequestPath = context.HttpContext.Request.Path.Value,
            ResponseCode = context.HttpContext.Response.StatusCode,
            Result = result,
            User = context.HttpContext?.User?.Claims?.
                FirstOrDefault(c => c.Type == "preferred_username")?.Value,
            Date = DateTime.Now,
        };

        _logger.LogInformation("{@log}", log);
    }
}

