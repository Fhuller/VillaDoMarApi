using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NuGet.Versioning;
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
        [Route("GetProductsWithAmount")]
        public async Task<ActionResult<List<Product>>> GetProductsWithAmount()
        {
            var productsWithTotalAmounts = await _context.Products
                .Select(product => new
                {
                    Product = new { 
                        productId = product.Id,
                        name = product.Name,
                        description = product.Description,
                        value = product.Value,
                        weight = product.Weight,
                        typeProductId = product.TypeProductId,
                        typeProduct = _context.TypeProduct.FirstOrDefault(x => x.Id == product.TypeProductId).Name,
                        supplierProductId = product.SupplierProductId,
                        supplierProduct = _context.Suppliers.FirstOrDefault(x => x.Id == product.SupplierProductId),
                    },
                    TotalAmount = _context.ProductMovements
                        .Where(pm => pm.ProductId == product.Id)
                        .Sum(pm => pm.IsEntry ? pm.MovementAmount : -pm.MovementAmount),
                })
                .ToListAsync();

            return Ok(productsWithTotalAmounts);
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

        [HttpGet]
        [Route("GetProductHistory")]
        public async Task<ActionResult<List<Product>>> GetProductHistory(int id)
        {
            var product = await _context.ProductMovements.Where(p => p.ProductId == id).ToListAsync();
            if (product is null)
                return NotFound("Produto não encontrado");
            return Ok(product);
        }

        [HttpPost]
        [Route("ProductMovement")]
        public async Task<ActionResult<Product>> ProductMovement(ProductMovementDto movement)
        {
            ProductMovements newMovement = new()
            {
                ProductId = movement.ProductId,
                MovementAmount = movement.Amount,
                IsEntry = movement.IsEntry,
                MovementDate = DateTime.Now,
                Description = movement.Description ?? ""
            };
            _context.ProductMovements.Add(newMovement);
            await _context.SaveChangesAsync();
            return Ok(newMovement);
        }

        [HttpPost]
        [Route("InsertProduct")]
        public async Task<ActionResult<Product>> InsertProducts(ProductDto product)
        {
            Product newProduct = new() {
                Name = product.Name, 
                Description = product.Description,
                Value = product.Value,
                Weight = product.Weight,
                TypeProductId = product.TypeProductID,
            };
            _context.Products.Add(newProduct);
            await _context.SaveChangesAsync();
            return Ok(product);
        }

        [HttpPut]
        [Route("EditProduct")]
        public async Task<ActionResult<Product>> EditProducts(ProductIdDto product)
        {
            Product newProduct = new()
            {
                Name = product.Name,
                Description = product.Description,
                Value = product.Value,
                Weight = product.Weight,
                TypeProductId = product.TypeProductID,
            };
            var oldProduct = _context.Products.SingleOrDefault(p => p.Id == product.Id);
            if(oldProduct is null)
                return NotFound("Produto não encontrado");
            newProduct.Id = oldProduct.Id;
            _context.Entry(oldProduct).CurrentValues.SetValues(newProduct);
            await _context.SaveChangesAsync();
            return Ok(product);
        }

        [HttpDelete]
        [Route("DeleteProduct")]
        public async Task<ActionResult> DeleteProduct(int id)
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
