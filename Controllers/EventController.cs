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

    [HttpPost]
    public async Task<ResponseDto<List<Event>>> QueryAsync(QueryDto dto)
    {
        var events = await _repo.QueryAsync(dto);

        if (events.Count == 0)
            throw new NotFoundException($"No post found");
        else
            return new ResponseDto<List<Event>>
            {
                IsSuccess = true,
                Data = events,
                ResultCode = "200",
            };
    }
}
