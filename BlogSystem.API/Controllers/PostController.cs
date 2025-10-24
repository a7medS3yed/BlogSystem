using BlogSystem.Application.Abstraction.Contracts.ServiceManager;
using BlogSystem.Application.Abstraction.Dtos.Post;
using BlogSystem.Shared._ٌApi_Response;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

[Authorize]
[Route("api/[controller]")]
[ApiController]
public class PostController(IServiceMangar serviceManager) : ControllerBase
{
    private string? GetUserId() =>
        User.FindFirstValue(ClaimTypes.NameIdentifier);

    [HttpGet]
    [AllowAnonymous]
    public async Task<ActionResult<ApiResponse<PagedResult<PostDto>>>> GetAll([FromQuery] PostQueryParameters parameters)
    {
        var response = await serviceManager.blogPostService.GetAllAsync(parameters);
        return Ok(response);
    }

    [HttpGet("{id}")]
    [AllowAnonymous]
    public async Task<ActionResult<ApiResponse<PostDto>>> GetById(int id)
    {
        var response = await serviceManager.blogPostService.GetByIdAsync(id);
        return Ok(response);
    }

    [HttpPost]
    [Authorize(Roles = "Admin,Editor")]
    public async Task<ActionResult<ApiResponse<PostDto>>> Create([FromBody] PostCreateDto dto)
    {
        var userId = GetUserId();
        if (userId is null) return Unauthorized(ApiResponse<PostDto>.FailResponse("Unauthorized"));

        var response = await serviceManager.blogPostService.CreateAsync(dto, userId);
        return Ok(response);
    }

    [HttpPut("{id}")]
    [Authorize(Roles = "Admin,Editor")]
    public async Task<ActionResult<ApiResponse<PostDto>>> Update(int id, [FromBody] PostUpdateDto dto)
    {
        var userId = GetUserId();
        if (userId is null) return Unauthorized(ApiResponse<PostDto>.FailResponse("Unauthorized"));

        dto.Id = id;
        var isAdminOrEditor = User.IsInRole("Admin") || User.IsInRole("Editor");

        var response = await serviceManager.blogPostService.UpdateAsync(dto, userId, isAdminOrEditor);
        return Ok(response);
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult<ApiResponse<bool>>> Delete(int id)
    {
        var userId = GetUserId();
        if (userId is null) return Unauthorized(ApiResponse<bool>.FailResponse("Unauthorized"));

        var isAdmin = User.IsInRole("Admin");
        var response = await serviceManager.blogPostService.DeleteAsync(id, userId, isAdmin);
        return Ok(response);
    }

    [HttpGet("my-posts")]
    [Authorize]
    public async Task<ActionResult<ApiResponse<IEnumerable<PostDto>>>> GetMyPosts()
    {
        var userId = GetUserId();
        var response = await serviceManager.blogPostService.GetPostsByUserAsync(userId);
        return Ok(response);
    }
}
