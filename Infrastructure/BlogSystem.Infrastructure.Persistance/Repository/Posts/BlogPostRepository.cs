using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlogSystem.Application.Abstraction.Contracts.Repository.Posts;
using BlogSystem.Application.Abstraction.Dtos.Post;
using BlogSystem.Domain.Entities.Post;
using BlogSystem.Infrastructure.Persistence.Data;
using Microsoft.EntityFrameworkCore;

namespace BlogSystem.Infrastructure.Persistence.Repository.Posts
{
    internal class BlogPostRepository(ApplicationDbContext dbContext) : GenaricRepository<BlogPost, int>(dbContext), IBlogPostRepository
    {
        public async Task<(IEnumerable<BlogPost> Items, int Total)> GetPostsAsync(PostQueryParameters parameters)
        {
            var query = dbContext.BlogPosts
                .Include(p => p.Author)
                .Include(p => p.Category)
                .Include(p => p.BlogPostTags).ThenInclude(pt => pt.Tag)
                .AsNoTracking()
                .AsQueryable();

            // Filtering
            if (!string.IsNullOrEmpty(parameters.Search))
                query = query.Where(q =>
                    EF.Functions.Like(q.Title, $"%{parameters.Search}%") ||
                    EF.Functions.Like(q.Content, $"%{parameters.Search}%"));

            if (parameters.CategoryId.HasValue)
                query = query.Where(q => q.CategoryId == parameters.CategoryId.Value);

            if (parameters.TagId.HasValue)
                query = query.Where(q => q.BlogPostTags.Any(pt => pt.TagId == parameters.TagId.Value));

            if (parameters.Status.HasValue)
                query = query.Where(p => (int)p.Status == parameters.Status.Value);

            // Sorting
            query = parameters.Sort switch
            {
                "date_desc" => query.OrderByDescending(p => p.CreatedOn),
                "title" => query.OrderBy(p => p.Title),
                _ => query.OrderBy(p => p.CreatedOn)
            };

            var total = await query.CountAsync();

            // Pagination
            var items = await query
                .Skip((parameters.Page - 1) * parameters.PageSize)
                .Take(parameters.PageSize)
                .ToListAsync();

            return (items, total);
        }

        public async Task<IEnumerable<BlogPost>> GetPostsByUserIdAsync(string userId)
        {
            return await dbContext.BlogPosts
                .Where(p => p.AuthorId == userId)
                .Include(p => p.Category)
                .Include(p => p.BlogPostTags).ThenInclude(pt => pt.Tag)
                .OrderByDescending(p => p.CreatedOn)
                .ToListAsync();
        }

        public async Task<BlogPost?> GetPostWithRelatedAsync(int id)
        {
            return await dbContext.BlogPosts
                .Include(p => p.Author)
                .Include(p => p.Category)
                .Include(p => p.BlogPostTags).ThenInclude(pt => pt.Tag)
                .FirstOrDefaultAsync(p => p.Id == id);
        }
       
    }
    }
