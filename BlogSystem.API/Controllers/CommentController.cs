using System.Security.Claims;
using BlogSystem.Application.Abstraction.Contracts.ServiceManager;
using BlogSystem.Application.Abstraction.Contracts.Services.Comment;
using BlogSystem.Application.Abstraction.Dtos.Comment;
using BlogSystem.Shared._ٌApi_Response;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BlogSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentController(IServiceMangar commentService) : ControllerBase
    {
        // Helper: Get current userId from JWT
        private string GetUserId() =>
            User.FindFirstValue(ClaimTypes.NameIdentifier)!;

        [HttpGet("{postId}")] // GET: api/Comment/3
        public async Task<ActionResult<ApiResponse<IEnumerable<CommentDto>>>> GetByPostId(int postId)
        {
            var response = await commentService.CommentService.GetByPostIdAsync(postId);
            return Ok(response);
        }

        [HttpPost] // POST: api/Comment
        [Authorize]
        public async Task<ActionResult<ApiResponse<CommentDto>>> Add(CommentCreateDto dto)
        {
            var userId = GetUserId();
            var response = await commentService.CommentService.AddAsync(dto, userId);
            return Ok(response);
        }

        [HttpDelete("{id}")] // DELETE: api/Comment/5
        [Authorize]
        public async Task<ActionResult<ApiResponse<bool>>> Delete(int id)
        {
            var userId = GetUserId();
            var isAdmin = User.IsInRole("Admin");
            var response = await commentService.CommentService.DeleteAsync(id, userId, isAdmin);
            return Ok(response);
        }

    }
}
