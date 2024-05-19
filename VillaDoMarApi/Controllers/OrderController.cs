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
            Order newOrder = new() { Products = order.Products, TotalValue = totalValue, Client = order.Client};
            _context.Orders.Add(newOrder);
            await _context.SaveChangesAsync();
            return Ok(order);
        }

        [HttpPut]
        [Route("EditOrder")]
        public async Task<ActionResult<Order>> EditOrder(OrderIdDto order)
        {
            var totalValue = order.Products.Sum(x => x.Value);
            Order newOrder = new() { Id = order.Id, TotalValue = totalValue, Client = order.Client };
            var oldOrder = _context.Products.SingleOrDefault(p => p.Id == order.Id);
            if (oldOrder is null)
                return NotFound("Pedido não encontrado");
            _context.Entry(oldOrder).CurrentValues.SetValues(newOrder);
            await _context.SaveChangesAsync();
            return Ok(order);
        }

        [HttpDelete]
        [Route("DeleteOrder")]
        public async Task<ActionResult> DeleteOrder(int id)
        {
            var order = _context.Orders.SingleOrDefault(p => p.Id == id);
            if (order is null)
                return NotFound("Pedido não encontrado");
            _context.Orders.Remove(order);
            await _context.SaveChangesAsync();
            return Ok("Pedido deletado com sucesso!");
        }
    }
}
