using CleanArchitecture.Infraestructure.Data;
using CleanArchitecture.Infraestructure.Repository;
using CleanArchitecture.Tests.Base;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace CleanArchitecture.Tests.Repositories
{
    public class ProductTests
    {
        private readonly CleanArchitectureContext _cleanArchitectureContext;
        private readonly ProductRepository _productRepository;
        private readonly ITestOutputHelper _testOutputHelper;
        private ProductBuilder ProductBuilder { get; } = new ProductBuilder();
        public ProductTests(ITestOutputHelper testOutputHelper)
        {
            _testOutputHelper = testOutputHelper;
            var dbOptions = new DbContextOptionsBuilder<CleanArchitectureContext>()
                .UseInMemoryDatabase(databaseName: "CleanArchitecture")
                .Options;
            _cleanArchitectureContext = new CleanArchitectureContext(dbOptions);
            _productRepository = new ProductRepository(_cleanArchitectureContext);
        }

        [Fact]
        public async Task Get_Product_By_Name()
        {
            var product = ProductBuilder.Build();
            _cleanArchitectureContext.Products.Add(product);
            await _cleanArchitectureContext.SaveChangesAsync();

            _testOutputHelper.WriteLine($"ProductName: {product.ProductName}");

            var productByName = await _productRepository.GetProductByNameAsync(product.ProductName);
            Assert.Equal(ProductBuilder.TestProductName, productByName.ToList().First().ProductName);
        }
        
    }
}
