using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlogSystem.Application.Abstraction.Dtos.Auth;

namespace BlogSystem.Application.Abstraction.Contracts.Services.Auth
{
    public interface IAuthService
    {
        Task<UserDto?> LoginAsync(LoginDto dto);
        Task<string?> RegisterAsync(RegisterDto dto);
    }
}
