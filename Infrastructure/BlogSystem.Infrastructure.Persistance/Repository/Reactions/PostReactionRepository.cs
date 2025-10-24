using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlogSystem.Application.Abstraction.Contracts.Repository.Reactions;
using BlogSystem.Application.Abstraction.Dtos;
using BlogSystem.Domain.Entities.Reaction;
using BlogSystem.Infrastructure.Persistence.Data;
using Microsoft.EntityFrameworkCore;

namespace BlogSystem.Infrastructure.Persistence.Repository.Reactions
{
    internal class PostReactionRepository(ApplicationDbContext context) : GenaricRepository<PostReactions, int>(context), IPostReactionRepository
    {
        public async Task<ReactionCountDto> GetPostReactionsCountAsync(int postId)
        {
            var likes = await context.Set<PostReactions>()
                .CountAsync(pr => pr.PostId == postId && pr.IsLike);

            var dislikes = await context.Set<PostReactions>()
                .CountAsync(pr => pr.PostId == postId && !pr.IsLike);

            return new ReactionCountDto { Likes = likes, Dislikes = dislikes };
        }

        public async Task<PostReactions?> GetUserReactionOnPostAsync(int postId, string userId)
        {
           return await context.Set<PostReactions>()
                .FirstOrDefaultAsync(pr => pr.PostId == postId && pr.UserId == userId);
        }
    }
}
