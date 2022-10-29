using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Ardalis.Specification;
using Common.Enums;
using Infrastructure.Data;
using Infrastructure.Services;
using Microsoft.AspNetCore.Http;

namespace ApplicationCore.Interfaces
{
    public interface IRepository<TEntity>  where TEntity : EntityBase
    {
        Task<TEntity> GetOne(Guid id);
        Task<IEnumerable<TEntity>> GetAll();
        Task<IEnumerable<TEntity>> Find(Expression<Func<TEntity, bool>> predicate);
        Task<bool> AddAsync(TEntity entity);
        void AddRange(IEnumerable<TEntity> entities);
        Task<bool> Remove(TEntity entity);
        void RemoveRange(IEnumerable<TEntity> entities);
        Task SaveChangesAsync();
    }
}
