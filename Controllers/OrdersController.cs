using Microsoft.AspNetCore.Mvc;
using GroceryOrderApi.Services;

namespace GroceryOrderApi.Controllers
{
    [ApiController]
    [Route("orders")]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrdersController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpPost]
        public async Task<IActionResult> PlaceOrder([FromBody] OrderRequest request)
        {
            var result = await _orderService.PlaceOrderAsync(
                request.ProductId,
                request.Quantity
            );

            if (!result.Success)
                return BadRequest(new { message = result.Message });

            return Ok(new { message = result.Message });
        }
    }

    public class OrderRequest
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }
    }
}
