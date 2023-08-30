using Microsoft.AspNetCore.Mvc;
using BasicLayeredService.API.Domain;
using BasicLayeredService.API.Contracts.Persistence;

namespace ECommerce.APIs.ItemAPI.Controllers;

[Route("api/events")]
public class EventController : BaseItemController<Event>
{

    public EventController(IBaseItemRepo<Event> repo, ILogger<EventController> logger) : base(repo, logger)
    {
    }
}
