using GroceryOrderApi.Data;
using GroceryOrderApi.Models;
using GroceryOrderApi.Repositories;
using Microsoft.EntityFrameworkCore;

namespace GroceryOrderApi.Services
{
    public class OrderService : IOrderService
    {
        private readonly AppDbContext _context;
        private readonly IProductRepository _productRepo;
        private readonly IOrderRepository _orderRepo;

        public OrderService(
            AppDbContext context,
            IProductRepository productRepo,
            IOrderRepository orderRepo)
        {
            _context = context;
            _productRepo = productRepo;
            _orderRepo = orderRepo;
        }

        public async Task<(bool Success, string Message)> PlaceOrderAsync(int productId, int quantity)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
                var product = await _productRepo.GetByIdAsync(productId);
                if (product == null)
                    return (false, "Product not found");

                if (product.Stock < quantity)
                    return (false, "Insufficient stock");

                // Reduce stock
                product.Stock -= quantity;
                await _productRepo.UpdateAsync(product);

                // Create order
                var order = new Order
                {
                    ProductId = productId,
                    Quantity = quantity,
                    TotalPrice = product.Price * quantity
                };

                await _orderRepo.AddAsync(order);

                await transaction.CommitAsync();
                return (true, "Order placed successfully");
            }
            catch
            {
                await transaction.RollbackAsync();
                return (false, "Order failed");
            }
        }
    }
}
