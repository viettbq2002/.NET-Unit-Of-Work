using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace UnitOfWork.Core.Interfaces
{
    public interface IGenericRepository <T> where T : class
    {
        Task<T?> GetByIdAsync(int id);
        Task<IEnumerable<T>> GetAllAsync();
        Task<IEnumerable<T>> SelectManyAsync(Expression<Func<T, bool>>? predicate , params Expression<Func<T, object>>[] includes);
        Task<T?> SelectOneAsync(Expression<Func<T, bool>> predicate , params Expression<Func<T, object>>[] includes);
        Task<IEnumerable<T>> GetBySpecificationAsync(ISpecification<T> specification);
        Task <T?> GetOneBySpecificationAsync(ISpecification<T> specification);

        Task<List<T>> AddRangeAsync (List<T> items);
        Task<T> AddAsync (T entity);

        void Update (T entity);

        void Delete (T entity);

         

    }
}
