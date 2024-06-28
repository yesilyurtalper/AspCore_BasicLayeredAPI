using Microsoft.AspNetCore.Mvc;
using BasicLayeredService.API.Contracts.Persistence;
using BasicLayeredService.API.DTOs;
using BasicLayeredService.API.Domain;
using BasicLayeredService.API.Exceptions;
using BasicLayeredService.API.Filters;

namespace BasicLayeredService.API.Controllers;

[ApiController]
public class BaseItemController<TModel, TQuery> : ControllerBase
        where TModel : BaseItem where TQuery : BaseQueryDto
{
    protected readonly IBaseItemRepo<TModel, TQuery> _repo;
    private readonly ILogger<BaseItemController<TModel, TQuery>> _logger;

    public BaseItemController(IBaseItemRepo<TModel, TQuery> repo,
        ILogger<BaseItemController<TModel, TQuery>> logger)
    {
        _repo = repo;
        _logger = logger;
    }

    [HttpGet]
    public async Task<ActionResult<QueryResult<TModel>>> QueryAsync([FromQuery] TQuery query)
    {
        var result = await _repo.QueryAsync(query);
        return Ok(result);
    }

    [HttpGet]
    [Route("{id}")]
    public async Task<ActionResult<TModel>> GetByIdAsync(Guid id)
    {
        var item = await _repo.GetByIdAsNoTrackingAsync(id);

        if (item == null)
            throw new NotFoundException(typeof(TModel).Name, id);
        else
            return Ok(item);
    }

    [HttpPost]
    [ServiceFilter(typeof(ResultLoggerFilter))]
    public async Task<ActionResult<TModel>> CreateAsync(TModel item)
    {
        await _repo.CreateAsync(item);
        return Ok(item);
    }

    [HttpDelete]
    [Route("{id}")]
    [ServiceFilter(typeof(ResultLoggerFilter))]
    public async Task<ActionResult> DeleteAsync(Guid id,
        [FromServices] IHttpContextAccessor contextAccessor)
    {
        var item = await _repo.GetByIdAsync(id) ??
            throw new NotFoundException(typeof(TModel).Name, id);

        if (item.Author != contextAccessor.HttpContext?.User?.Claims?.
                    FirstOrDefault(c => c.Type == "preferred_username")?.Value)
            throw new NotAllowedException($"Authors are different");

        await _repo.DeleteAsync(item);

        return NoContent();
    }

    [HttpPut]
    [Route("{id}")]
    [ServiceFilter(typeof(ResultLoggerFilter))]
    public async Task<ActionResult> UpdateAsync(Guid id, [FromBody] TModel item,
        [FromServices] IHttpContextAccessor contextAccessor)
    {
        if (item == null || id != item.Id)
            throw new BadRequestException("No input or invalid input for Id");

        var model = await _repo.GetByIdAsNoTrackingAsync(item.Id) ??
            throw new NotFoundException(nameof(BaseItem), item.Id);

        if (item.Author != model.Author)
            throw new NotAllowedException($"Authors are different");

        if (item.Author != contextAccessor?.HttpContext?.User?.Claims?.
                    FirstOrDefault(c => c.Type == "preferred_username")?.Value)
            throw new NotAllowedException($"Authors are different");

        item.DateCreated = model.DateCreated;
        await _repo.UpdateAsync(item);

        return NoContent();
    }
}
