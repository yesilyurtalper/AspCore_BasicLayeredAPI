using Microsoft.AspNetCore.Mvc;
using BasicLayeredService.API;
using BasicLayeredService.API.Contracts.Persistence;
using BasicLayeredService.API.DTOs;
using BasicLayeredService.API.Domain;
using Microsoft.AspNetCore.Components.Forms;
using BasicLayeredService.API.Exceptions;

namespace BasicLayeredService.API.Controllers;

[Route("api/posts")]
public class PostController : BaseItemController<Post,PostQueryDto>
{

    public PostController(IBaseItemRepo<Post, PostQueryDto> repo, 
        ILogger<PostController> logger) : base(repo, logger)
    {
    }
}
