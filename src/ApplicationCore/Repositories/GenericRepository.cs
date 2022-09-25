using ApplicationCore.Interfaces;
using Ardalis.Specification;
using Common.Enums;
using Infrastructure.Data;
using Infrastructure.Data.Models;
using Infrastructure.Services;
using Infrastructure.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Repositories
{
    public abstract class GenericRepository<TEntity> : IRepository<TEntity> where TEntity : EntityBase
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<GenericRepository<TEntity>> _logger;

        public GenericRepository(ApplicationDbContext context, ILogger<GenericRepository<TEntity>> logger)
        {
            _context = context;
            _logger = logger;
        }

        public virtual async Task<TEntity> GetOne(Guid id)
        {
            try
            {
                return await _context.Set<TEntity>().FindAsync(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return null;
            }
        }

        public virtual async Task<IEnumerable<TEntity>> GetAll()
        {
            return await _context.Set<TEntity>().ToListAsync();
        }

        public virtual async Task<IEnumerable<TEntity>> Find(Expression<Func<TEntity, bool>> predicate)
        {
            return await _context.Set<TEntity>().Where(predicate).ToListAsync();
        }

        public virtual async void Add(TEntity entity)
        {
            await _context.Set<TEntity>().AddAsync(entity);
            await _context.SaveChangesAsync();
        }
        public virtual async void AddRange(IEnumerable<TEntity> entities)
        {
            await _context.Set<TEntity>().AddRangeAsync(entities);
            await _context.SaveChangesAsync();
        }
        public virtual void Remove(TEntity entity)
        {
            _context.Set<TEntity>().Remove(entity);
            _context.SaveChanges();
        }
        public virtual void RemoveRange(IEnumerable<TEntity> entities)
        {
            _context.Set<TEntity>().RemoveRange(entities);
            _context.SaveChanges();
        }       
    }
}
