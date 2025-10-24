using System.Security.Claims;
using BlogSystem.Application.Abstraction.Contracts.ServiceManager;
using BlogSystem.Application.Abstraction.Dtos.Auth;
using BlogSystem.Shared._ٌApi_Response;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BlogSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ProfileController(IServiceMangar serviceMangar) : ControllerBase
    {
        private string? GetUserId() =>
            User.FindFirstValue(ClaimTypes.NameIdentifier);
        [HttpGet]
        public async Task<ActionResult<ApiResponse<ProfileResponseDto>>> GetProfile()
        {
            var userId = GetUserId();
            if (userId is null) return Unauthorized(ApiResponse<ProfileResponseDto>.FailResponse("Unauthorized"));
            var response = await serviceMangar.ProfileService.GetProfileAsync(userId);
            return Ok(response);
        }
    }
}
