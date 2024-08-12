using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnitOfWork.Core.Interfaces;

namespace UnitOfWork.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        public IProductRepository Products { get; }
        private readonly DbContextClass _context;
        public UnitOfWork(DbContextClass context , IProductRepository productRepository)
        {
            _context = context;
            Products = productRepository;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);

        }
        protected void Dispose(bool disposing)
        {
            if (disposing)
            {
                _context.Dispose();
            }
        }

        public int Save()
        {
            return _context.SaveChanges();
        }

        public async Task<int> SaveAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}
