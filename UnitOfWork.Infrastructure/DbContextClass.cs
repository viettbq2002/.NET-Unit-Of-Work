using Microsoft.EntityFrameworkCore;
using UnitOfWork.Core.Models;

namespace UnitOfWork.Infrastructure
{
    public class DbContextClass : DbContext
    {
        public DbContextClass()
        {
        }

        public DbContextClass(DbContextOptions options) : base(options)
        {
        }
        public virtual DbSet<Product> Products { get; set; }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var entries = ChangeTracker.Entries<BaseModel>().Where(e =>  e.State == EntityState.Modified || e.State == EntityState.Modified);
            foreach (var entry in entries)
            {
                if (entry.State == EntityState.Added)
                {
                    entry.Entity.CreatedAt = DateTime.UtcNow;
                    entry.Entity.UpdatedAt = DateTime.UtcNow;
                }

                if (entry.State == EntityState.Modified)
                {
                    entry.Entity.UpdatedAt = DateTime.UtcNow;
                }
            }
            return await base.SaveChangesAsync(cancellationToken);
        }
    }
}
