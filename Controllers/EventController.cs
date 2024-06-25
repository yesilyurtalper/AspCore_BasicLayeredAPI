using Microsoft.AspNetCore.Mvc;
using BasicLayeredService.API.Domain;
using BasicLayeredService.API.Contracts.Persistence;
using BasicLayeredService.API.DTOs;
using BasicLayeredService.API.Exceptions;
using Microsoft.AspNetCore.Authorization;

namespace ECommerce.APIs.ItemAPI.Controllers;

[Route("api/events")]
public class EventController : BaseItemController<Event>
{
    private new readonly IEventRepo _repo;

    public EventController(IEventRepo repo, ILogger<EventController> logger) : base(repo, logger)
    {   
        _repo = repo;
    }

    [HttpGet]
    [Route("query")]
    public async Task<ResponseDto<QueryResult<List<Event>>>> QueryAsync([FromQuery] QueryDto dto)
    {
        var queryResult = await _repo.QueryAsync(dto);

        return new ResponseDto<QueryResult<List<Event>>>
        {
            IsSuccess = true,
            Data = queryResult,
            ResultCode = "200",
        };

    }
}
