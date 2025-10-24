using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using BlogSystem.Application.Abstraction.Contracts.Services.Post;
using BlogSystem.Application.Abstraction.Contracts.UnitOfWork;
using BlogSystem.Application.Abstraction.Dtos.Post;
using BlogSystem.Domain.Entities.Post;
using BlogSystem.Domain.Entities.User;
using BlogSystem.Shared._ٌApi_Response;

namespace BlogSystem.Application.Services.Post
{
    internal class BlogPostService(IUnitOfWork unitOfWork,
        IMapper mapper) : IBlogPostService
    {
        public async Task<ApiResponse<PagedResult<PostDto>>> GetAllAsync(PostQueryParameters parameters)
        {
            var (items, total) = await unitOfWork.BlogPostRepository().GetPostsAsync(parameters);

            var result = new PagedResult<PostDto>()
            {
                Items = mapper.Map<IEnumerable<PostDto>>(items),
                Page = parameters.Page,
                PageSize = parameters.PageSize,
                TotalCount = total
            };

            return ApiResponse<PagedResult<PostDto>>.SuccessResponse(result);
        }
        public async Task<ApiResponse<PostDto>> GetByIdAsync(int id)
        {
            var post = await unitOfWork.BlogPostRepository().GetPostWithRelatedAsync(id);

            if (post is null)
                return ApiResponse<PostDto>.FailResponse("Post Not Found");

            var result =  mapper.Map<PostDto>(post);
            return ApiResponse<PostDto>.SuccessResponse(result);
        }

        public async Task<ApiResponse<PostDto>> CreateAsync(PostCreateDto dto, string authorId)
        {
            // ✅ Check Author exists
            //var author = await unitOfWork.GenaricRepository<ApplicationUser, string>().GetByIdAsync(authorId);
            //if (author == null)
            //    return ApiResponse<PostDto>.FailResponse("Invalid AuthorId");

            // ✅ Check Category exists
            var category = await unitOfWork.GenaricRepository<Category, int>().GetByIdAsync(dto.CategoryId);
            if (category == null)
                return ApiResponse<PostDto>.FailResponse("Invalid CategoryId");

            // ✅ Map Post
            var post = mapper.Map<BlogPost>(dto);
            post.AuthorId = authorId;
            post.CreatedOn = DateTime.UtcNow;

            // Attach existing references
            //post.Author = author;
            post.Category = category;

            // ✅ Save Post
            await unitOfWork.BlogPostRepository().AddAsync(post);
            await unitOfWork.CompleteAsync();

            // Re-fetch with related data
            var createdPost = await unitOfWork.BlogPostRepository().GetPostWithRelatedAsync(post.Id);

            var result = mapper.Map<PostDto>(createdPost);
            return ApiResponse<PostDto>.SuccessResponse(result, "Post created successfully");
        }



        public async Task<ApiResponse<PostDto>> UpdateAsync(PostUpdateDto dto, string userId, bool isAdminOrEditor)
        {
            var post = await unitOfWork.BlogPostRepository().GetPostWithRelatedAsync(dto.Id);
            if (post is null)
                return ApiResponse<PostDto>.FailResponse("Post not found");

            // Rule: Only author or Admin/Editor can update
            if (post.AuthorId != userId && !isAdminOrEditor)
                return ApiResponse<PostDto>.FailResponse("Unauthorized");

            post.Title = dto.Title;
            post.Content = dto.Content;
            post.CategoryId = dto.CategoryId;
            post.Status = (PostStatus)dto.Status;
            // Update Tags logic ممكن نزوده بعدين

            await unitOfWork.CompleteAsync();

            var result = mapper.Map<PostDto>(post);
            return ApiResponse<PostDto>.SuccessResponse(result, "Post updated successfully");
        }
        public async Task<ApiResponse<bool>> DeleteAsync(int id, string userId, bool isAdmin)
        {
            var post = await unitOfWork.BlogPostRepository().GetByIdAsync(id);
            if (post is null)
                return ApiResponse<bool>.FailResponse("Post not found");

            // Rule: Only Admin can delete
            if (!isAdmin)
                return ApiResponse<bool>.FailResponse("Unauthorized");

            unitOfWork.BlogPostRepository().Delete(id);
            await unitOfWork.CompleteAsync();

            return ApiResponse<bool>.SuccessResponse(true, "Post deleted successfully");


        }

        public async Task<ApiResponse<IEnumerable<PostDto>>> GetPostsByUserAsync(string userId)
        {
            var posts = await unitOfWork.BlogPostRepository().GetPostsByUserIdAsync(userId);
            var result = mapper.Map<IEnumerable<PostDto>>(posts);
            return ApiResponse<IEnumerable<PostDto>>.SuccessResponse(result);
        }
    }
}
