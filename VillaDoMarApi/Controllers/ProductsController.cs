using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VillaDoMarApi.Data;
using VillaDoMarApi.Dtos;
using VillaDoMarApi.Entities;

namespace VillaDoMarApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly DataContext _context;

        public ProductsController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("GetProducts")]
        public async Task<ActionResult<List<Product>>> GetProducts()
        {
            var products = await _context.Products.ToListAsync();
            return Ok(products);
        }

        [HttpGet]
        [Route("GetProduct")]
        public async Task<ActionResult<List<Product>>> GetProduct(int id)
        {
            var product = await _context.Products.SingleOrDefaultAsync(p => p.Id == id);
            if (product is null)
                return NotFound("Produto não encontrado");
            return Ok(product);
        }

        [HttpPost]
        [Route("InsertProduct")]
        public async Task<ActionResult<Product>> InsertProducts(ProductDto product)
        {
            Product newProduct = new() { Name = product.Name, Description = product.Description };
            _context.Products.Add(newProduct);
            await _context.SaveChangesAsync();
            return Ok(product);
        }

        [HttpPut]
        [Route("EditProduct")]
        public async Task<ActionResult<Product>> EditProducts(ProductIdDto product)
        {
            Product newProduct = new() { Id = product.Id, Name = product.Name, Description = product.Description };
            var oldProduct = _context.Products.SingleOrDefault(p => p.Id == product.Id);
            if(oldProduct is null)
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
