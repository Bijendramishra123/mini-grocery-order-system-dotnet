using GroceryOrderApi.Models;

namespace GroceryOrderApi.Repositories
{
    public interface IOrderRepository
    {
        Task AddAsync(Order order);
    }
}
