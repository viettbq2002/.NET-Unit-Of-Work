using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnitOfWork.Core.Interfaces;
using UnitOfWork.Core.Models;
using UnitOfWork.Infrastructure.DTOs;
using UnitOfWork.Services.Error;
using UnitOfWork.Services.Interfaces;

namespace UnitOfWork.Services.Implements
{
    public class ProductService : IProductService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ICacheService _cacheService;

        public ProductService(IUnitOfWork unitOfWork, IMapper mapper, ICacheService cacheService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _cacheService = cacheService;
        }

        public async Task CreateProduct(CreateProduct request)
        {
            var product = _mapper.Map<Product>(request);
            await _unitOfWork.Products.AddAsync(product);
            await _unitOfWork.SaveAsync();
        }

        public async Task<List<Product>> CreateProductBulk(List<CreateProduct> requests)
        {
            var products = _mapper.Map<List<Product>>(requests);
            var response = await _unitOfWork.Products.AddRangeAsync(products);
            await _unitOfWork.SaveAsync();
            return response;
        }

        public async Task DeleteProduct(int productId)
        {
            var product = await _unitOfWork.Products.GetByIdAsync(productId) ?? throw new NotFoundException("Product Not Found");
            _unitOfWork.Products.Delete(product);
            await _unitOfWork.SaveAsync();
            
            
        }

        public async Task<IEnumerable<Product>> GetAllProducts()
        {
            var products = await _cacheService.GetData<IEnumerable<Product>>("products:list");
            if(products is not null)
            {
                return products;
            }
            products = await _unitOfWork.Products.GetAllAsync();
            await _cacheService.SetData<IEnumerable<Product>>("products:list",products);
            return products;
        }

        public async Task<Product> GetProductById(int id)
        {
            var product = await _cacheService.GetData<Product>($"product:{id}");
            if(product is not null)
            {
                return product;
            }
            product = await _unitOfWork.Products.GetByIdAsync(id) ?? throw new NotFoundException("Product Not Found");
            await _cacheService.SetData<Product>($"product:{id}", product);

            return product;
        }

        public async Task UpdateProduct(UpdateProduct request , int productId)
        {
            var product = await _unitOfWork.Products.GetByIdAsync(productId) ?? throw new NotFoundException("Product Not Found");
            _mapper.Map<UpdateProduct, Product>(request, product);
            _unitOfWork.Products.Update(product);
            await _unitOfWork.SaveAsync();
        }
    }
}
