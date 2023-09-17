using Microsoft.AspNetCore.Mvc;
using BasicLayeredService.API.Contracts.Persistence;
using BasicLayeredService.API.DTOs;
using BasicLayeredService.API.Domain;
using BasicLayeredService.API.Exceptions;

namespace ECommerce.APIs.ItemAPI.Controllers;

[ApiController]
public class BaseItemController <TModel> : ControllerBase where TModel : BaseItem 
{
    protected readonly IBaseItemRepo<TModel> _repo;
    private readonly ILogger<BaseItemController<TModel>> _logger;

    public BaseItemController(IBaseItemRepo<TModel> repo, ILogger<BaseItemController<TModel>> logger)
    {
        _repo = repo;
        _logger = logger;
    }

    [HttpGet]
    public async Task<ResponseDto<List<TModel>>> GetLatest()
    {
        var items = await _repo.GetLatestAsync(10);

        if (items.Count == 0)
            throw new NotFoundException($"No post found");
        else
            return new ResponseDto<List<TModel>>
            {
                IsSuccess = true,
                Data = items,
                ResultCode = "200",
            };
    }

    [HttpGet]
    [Route("id/{id}")]
    public async Task<ResponseDto<TModel>> GetByIdAsync(int id)
    {   
        var item = await _repo.GetByIdAsync(id);

        if (item == null)
            throw new NotFoundException(typeof(TModel).Name,id);
        else
            return new ResponseDto<TModel>
            {
                IsSuccess = true,
                Data = item,
                ResultCode = "200",
            };
    }

    [HttpGet]
    [Route("author/{author}")]
    public async Task<ResponseDto<List<TModel>>> GetByAuthorAsync(string author)//
    {
        var items = await _repo.GetByAuthorAsync(author);

        if (items.Count == 0)
            throw new NotFoundException($"No {typeof(TModel).Name} found for the author {author}");
        else
            return new ResponseDto<List<TModel>>
            {
                IsSuccess = true,
                Data = items,
                ResultCode = "200",
            };
    }

    [HttpPost]
    [Route("create")]
    [Route("id/{id}/copy")]
    public async Task<ResponseDto<TModel>> CreateAsync(TModel item)
    {
        await _repo.CreateAsync(item);

        var response = new ResponseDto<TModel>
        {
            IsSuccess = true,
            Data = item,
            ResultCode = "200",
            Message = "Item created successfully!",
        };

        _logger.LogInformation("{@response}", response);

        return response;
    }

    [HttpDelete]
    [Route("id/{id}")]
    public async Task<ResponseDto<TModel>> DeleteAsync(int id,
        [FromServices] IHttpContextAccessor contextAccessor)
    {
        if (id == 0)
            throw new BadRequestException("Invalid input for Id");

        var item = await _repo.GetByIdAsync(id);
        if (item == null)
            throw new NotFoundException(typeof(TModel).Name, id);

        if (item.Author != contextAccessor.HttpContext.User.Claims.
                    FirstOrDefault(c => c.Type == "preferred_username").Value)
            throw new NotAllowedException($"Authors are different");

        await _repo.DeleteAsync(item);

        var response = new ResponseDto<TModel>
        {
            IsSuccess = true,
            Data = item,
            ResultCode = "200",
            Message = "Post deleted successfully!",
        };

        _logger.LogInformation("{@response}", response);

        return response;
    }

    [HttpPut]
    [Route("id/{id}/update")]
    public async Task<ResponseDto<TModel>> UpdateAsync(int id, [FromBody]TModel item, 
        [FromServices]IHttpContextAccessor contextAccessor)
    {
        if (item == null || item.Id == 0 || id != item.Id)
            throw new BadRequestException("No input or invalid input for Id");

        var model = await _repo.GetByIdAsync(item.Id);
        if (model == null)
            throw new NotFoundException(nameof(Post), item.Id);

        if (item.Author != model.Author)
            throw new NotAllowedException($"Authors are different");

        if (item.Author != contextAccessor.HttpContext.User.Claims.
                    FirstOrDefault(c => c.Type == "preferred_username").Value)
            throw new NotAllowedException($"Authors are different");

        //var existing = await _repo.GetByNameAsync(dto.Name);
        //if (existing != null && model.Id != existing.Id)
        //    throw new BadRequestException($"{typeof(TModel).Name} with name = {command._dto.Name} already exists!");

        model.Title = item.Title;
        model.Body = item.Body;
        model.DateModified = DateTime.UtcNow;

        await _repo.UpdateAsync(model);

        var response = new ResponseDto<TModel>
        {
            IsSuccess = true,
            Data = item,
            ResultCode = "200",
            Message = "Post updated successfully!",
        };

        _logger.LogInformation("{@response}", response);

        return response;
    }
}
