using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnitOfWork.Core.Interfaces;
using UnitOfWork.Core.Models;
using UnitOfWork.Infrastructure.DTOs;
using UnitOfWork.Services.Interfaces;

namespace UnitOfWork.Services.Implements
{
    public class ProductService : IProductService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ProductService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task CreateProduct(CreateProduct request)
        {
            var product = _mapper.Map<Product>(request);
            await _unitOfWork.Products.AddAsync(product);
            await _unitOfWork.SaveAsync();
        }

        public async Task DeleteProduct(int productId)
        {
            var product = await _unitOfWork.Products.GetByIdAsync(productId) ?? throw new KeyNotFoundException("Product not found");
            _unitOfWork.Products.Delete(product);
            await _unitOfWork.SaveAsync();
            
            
        }

        public async Task<IEnumerable<Product>> GetAllProducts()
        {
            var products = await _unitOfWork.Products.GetAllAsync();
            return products;
        }

        public Task<Product> GetProductById(int id)
        {
            throw new NotImplementedException();
        }

        public async Task UpdateProduct(UpdateProduct request , int productId)
        {
            var product = await _unitOfWork.Products.GetByIdAsync(productId) ?? throw new KeyNotFoundException("Product not found");
             _mapper.Map<UpdateProduct, Product>(request, product);
            _unitOfWork.Products.Update(product);
            await _unitOfWork.SaveAsync();

            


        }
    }
}
