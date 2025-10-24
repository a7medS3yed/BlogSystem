using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using BlogSystem.Application.Abstraction.Contracts.Services.Auth;
using BlogSystem.Application.Abstraction.Dtos.Auth;
using BlogSystem.Domain.Entities.User;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace BlogSystem.Application.Services.Auth
{
    internal class AuthService(UserManager<ApplicationUser> userManager,
         SignInManager<ApplicationUser> signInManager,
         IConfiguration configuration) : IAuthService
    {
        public async Task<UserDto?> LoginAsync(LoginDto dto)
        {
            // find user by email
            var user = await userManager.FindByEmailAsync(dto.Email);
            if (user is null) return null;

            // check password
            var result = await signInManager.CheckPasswordSignInAsync(user, dto.Password, false);
            if (!result.Succeeded) return null;

            // get roles
            var roles = await userManager.GetRolesAsync(user);

            return new UserDto
            {
                Id = user.Id,
                Email = dto.Email,
                UserName = user.UserName!,
                Token = await GenerateToken(user, roles)
            };
        }

        public async Task<string?> RegisterAsync(RegisterDto dto)
        {
            var user = new ApplicationUser()
            {
                UserName = dto.UserName,
                Email = dto.Email,
                PhoneNumber = dto.PhoneNumber
            };

            var result = await userManager.CreateAsync(user, dto.Password);

            if (!result.Succeeded) return null;

            await userManager.AddToRoleAsync(user, "Reader");
            return "User registered successfully";
        }

        private Task<string> GenerateToken(ApplicationUser user, IList<string> roles)
        {
            var jwtSettings = configuration.GetSection("JwtSettings");

            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id),
                new Claim(JwtRegisteredClaimNames.Email, user.Email ?? ""),
                new Claim(ClaimTypes.Name, user.UserName ?? "")
            };

            // add roles to claims
            claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["SecretKey"]!));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: jwtSettings["Issuer"],
                audience: jwtSettings["Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddHours(2),
                signingCredentials: creds
            );

            var tokenString = new JwtSecurityTokenHandler().WriteToken(token);

            return Task.FromResult(tokenString);
        }
    }
}
