using Microsoft.AspNetCore.Mvc;
using BasicLayeredService.API;
using BasicLayeredService.API.Contracts.Persistence;
using BasicLayeredService.API.DTOs;
using BasicLayeredService.API.Domain;
using BasicLayeredService.API.Exceptions;
using Microsoft.AspNetCore.Authorization;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using Microsoft.Extensions.Hosting;

namespace ECommerce.APIs.ItemAPI.Controllers;

//[AllowAnonymous]
[Route("api/posts")]
[ApiController]
public class PostingAPIController 
{
    private readonly IPostRepo _repo;
    private readonly ILogger<PostingAPIController> _logger;

    public PostingAPIController(IPostRepo repo, ILogger<PostingAPIController> logger)
    {
        _repo = repo;
        _logger = logger;
    }

    [HttpGet]
    public async Task<ResponseDto<List<Post>>> GetLatest()
    {
        var posts = await _repo.GetLatestAsync(10);

        if (posts.Count == 0)
            throw new NotFoundException($"No post found");
        else
            return new ResponseDto<List<Post>>
            {
                IsSuccess = true,
                Data = posts,
                ResultCode = "200",
            };
    }

    [HttpGet]
    [Route("id/{id}")]
    public async Task<ResponseDto<Post>> GetByPostIdAsync(int id)
    {   
        var post = await _repo.GetByIdAsync(id);

        if (post == null)
            throw new NotFoundException("UserPost",id);
        else
            return new ResponseDto<Post>
            {
                IsSuccess = true,
                Data = post,
                ResultCode = "200",
            };
    }

    [HttpGet]
    [Route("author/{author}")]
    public async Task<ResponseDto<List<Post>>> GetByAuthorAsync(string author)//
    {
        var posts = await _repo.GetByAuthorAsync(author);

        if (posts.Count == 0)
            throw new NotFoundException($"No post found for the author {author}");
        else
            return new ResponseDto<List<Post>>
            {
                IsSuccess = true,
                Data = posts,
                ResultCode = "200",
            };
    }

    [HttpPost]
    [Route("create")]
    [Route("id/{id}/copy")]
    public async Task<ResponseDto<Post>> CreatePostAsync(Post post)
    {
        await _repo.CreateAsync(post);

        var response = new ResponseDto<Post>
        {
            IsSuccess = true,
            Data = post,
            ResultCode = "200",
            Message = "Post created successfully!",
        };

        _logger.LogInformation("{@response}", response);

        return response;
    }

    [HttpDelete]
    [Route("id/{id}")]
    public async Task<ResponseDto<Post>> DeletePostAsync(int id,
        [FromServices] IHttpContextAccessor contextAccessor)
    {
        if (id == 0)
            throw new BadRequestException("Invalid input for Id");

        var post = await _repo.GetByIdAsync(id);
        if (post == null)
            throw new NotFoundException(nameof(Post), id);

        if (post.Author != contextAccessor.HttpContext.User.Claims.
                    FirstOrDefault(c => c.Type == "preferred_username").Value)
            throw new NotAllowedException($"Authors are different");

        await _repo.DeleteAsync(post);

        var response = new ResponseDto<Post>
        {
            IsSuccess = true,
            Data = post,
            ResultCode = "200",
            Message = "Post deleted successfully!",
        };

        _logger.LogInformation("{@response}", response);

        return response;
    }

    [HttpPut]
    [Route("id/{id}/update")]
    public async Task<ResponseDto<Post>> UpdatePostAsync(int id, [FromBody]Post post, 
        [FromServices]IHttpContextAccessor contextAccessor)
    {
        if (post == null || post.Id == 0 || id != post.Id)
            throw new BadRequestException("No input or invalid input for Id");

        var model = await _repo.GetByIdAsync(post.Id);
        if (model == null)
            throw new NotFoundException(nameof(Post), post.Id);

        if (post.Author != model.Author)
            throw new NotAllowedException($"Authors are different");

        if (post.Author != contextAccessor.HttpContext.User.Claims.
                    FirstOrDefault(c => c.Type == "preferred_username").Value)
            throw new NotAllowedException($"Authors are different");

        //var existing = await _repo.GetByNameAsync(dto.Name);
        //if (existing != null && model.Id != existing.Id)
        //    throw new BadRequestException($"{typeof(TModel).Name} with name = {command._dto.Name} already exists!");

        model.Title = post.Title;
        model.Body = post.Body;
        model.DateModified = DateTime.UtcNow;

        await _repo.UpdateAsync(model);

        var response = new ResponseDto<Post>
        {
            IsSuccess = true,
            Data = post,
            ResultCode = "200",
            Message = "Post updated successfully!",
        };

        _logger.LogInformation("{@response}", response);

        return response;
    }
}
