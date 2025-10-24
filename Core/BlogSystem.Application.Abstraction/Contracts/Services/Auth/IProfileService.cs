using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlogSystem.Application.Abstraction.Dtos.Auth;
using BlogSystem.Shared._ٌApi_Response;

namespace BlogSystem.Application.Abstraction.Contracts.Services.Auth
{
    public interface IProfileService
    {
        Task<ApiResponse<ProfileResponseDto>> GetProfileAsync(string userId);   
    }
}
