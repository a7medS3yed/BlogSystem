using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlogSystem.Application.Abstraction.Dtos.Post;
using BlogSystem.Domain.Entities.Post;

namespace BlogSystem.Application.Abstraction.Contracts.Repository.Posts
{
    public interface IBlogPostRepository : IGenaricRepository<BlogPost, int>
    {
        Task<BlogPost?> GetPostWithRelatedAsync(int id);
        Task<(IEnumerable<BlogPost> Items, int Total)> GetPostsAsync(PostQueryParameters parameters);
        Task<IEnumerable<BlogPost>> GetPostsByUserIdAsync(string userId);
        
    }
}
