using Microsoft.AspNetCore.Mvc;
using GroceryOrderApi.Data;
using Microsoft.EntityFrameworkCore;

namespace GroceryOrderApi.Controllers
{
    [ApiController]
    [Route("products")]
    public class ProductsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ProductsController(AppDbContext context)
        {
            _context = context;
        }

        // GET /products
        [HttpGet]
        public async Task<IActionResult> GetProducts()
        {
            var products = await _context.Products.ToListAsync();
            return Ok(products);
        }
    }
}
