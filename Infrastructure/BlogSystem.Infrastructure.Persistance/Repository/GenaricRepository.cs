using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlogSystem.Application.Abstraction.Contracts.Repository;
using BlogSystem.Domain.Entities;
using BlogSystem.Domain.Entities.Post;
using BlogSystem.Infrastructure.Persistence.Data;
using Microsoft.EntityFrameworkCore;

namespace BlogSystem.Infrastructure.Persistence.Repository
{
    internal class GenaricRepository<TEntity, TKey>(ApplicationDbContext dbContext) : IGenaricRepository<TEntity, TKey>
        where TEntity : BaseEntity<TKey>
        where TKey : IEquatable<TKey>
    {
        public IQueryable<TEntity> GetQueryable()
        {
            return dbContext.Set<TEntity>().AsQueryable();
        }
        public async Task<IEnumerable<TEntity>> GetAsync(bool withTraking = false)
        {
            if(typeof(TEntity) == typeof(BlogPost))
            {
                var query = dbContext.Set<BlogPost>()
                    .Include(b => b.Author)
                    .Include(nameof(BlogPost.Comments))
                    .AsQueryable();
                if (!withTraking)
                {
                    query = query.AsNoTracking();
                }
                return (IEnumerable<TEntity>) await query.ToListAsync();
            }
            else
            {
                var query = dbContext.Set<TEntity>().AsQueryable();
                if (!withTraking)
                {
                    query = query.AsNoTracking();
                }
                return await query.ToListAsync();
            }
        }
        public async Task<TEntity?> GetByIdAsync(TKey id)
        {
            var query = await dbContext.Set<TEntity>().FindAsync(id);
            return query;
        }
        public async Task AddAsync(TEntity entity)
             => await dbContext.Set<TEntity>().AddAsync(entity);
        public void Update(TEntity entity)
            => dbContext.Set<TEntity>().Update(entity);
        public void Delete(TKey id)
        {
           var entity = dbContext.Set<TEntity>().Find(id);
            if (entity != null)
                dbContext.Set<TEntity>().Remove(entity);
        }



    }
}
