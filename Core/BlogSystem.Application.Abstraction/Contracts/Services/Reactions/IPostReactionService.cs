using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlogSystem.Application.Abstraction.Dtos;
using BlogSystem.Shared._ٌApi_Response;

namespace BlogSystem.Application.Abstraction.Contracts.Services.Reactions
{
    public interface IPostReactionService
    {
        Task<ApiResponse<ReactionCountDto>> GetReactionsAsync(int postId);
        Task<ApiResponse<ReactionCountDto>> LikePostAsync(int postId, string userId);
        Task<ApiResponse<ReactionCountDto>> DisLikePostAsync(int postId, string userId);
        
    }
}
