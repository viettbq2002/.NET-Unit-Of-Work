using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnitOfWork.Core.Interfaces;

namespace UnitOfWork.Infrastructure.ServiceExtensions
{
    public static class CoreExtension
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<DbContextClass>(otp => otp.UseNpgsql(configuration.GetConnectionString("defaultConnection")));
            services.AddScoped<IUnitOfWork,Repositories.UnitOfWork>();
            services.AddScoped<IProductRepository,Repositories.ProductRepository>();




            return services;
        }
    }
}
