using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlogSystem.Application.Abstraction.Contracts.Repository.Comment;
using BlogSystem.Domain.Entities.Comments;
using BlogSystem.Infrastructure.Persistence.Data;
using Microsoft.EntityFrameworkCore;

namespace BlogSystem.Infrastructure.Persistence.Repository.Comments
{
    internal class CommentRepository(ApplicationDbContext dbContext) : GenaricRepository<Comment, int>(dbContext), ICommentRepository
    {
        public async Task<IEnumerable<Comment>> GetCommentsByPostId(int postId)
        {
            return await dbContext.Comments
                .Where(Comments => Comments.BlogPostId == postId)
                .Include(c => c.Author)
                .OrderByDescending(c => c.CreatedOn)
                .ToListAsync();
        }
    }
}
