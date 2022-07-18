using AutoMapper;
using CleanArchitecture.Application.Interfaces;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchitecture.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IProductService _productService;
        public ProductController(IMapper mapper, IProductService productService)
        {
            _mapper = mapper;
            _productService = productService;
        }

        [HttpGet("GetProductByName/{productName}")]
        public async Task<IEnumerable<ProductDTO>> GetProducts(string productName)
        {
            if (string.IsNullOrWhiteSpace(productName))
            {
                var list = await _productService.GetProductList();
                return _mapper.Map<IEnumerable<ProductDTO>>(list);
            }

            var listByName = await _productService.GetProductByName(productName);
            return _mapper.Map<IEnumerable<ProductDTO>>(listByName);
        }

        [HttpGet("GetProductById/{productId}")]
        public async Task<ProductDTO> GetProductById(int productId)
        {
            var product = await _productService.GetProductById(productId);
            return _mapper.Map<ProductDTO>(product);
        }

        [HttpPost]
        public async Task<ProductDTO> CreateProduct(ProductDTO productViewModel)
        {
            var mapped = _mapper.Map<Product>(productViewModel);
            if (mapped == null)
                throw new Exception("Entity could not be mapped");

            var entityDto = await _productService.Create(mapped);

            return _mapper.Map<ProductDTO>(entityDto);
        }

        [HttpPut]
        public async Task UpdateProduct(ProductDTO productViewModel)
        {
            var mapped = _mapper.Map<Product>(productViewModel);
            if (mapped == null)
                throw new Exception("Entity could not be mapped");

            await _productService.Update(mapped);
        }

        [HttpDelete]
        public async Task DeleteProduct(ProductDTO productViewModel)
        {
            var mapped = _mapper.Map<Product>(productViewModel);
            if (mapped == null)
                throw new Exception("Entity could not be mapped");

            await _productService.Delete(mapped);
        }
    }
}
