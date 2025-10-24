using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlogSystem.Application.Abstraction.Contracts.Repository;
using BlogSystem.Application.Abstraction.Contracts.Repository.Comment;
using BlogSystem.Application.Abstraction.Contracts.Repository.Posts;
using BlogSystem.Application.Abstraction.Contracts.Repository.Reactions;
using BlogSystem.Application.Abstraction.Contracts.UnitOfWork;
using BlogSystem.Domain.Entities;
using BlogSystem.Infrastructure.Persistence.Data;
using BlogSystem.Infrastructure.Persistence.Repository;
using BlogSystem.Infrastructure.Persistence.Repository.Comments;
using BlogSystem.Infrastructure.Persistence.Repository.Posts;
using BlogSystem.Infrastructure.Persistence.Repository.Reactions;
using Microsoft.EntityFrameworkCore;

namespace BlogSystem.Infrastructure.Persistence.UnitOfWork
{
    internal class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        private readonly ConcurrentDictionary<string, object> _repositories = new();
        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
        }


        public async Task<int> CompleteAsync()
            => await _context.SaveChangesAsync();

        public async ValueTask DisposeAsync()
            => await _context.DisposeAsync();
        public IBlogPostRepository BlogPostRepository()
        {
            return (IBlogPostRepository)_repositories.GetOrAdd(nameof(IBlogPostRepository), new BlogPostRepository(_context));
        }

        public IGenaricRepository<TEntity, TKey> GenaricRepository<TEntity, TKey>()
            where TEntity : BaseEntity<TKey>
            where TKey : IEquatable<TKey>
        {
            return (IGenaricRepository<TEntity, TKey>) _repositories.GetOrAdd(typeof(TEntity).Name, new GenaricRepository<TEntity, TKey>(_context));
        }

        public ICommentRepository commentRepository()
        {
            return (ICommentRepository)_repositories.GetOrAdd(nameof(ICommentRepository), new CommentRepository(_context));
        }

        public IPostReactionRepository PostReactionRepository()
        {
            return (IPostReactionRepository)_repositories.GetOrAdd(nameof(IPostReactionRepository), new PostReactionRepository(_context));
        }
    }
}
