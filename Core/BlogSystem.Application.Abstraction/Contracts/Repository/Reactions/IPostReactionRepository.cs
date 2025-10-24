using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlogSystem.Application.Abstraction.Dtos;
using BlogSystem.Domain.Entities.Reaction;

namespace BlogSystem.Application.Abstraction.Contracts.Repository.Reactions
{
    public interface IPostReactionRepository : IGenaricRepository<PostReactions, int>
    {
        Task<PostReactions?> GetUserReactionOnPostAsync(int postId, string userId);
        Task<ReactionCountDto> GetPostReactionsCountAsync(int postId);
    }
}
