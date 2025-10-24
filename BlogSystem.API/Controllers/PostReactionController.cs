using BlogSystem.Application.Abstraction.Contracts.ServiceManager;
using BlogSystem.Domain.Entities.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BlogSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostReactionController(IServiceMangar serviceMangar, UserManager<ApplicationUser> userManager) : ControllerBase
    {
        [HttpPost("{postId}/like")]
        [Authorize]
        public async Task<IActionResult> LikePost(int postId)
        {
            var userId = userManager.GetUserId(User);
            var response = await serviceMangar.PostReactionService.LikePostAsync(postId, userId);
            return Ok(response);
        }

        [HttpPost("{postId}/dislike")]
        [Authorize]
        public async Task<IActionResult> DislikePost(int postId)
        {
            var userId = userManager.GetUserId(User);
            var response = await serviceMangar.PostReactionService.DisLikePostAsync(postId, userId);
            return Ok(response);
        }

        [HttpGet("{postId}/reactions")]
        public async Task<IActionResult> GetReactions(int postId)
        {
            var response = await serviceMangar.PostReactionService.GetReactionsAsync(postId);
            return Ok(response);
        }
    }
}
