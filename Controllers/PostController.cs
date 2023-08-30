using Microsoft.AspNetCore.Mvc;
using BasicLayeredService.API;
using BasicLayeredService.API.Contracts.Persistence;
using BasicLayeredService.API.DTOs;
using BasicLayeredService.API.Domain;
using Microsoft.AspNetCore.Components.Forms;

namespace ECommerce.APIs.ItemAPI.Controllers;

[Route("api/posts")]
public class PostController : BaseItemController<Post>
{

    public PostController(IBaseItemRepo<Post> repo, ILogger<PostController> logger) : base(repo, logger)
    {
    }

}
