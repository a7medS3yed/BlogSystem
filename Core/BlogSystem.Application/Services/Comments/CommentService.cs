using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using BlogSystem.Application.Abstraction.Contracts.Services.Comment;
using BlogSystem.Application.Abstraction.Contracts.UnitOfWork;
using BlogSystem.Application.Abstraction.Dtos.Comment;
using BlogSystem.Domain.Entities.Comments;
using BlogSystem.Shared._ٌApi_Response;

namespace BlogSystem.Application.Services.Comments
{
    internal class CommentService(IUnitOfWork unitOfWork, IMapper mapper) : ICommentService
    {
        public async Task<ApiResponse<IEnumerable<CommentDto>>> GetByPostIdAsync(int postId)
        {
            var comments = await unitOfWork.commentRepository().GetCommentsByPostId(postId);

            var commentsDto = mapper.Map<IEnumerable<CommentDto>>(comments);

            return ApiResponse<IEnumerable<CommentDto>>.SuccessResponse(commentsDto, "Comment Add Successfully");
        }
        public async Task<ApiResponse<CommentDto>> AddAsync(CommentCreateDto dto, string userId)
        {
            
            var post = await unitOfWork.BlogPostRepository().GetByIdAsync(dto.PostId);
            if (post is null)
                return ApiResponse<CommentDto>.FailResponse("Post not found");

            var comment = new Comment
            {
                Content = dto.Content,
                BlogPostId = dto.PostId,
                AuthorId = userId,
                CreatedOn = DateTime.UtcNow
            };

            await unitOfWork.commentRepository().AddAsync(comment);
            await unitOfWork.CompleteAsync();

            var created = await unitOfWork.commentRepository().GetByIdAsync(comment.Id);

            var result = mapper.Map<CommentDto>(created);
            return ApiResponse<CommentDto>.SuccessResponse(result, "Comment added successfully");
        }


        public async Task<ApiResponse<bool>> DeleteAsync(int id, string userId, bool isAdmin)
        {
            var comment = await unitOfWork.commentRepository().GetByIdAsync(id);
            if (comment is null)
                return ApiResponse<bool>.FailResponse("Comment not found");

            // Rule: Only Author or Admin can delete
            if (comment.AuthorId != userId && !isAdmin)
                return ApiResponse<bool>.FailResponse("Unauthorized");

            unitOfWork.commentRepository().Delete(id);
            await unitOfWork.CompleteAsync();

            return ApiResponse<bool>.SuccessResponse(true, "Comment deleted successfully");
        }

    }
}
