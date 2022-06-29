using CleanArchitecture.Application.Interfaces;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.Repositories;

namespace CleanArchitecture.Application.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }
        public async Task<Product> Create(Product product)
        {
            await ValidateProductIfExist(product);

            var newEntity = await _productRepository.AddAsync(product);
            return newEntity;
        }

        public async Task Delete(Product product)
        {
            ValidateProductIfNotExist(product);
            var deletedProduct = await _productRepository.GetByIdAsync(product.Id);
            if (deletedProduct == null)
                throw new ApplicationException("Entity could not be loaded");

            await _productRepository.DeleteAsync(deletedProduct);
        }

        public async Task<Product> GetProductById(int productId)
        {
            var product = await _productRepository.GetByIdAsync(productId);
            return product;
        }

        public async Task<IEnumerable<Product>> GetProductByName(string productName)
        {
            var productList = await _productRepository.GetProductByNameAsync(productName);
            return productList;
        }

        public async Task<IEnumerable<Product>> GetProductList()
        {
            var productList = await _productRepository.GetAllAsync();
            return productList;
        }

        public async Task Update(Product product)
        {
            ValidateProductIfNotExist(product);

            var editProduct = await _productRepository.GetByIdAsync(product.Id);
            if (editProduct == null)
                throw new ApplicationException("Entity could not be loaded");

            editProduct.Id = product.Id;
            editProduct.ProductName = product.ProductName;
            editProduct.UnitPrice = product.UnitPrice;
            editProduct.UnitsInStock = product.UnitsInStock;
            editProduct.UnitsOnOrder = product.UnitsOnOrder;

            await _productRepository.UpdateAsync(editProduct);

        }
        private async Task ValidateProductIfExist(Product product)
        {
            var existingEntity = await _productRepository.GetByIdAsync(product.Id);
            if (existingEntity != null)
                throw new ApplicationException($"{product.ToString()} already exists");
        }
        private void ValidateProductIfNotExist(Product product)
        {
            var existingEntity = _productRepository.GetByIdAsync(product.Id);
            if (existingEntity == null)
                throw new ApplicationException($"{product.ToString()} not found");
        }
    }
}
