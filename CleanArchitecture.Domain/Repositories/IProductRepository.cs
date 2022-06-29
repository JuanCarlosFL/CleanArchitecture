using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.Repositories.Base;

namespace CleanArchitecture.Domain.Repositories
{
    public interface IProductRepository : IRepository<Product>
    {
        Task<IEnumerable<Product>> GetProductByNameAsync(string productName);
    }
}
