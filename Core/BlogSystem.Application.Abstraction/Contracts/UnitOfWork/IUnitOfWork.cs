using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlogSystem.Application.Abstraction.Contracts.Repository;
using BlogSystem.Application.Abstraction.Contracts.Repository.Comment;
using BlogSystem.Application.Abstraction.Contracts.Repository.Posts;
using BlogSystem.Application.Abstraction.Contracts.Repository.Reactions;
using BlogSystem.Domain.Entities;

namespace BlogSystem.Application.Abstraction.Contracts.UnitOfWork
{
    public interface IUnitOfWork : IAsyncDisposable
    {
        IGenaricRepository<TEntity, TKey> GenaricRepository<TEntity, TKey>()
            where TEntity : BaseEntity<TKey>
            where TKey : IEquatable<TKey>;

        IBlogPostRepository BlogPostRepository();
        ICommentRepository commentRepository();

        IPostReactionRepository PostReactionRepository();

        Task<int> CompleteAsync();

    }
}
