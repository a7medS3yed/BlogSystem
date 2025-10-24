using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlogSystem.Domain.Entities;

namespace BlogSystem.Application.Abstraction.Contracts.Repository
{
    public interface IGenaricRepository<TEntity, TKey>
        where TEntity : BaseEntity<TKey>
        where TKey : IEquatable<TKey>
    {

        Task<IEnumerable<TEntity>> GetAsync(bool withTraking = false);
        Task<TEntity?> GetByIdAsync(TKey id);
        Task AddAsync(TEntity entity);
        void Update(TEntity entity);
        void Delete(TKey id);
        IQueryable<TEntity> GetQueryable();
    }
}
