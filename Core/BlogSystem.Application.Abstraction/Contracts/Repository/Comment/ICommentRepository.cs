using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlogSystem.Domain.Entities.Comments;

namespace BlogSystem.Application.Abstraction.Contracts.Repository.Comment
{
    public interface ICommentRepository : IGenaricRepository<BlogSystem.Domain.Entities.Comments.Comment, int>
    {
        Task<IEnumerable<BlogSystem.Domain.Entities.Comments.Comment>> GetCommentsByPostId(int postId);
    }
}
