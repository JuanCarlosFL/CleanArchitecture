using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.Repositories;
using CleanArchitecture.Infraestructure.Data;
using CleanArchitecture.Infraestructure.Repository.Base;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Infraestructure.Repository
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        public ProductRepository(CleanArchitectureContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Product>> GetProductByNameAsync(string productName)
        {
            return await _context.Products
                .Where(p => p.ProductName.Contains(productName))
                .ToListAsync();
        }
    }
}
