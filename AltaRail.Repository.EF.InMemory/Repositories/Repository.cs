using AltaRail.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace AltaRail.Repository.EF.InMemory.Repositories
{
    public abstract class Repository<TEntity> : IRepository<TEntity> where TEntity : EntityBase, new()
    {
        protected readonly GenericContext _context;

        public Repository(GenericContext context)
        {
            _context = context ?? throw new ArgumentNullException("context");
            context.Database.EnsureCreated();
        }

        public async Task<TEntity> GetAsync(string id)
        {
            var entity = await _context.Set<TEntity>().FirstOrDefaultAsync(e => e.Id == id);
            return entity;
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await _context.Set<TEntity>().ToListAsync();
        }

        public async Task AddAsync(TEntity entity)
        {
            await _context.Set<TEntity>().AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(string id)
        {
            var entity = await _context.Set<TEntity>().FindAsync(id);
            _context.Set<TEntity>().Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<TEntity> FindAsync(Expression<Func<TEntity, bool>> lambda)
        {
            var entity = await _context.Set<TEntity>().FirstOrDefaultAsync(lambda);
            return entity;
        }
    
        public async Task UpdateAsync(string id, TEntity entity)
        {
            TEntity exist = await _context.Set<TEntity>().FindAsync(id);
            _context.Entry(exist).CurrentValues.SetValues(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> ExistsAsync(Expression<Func<TEntity, bool>> lambda)
        {
            var exists = await _context.Set<TEntity>().AnyAsync(lambda);
            return exists;
        }
    }
}
