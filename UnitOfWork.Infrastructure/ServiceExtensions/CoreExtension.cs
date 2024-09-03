using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnitOfWork.Core.Interfaces;
using UnitOfWork.Infrastructure.Caching;

namespace UnitOfWork.Infrastructure.ServiceExtensions
{
    public static class CoreExtension
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            var dbConString = configuration.GetConnectionString("DefaultConnection");
            var redisConString = configuration.GetConnectionString("Redis");
            services.AddDbContext<DbContextClass>(otp => otp.UseNpgsql(dbConString));
            services.AddStackExchangeRedisCache(opt =>
            {
                opt.Configuration = redisConString;
                opt.InstanceName = "Products_";

            });
            services.AddScoped<IUnitOfWork,Repositories.UnitOfWork>();

            services.AddScoped<IProductRepository,Repositories.ProductRepository>();
            services.AddScoped<ICacheService,CacheService>();




            return services;
        }
    }
}
