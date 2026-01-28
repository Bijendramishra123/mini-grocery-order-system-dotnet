using GroceryOrderApi.Models;

namespace GroceryOrderApi.Repositories
{
    public interface IProductRepository
    {
        Task<Product?> GetByIdAsync(int id);
        Task UpdateAsync(Product product);
    }
}
