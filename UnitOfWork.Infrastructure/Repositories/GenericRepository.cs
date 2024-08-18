using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using UnitOfWork.Core.Interfaces;

namespace UnitOfWork.Infrastructure.Repositories
{
    public abstract class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected readonly DbContextClass _context;
        private readonly DbSet<T> _dbSet;

        protected GenericRepository(DbContextClass context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }
            
        public async Task<T> AddAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
            return entity;
        }

        public void Delete(T entity)
        {
            _dbSet.Remove(entity);
        }

       
       
        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _dbSet.AsNoTracking().ToListAsync();
        }

        public async Task<T?> GetByIdAsync(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task<IEnumerable<T>> SelectManyAsync(Expression<Func<T, bool>> predicate)
        {
            return await _dbSet.AsNoTracking().Where(predicate).ToListAsync();
        }

        public async Task<IEnumerable<T>> SelectManyAsync(Expression<Func<T, bool>>? predicate, params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> query = _dbSet.AsNoTracking();
            if (includes.Length > 0)
            {

                query = ApplyIncludes(query, includes);
            }
            
            if (predicate != null) {
                query = query.Where(predicate);
            }
            return await query.ToListAsync();
        }

        public async Task<T?> SelectOneAsync(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> query = _dbSet;
            if (includes.Length > 0 )
            {
                query = ApplyIncludes(query, includes);
            }

           
            return await query.FirstOrDefaultAsync(predicate);

        }
        private static IQueryable<T> ApplyIncludes(IQueryable<T> query, params Expression<Func<T, object>>[] includes)
        {
            foreach (var include in includes)
            {
                query = query.Include(include);
            }
            return query;
        }

        public void Update(T entity)
        {
            _context.Entry<T>(entity).State = EntityState.Modified;
            
        }

        public async Task<IEnumerable<T>> GetBySpecificationAsync(ISpecification<T> specification)
        {
            IQueryable<T> query = _dbSet.AsNoTracking();

            if (specification.Includes.Count > 0)
            {
                query = ApplyIncludes(query, specification.Includes.ToArray());
            }

            if (specification.ToExpression() != null)
            {
                query = query.Where(specification.ToExpression());
            }

            return await query.ToListAsync();
        }

        public async Task<T?> GetOneBySpecificationAsync(ISpecification<T> specification)
        {
            IQueryable<T> query = _dbSet;

            if (specification.Includes.Count > 0)
            {
                query = ApplyIncludes(query, specification.Includes.ToArray());
            }

            if (specification.ToExpression() != null)
            {
                query = query.Where(specification.ToExpression());
            }

            return await query.FirstOrDefaultAsync();
        }
    }
}
