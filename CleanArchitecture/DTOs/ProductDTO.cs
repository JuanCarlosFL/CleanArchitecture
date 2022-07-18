using CleanArchitecture.DTOs.Base;

namespace CleanArchitecture.DTOs
{
    public class ProductDTO : BaseDTO
    {
        public string ProductName { get; set; }
        public decimal? UnitPrice { get; set; }
        public short? UnitsInStock { get; set; }
        public short? UnitsOnOrder { get; set; }
    }
}
