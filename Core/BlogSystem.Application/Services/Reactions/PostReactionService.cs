using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlogSystem.Application.Abstraction.Contracts.Services.Reactions;
using BlogSystem.Application.Abstraction.Contracts.UnitOfWork;
using BlogSystem.Application.Abstraction.Dtos;
using BlogSystem.Domain.Entities.Reaction;
using BlogSystem.Shared._ٌApi_Response;

namespace BlogSystem.Application.Services.Reactions
{
    internal class PostReactionService(IUnitOfWork unitOfWork) : IPostReactionService
    {
        public async Task<ApiResponse<ReactionCountDto>> GetReactionsAsync(int postId)
        {
            var result = await unitOfWork.PostReactionRepository().GetPostReactionsCountAsync(postId);
            return ApiResponse<ReactionCountDto>.SuccessResponse(result);
        }
        public async Task<ApiResponse<ReactionCountDto>> DisLikePostAsync(int postId, string userId)
        {
            var result = await unitOfWork.PostReactionRepository().GetUserReactionOnPostAsync(postId, userId);
            if (result is null)
            {
                result = new PostReactions
                {
                    PostId = postId,
                    UserId = userId,
                    IsLike = false,
                    CreatedOn = DateTime.UtcNow
                };
                await unitOfWork.PostReactionRepository().AddAsync(result);
            }
            else
                result.IsLike = false; // Update to dislike

            await unitOfWork.CompleteAsync();

            var reactionsCount = await unitOfWork.PostReactionRepository().GetPostReactionsCountAsync(postId);
            return ApiResponse<ReactionCountDto>.SuccessResponse(reactionsCount, "Disliked successfully");
        }


        public async Task<ApiResponse<ReactionCountDto>> LikePostAsync(int postId, string userId)
        {
            var result = await unitOfWork.PostReactionRepository().GetUserReactionOnPostAsync(postId, userId);

            if (result is null)
            {
                result = new PostReactions
                {
                    PostId = postId,
                    UserId = userId,
                    IsLike = true,
                    CreatedOn = DateTime.UtcNow
                };
                await unitOfWork.PostReactionRepository().AddAsync(result);
            }
            else
                result.IsLike = true; // Update to like

            await unitOfWork.CompleteAsync();

            var reactionsCount = await unitOfWork.PostReactionRepository().GetPostReactionsCountAsync(postId);
            return ApiResponse<ReactionCountDto>.SuccessResponse(reactionsCount, "Liked successfully");
        }
    }
}
