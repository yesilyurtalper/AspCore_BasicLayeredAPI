using Microsoft.AspNetCore.Mvc;
using BasicLayeredService.API.Domain;
using BasicLayeredService.API.Contracts.Persistence;
using BasicLayeredService.API.DTOs;
using BasicLayeredService.API.Exceptions;
using Microsoft.AspNetCore.Authorization;

namespace BasicLayeredService.API.Controllers;

[Route("api/events")]
public class EventController : BaseItemController<Event,EventQueryDto>
{
    public EventController(IBaseItemRepo<Event, EventQueryDto> repo,
        ILogger<EventController> logger) : base(repo, logger)
    {
    }
}
