using Microsoft.AspNetCore.Mvc;
using BasicLayeredService.API;
using BasicLayeredService.API.Contracts.Persistence;
using BasicLayeredService.API.DTOs;
using BasicLayeredService.API.Domain;
using BasicLayeredService.API.Exceptions;
using Microsoft.AspNetCore.Authorization;

namespace ECommerce.APIs.ItemAPI.Controllers;

[AllowAnonymous]
[Route("api/userposts")]
[ApiController]
public class PostingAPIController 
{
    private readonly IPostRepo _repo;

    public PostingAPIController(IPostRepo repo)
    {
        _repo = repo;   
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
    public async Task<ResponseDto<List<Post>>> GetByAuthorAsync(string author)
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
}
