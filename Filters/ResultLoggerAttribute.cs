using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using BasicLayeredAPI.API.DTOs;
using BasicLayeredAPI.API.Models;

namespace BasicLayeredAPI.API.Filters;

[AttributeUsage(AttributeTargets.Method)]
public class ResultLoggerAttribute : Attribute, IAsyncResultFilter
{
    private readonly ILogger<ResultLoggerAttribute> _logger;

    public ResultLoggerAttribute(ILogger<ResultLoggerAttribute> logger)
    {
        _logger = logger;
    }

    public async Task OnResultExecutionAsync(ResultExecutingContext context, ResultExecutionDelegate next)
    {
        var result = (ResponseDtoBase)((context.Result as ObjectResult)?.Value);
        if (result != null && result.IsSuccess)
        {
            result.ResultCode = context.HttpContext.Response.StatusCode.ToString();

            var log = new CustomLog
            {
                Method = context.HttpContext.Request.Method,
                Path = context.HttpContext.Request.Path.Value,
                Result = result,
                User = context.HttpContext.User.Claims.FirstOrDefault(c => c.Type == "preferred_username")?.Value,
            };

            _logger.LogInformation("{@log}",log);
        }
        
        await next();
    }
}

