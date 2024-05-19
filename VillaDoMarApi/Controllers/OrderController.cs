using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VillaDoMarApi.Data;
using VillaDoMarApi.Dtos;
using VillaDoMarApi.Entities;

namespace VillaDoMarApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly DataContext _context;

        public OrderController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("GetOrders")]
        public async Task<ActionResult<List<Order>>> GetOrders()
        {
            var orders = await _context.Orders.ToListAsync();
            return Ok(orders);
        }

        [HttpGet]
        [Route("GetOrder")]
        public async Task<ActionResult<List<Order>>> GetOrder(int id)
        {
            var order = await _context.Orders.SingleOrDefaultAsync(p => p.Id == id);
            if (order is null)
                return NotFound("Pedido não encontrado");
            return Ok(order);
        }

        [HttpPost]
        [Route("CreateOrder")]
        public async Task<ActionResult<Order>> CreateOrder(OrderDto order)
        {
            var totalValue = order.Products.Sum(x => x.Value);
            Order newOrder = new() { Products = order.Products, TotalValue = totalValue};
            _context.Orders.Add(newOrder);
            await _context.SaveChangesAsync();
            return Ok(order);
        }

        [HttpPut]
        [Route("EditOrder")]
        public async Task<ActionResult<Product>> EditOrder(ProductIdDto product)
        {
            Product newProduct = new() { Id = product.Id, Name = product.Name, Description = product.Description };
            var oldProduct = _context.Products.SingleOrDefault(p => p.Id == product.Id);
            if (oldProduct is null)
                return NotFound("Produto não encontrado");
            _context.Entry(oldProduct).CurrentValues.SetValues(newProduct);
            await _context.SaveChangesAsync();
            return Ok(product);
        }

        [HttpDelete]
        [Route("DeleteProduct")]
        public async Task<ActionResult> DeleteProducts(int id)
        {
            var product = _context.Products.SingleOrDefault(p => p.Id == id);
            if (product is null)
                return NotFound("Produto não encontrado");
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
            return Ok("Produto deletado com sucesso!");
        }
    }
}
