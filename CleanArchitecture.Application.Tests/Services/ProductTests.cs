using CleanArchitecture.Application.Services;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.Repositories;
using Moq;
using Xunit;

namespace CleanArchitecture.Application.Tests.Services
{
    public class ProductTests
    {
        private Mock<IProductRepository> mockProductRepository;
        
        public ProductTests()
        {
            mockProductRepository = new Mock<IProductRepository>();
        }

        [Fact]
        public async Task Crate_New_Product()
        {
            var product = new Product();
            Product nullProduct = null;

            mockProductRepository.Setup(x => x.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(nullProduct);
            mockProductRepository.Setup(x => x.AddAsync(product)).ReturnsAsync(product);

            var productService = new ProductService(mockProductRepository.Object);
            var productCrate = await productService.Create(product);

            mockProductRepository.Verify(x => x.GetByIdAsync(It.IsAny<int>()), Times.Once);
            mockProductRepository.Verify(x => x.AddAsync(product), Times.Once);

        }

        [Fact]
        public async Task Create_New_Product_Validate_If_Exists()
        {
            var product = new Product();

            mockProductRepository.Setup(x => x.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(product);

            var productService = new ProductService(mockProductRepository.Object);

            await Assert.ThrowsAsync<ApplicationException>(async () => await productService.Create(product));
        }

        [Fact]
        public async Task Delete_Product()
        {
            var product = new Product();

            mockProductRepository.Setup(x => x.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(product);
            mockProductRepository.Setup(x => x.DeleteAsync(product));

            var productService = new ProductService(mockProductRepository.Object);
            await productService.Delete(product);

            mockProductRepository.Verify(x => x.GetByIdAsync(It.IsAny<int>()), Times.AtLeast(2));
            mockProductRepository.Verify(x => x.DeleteAsync(product), Times.Once);
        }

        [Fact]
        public async Task Delete_Product_Validate_If_Exists()
        {
            var product = new Product();
            Product nullProduct = null;

            mockProductRepository.Setup(x => x.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(nullProduct);

            var productService = new ProductService(mockProductRepository.Object);

            await Assert.ThrowsAsync<ApplicationException>(async () => await productService.Delete(product));
        }

        [Fact]
        public async Task Get_Product_By_Id()
        {
            var product = new Product();

            mockProductRepository.Setup(x => x.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(product);

            var productService = new ProductService(mockProductRepository.Object);
            await productService.GetProductById(product.Id);

            mockProductRepository.Verify(x => x.GetByIdAsync(It.IsAny<int>()), Times.Once);
        }

        [Fact]
        public async Task Get_Product_By_Name()
        {
            var product = new Product();

            mockProductRepository.Setup(x => x.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(product);

            var productService = new ProductService(mockProductRepository.Object);
            await productService.GetProductByName(product.ProductName);

            mockProductRepository.Verify(x => x.GetProductByNameAsync(It.IsAny<string>()), Times.Once);
        }
        
        [Fact]
        public async Task Get_All_Products()
        {
            var product = new Product();

            mockProductRepository.Setup(x => x.GetAllAsync()).ReturnsAsync(new List<Product>());

            var productService = new ProductService(mockProductRepository.Object);
            var products = await productService.GetProductList();

            mockProductRepository.Verify(x => x.GetAllAsync(), Times.Once);
        }

        [Fact]
        public async Task Update_Product()
        {
            var product = new Product();

            mockProductRepository.Setup(x => x.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(product);

            var productService = new ProductService(mockProductRepository.Object);
            await productService.Update(product);

            mockProductRepository.Verify(x => x.GetByIdAsync(It.IsAny<int>()), Times.AtLeast(2));
            mockProductRepository.Verify(x => x.UpdateAsync(product), Times.Once);
        }

        [Fact]
        public async Task Update_Product_If_Product_Not_Exist()
        {
            var product = new Product();
            Product nullProduct = null;

            mockProductRepository.Setup(x => x.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(nullProduct);

            var productService = new ProductService(mockProductRepository.Object);

            await Assert.ThrowsAsync<ApplicationException>(async () => await productService.Update(product));
        }
    }
}
