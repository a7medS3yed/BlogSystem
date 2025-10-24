using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlogSystem.Application.Abstraction.Contracts.Services.Auth;
using BlogSystem.Application.Abstraction.Contracts.UnitOfWork;
using BlogSystem.Application.Abstraction.Dtos.Auth;
using BlogSystem.Domain.Entities.User;
using BlogSystem.Shared._ٌApi_Response;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace BlogSystem.Application.Services.Auth
{
    internal class ProfileService(IUnitOfWork unitOfWork, UserManager<ApplicationUser> userManager) : IProfileService
    {
        public async Task<ApiResponse<ProfileResponseDto>> GetProfileAsync(string userId)
        {
            // 1. Get User
            var user = await userManager.FindByIdAsync(userId);
            if (user is null)
                return ApiResponse<ProfileResponseDto>.FailResponse("User not found");
            var role = (await userManager.GetRolesAsync(user)).FirstOrDefault() ?? "User";

            // 2. Get Posts
            var posts = new List<PostSummaryDto>();
            if (role == "Admin" || role == "Editor")
            {
                var userPosts = await unitOfWork.BlogPostRepository()
                    .GetQueryable()
                    .AsNoTracking()
                    .Where(p => p.AuthorId == userId)
                    .Include(p => p.Category)
                    .Include(p => p.BlogPostTags).ThenInclude(pt => pt.Tag)
                    .Include(p => p.Comments)
                    .ToListAsync();

                foreach (var p in userPosts)
                {
                    var reactions = await unitOfWork.PostReactionRepository().GetPostReactionsCountAsync(p.Id);
                    posts.Add(new PostSummaryDto
                    {
                        Id = p.Id,
                        Title = p.Title,
                        Category = p.Category.Name,
                        Likes = reactions.Likes,
                        Dislikes = reactions.Dislikes,
                        CommentsCount = p.Comments.Count
                    });
                }
            }

            // 3. Get Comments
            var userComments = await unitOfWork.commentRepository()
                .GetQueryable()
                .AsNoTracking()
                .Where(c => c.AuthorId == userId)
                .Include(c => c.BlogPost)
                .ToListAsync();

            var comments = userComments.Select(c => new CommentSummaryDto
            {
                Id = c.Id,
                Content = c.Content,
                PostTitle = c.BlogPost?.Title ?? "",
                CreatedOn = c.CreatedOn
            }).ToList();

            // 4. Combine everything
            var profile = new ProfileResponseDto
            {
                User = new UserProfileDto
                {
                    Id = user.Id,
                    UserName = user.UserName ?? "",
                    Email = user.Email ?? "",
                    Role = role
                },
                Posts = posts,
                Comments = comments
            };

            return ApiResponse<ProfileResponseDto>.SuccessResponse(profile);
        }
    }

}
