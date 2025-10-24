using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlogSystem.Application.Abstraction.Dtos.Comment;
using BlogSystem.Shared._ٌApi_Response;

namespace BlogSystem.Application.Abstraction.Contracts.Services.Comment
{
    public interface ICommentService
    {
        Task<ApiResponse<IEnumerable<CommentDto>>> GetByPostIdAsync(int postId);
        Task<ApiResponse<CommentDto>> AddAsync(CommentCreateDto dto, string userId);
        Task<ApiResponse<bool>> DeleteAsync(int id, string userId, bool isAdmin);
    }
}
