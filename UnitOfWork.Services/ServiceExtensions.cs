using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnitOfWork.Services.Implements;
using UnitOfWork.Services.Interfaces;

namespace UnitOfWork.Services
{
    public static class ServiceExtensions
    {
        public static IServiceCollection AddService(this IServiceCollection services) {

            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            services.AddScoped<IProductService, ProductService>();
            return services;
        }
    }
}
