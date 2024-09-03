using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnitOfWork.Core.Models;
using UnitOfWork.Infrastructure.DTOs;

namespace UnitOfWork.Services.Interfaces
{
    public interface IProductService
    {
        Task CreateProduct(CreateProduct request);
        Task<IEnumerable<Product>> GetAllProducts();
        Task<Product> GetProductById(int id);
        Task UpdateProduct(UpdateProduct request, int productId);
        Task DeleteProduct(int productId);
        Task<List<Product>> CreateProductBulk(List<CreateProduct> requests);

    }
}
