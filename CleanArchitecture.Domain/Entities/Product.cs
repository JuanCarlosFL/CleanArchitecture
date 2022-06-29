using CleanArchitecture.Domain.Entities.Base;

namespace CleanArchitecture.Domain.Entities
{
    public class Product : BaseEntity
    {
        public string ProductName { get; set; }
        public decimal? UnitPrice { get; set; }
        public short? UnitsInStock { get; set; }
        public short? UnitsOnOrder { get; set; }
    }
}
