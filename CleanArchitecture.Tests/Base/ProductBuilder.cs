using CleanArchitecture.Domain.Entities;

namespace CleanArchitecture.Tests.Base
{
    public class ProductBuilder
    {
        public int TestProductId => 5;
        public string TestProductName => "Test product name";
        public decimal? TestProductUnitPrice => 5.5m;
        public short? TestProductUnitsInStock => 6;
        public short? TestProductUnitsOnOrder => 3;

        public Product Build()
        {
            return new Product()
            {
                Id = TestProductId,
                ProductName = TestProductName,
                UnitPrice = TestProductUnitPrice,
                UnitsInStock = TestProductUnitsInStock,
                UnitsOnOrder = TestProductUnitsOnOrder
            };
        }
    }
}
