using BlogSystem.Application.Abstraction.Contracts.ServiceManager;
using BlogSystem.Application.Abstraction.Dtos.Auth;
using BlogSystem.Shared._ٌApi_Response;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BlogSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController(IServiceMangar serviceMangar) : ControllerBase
    {
        [HttpPost("login")]
        public async Task<ActionResult<ApiResponse<UserDto>>> Login([FromBody] LoginDto model)
        {
            var result = await serviceMangar.AuthService.LoginAsync(model);

            if (result is null)
                return Unauthorized(ApiResponse<UserDto>.FailResponse("Invalid credentials"));

            return Ok(ApiResponse<UserDto>.SuccessResponse(result, "Login successful"));
        }

        [HttpPost("register")]
        public async Task<ActionResult<ApiResponse<string>>> Register([FromBody] RegisterDto model)
        {
            var result = await serviceMangar.AuthService.RegisterAsync(model);

            if (result is null)
                return BadRequest(ApiResponse<string>.FailResponse("Registration failed"));

            return Ok(ApiResponse<string>.SuccessResponse(result, "User registered successfully"));
        }

    }
}
