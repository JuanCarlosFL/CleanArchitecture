using CleanArchitecture.Domain.Entities;

namespace CleanArchitecture.Application.Interfaces
{
    public interface IProductService
    {
        Task<IEnumerable<Product>> GetProductList();
        Task<Product> GetProductById(int productId);
        Task<IEnumerable<Product>> GetProductByName(string productName);
        Task<Product> Create(Product product);
        Task Update(Product product);
        Task Delete(Product product);

    }
}
