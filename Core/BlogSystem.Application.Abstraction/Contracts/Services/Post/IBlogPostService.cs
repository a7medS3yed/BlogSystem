using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlogSystem.Application.Abstraction.Dtos.Post;
using BlogSystem.Shared._ٌApi_Response;

namespace BlogSystem.Application.Abstraction.Contracts.Services.Post
{
    public interface IBlogPostService
    {
        Task<ApiResponse<PostDto>> CreateAsync(PostCreateDto dto, string AuthorId);
        Task<ApiResponse<PostDto>> UpdateAsync(PostUpdateDto dto, string userId, bool isAdminOrEditor);
        Task<ApiResponse<bool>> DeleteAsync(int id, string userId, bool isAdmin);
        Task<ApiResponse<PostDto>> GetByIdAsync(int id);
        Task<ApiResponse<PagedResult<PostDto>>> GetAllAsync(PostQueryParameters parameters);

        Task<ApiResponse<IEnumerable<PostDto>>> GetPostsByUserAsync(string userId);
    }
}
