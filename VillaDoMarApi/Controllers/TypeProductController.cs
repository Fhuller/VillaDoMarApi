using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VillaDoMarApi.Data;
using VillaDoMarApi.Dtos;
using VillaDoMarApi.Entities;

namespace VillaDoMarApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TypeProductController : ControllerBase
    {
        private readonly DataContext _context;

        public TypeProductController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("GetTypeProducts")]
        public async Task<ActionResult<List<TypeProduct>>> GetT ypeProducts()
        {
            var typeProducts = await _context.TypeProduct.ToListAsync();
            return Ok(typeProducts);
        }

        [HttpGet]
        [Route("GetTypeProduct")]
        public async Task<ActionResult<List<TypeProduct>>> GetTypeProduct(int id)
        {
            var typeProduct = await _context.TypeProduct.SingleOrDefaultAsync(p => p.Id == id);
            if (typeProduct is null)
                return NotFound("Tipo de produto não encontrado");
            return Ok(typeProduct);
        }

        [HttpPost]
        [Route("InsertTypeProduct")]
        public async Task<ActionResult<TypeProduct>> InsertStorage(TypeProductDto dto)
        {
            TypeProduct newTypeProduct = new() { Name = dto.Name };
            _context.TypeProduct.Add(newTypeProduct);
            await _context.SaveChangesAsync();
            return Ok(newTypeProduct);
        }

        [HttpPut]
        [Route("EditTypeProduct")]
        public async Task<ActionResult<TypeProduct>> EditTypeProduct(TypeProduct typeProduct)
        {
            var oldStorage = _context.TypeProduct.SingleOrDefault(p => p.Id == typeProduct.Id);
            if (oldStorage is null)
                return NotFound("Tipo de produto não encontrado");
            _context.Entry(oldStorage).CurrentValues.SetValues(typeProduct);
            await _context.SaveChangesAsync();
            return Ok(typeProduct);
        }

        [HttpDelete]
        [Route("DeleteTypeProduct")]
        public async Task<ActionResult> DeleteStorage(int id)
        {
            var typeProduct = _context.TypeProduct.SingleOrDefault(p => p.Id == id);
            if (typeProduct is null)
                return NotFound("Tipo de produto não encontrado");
            _context.TypeProduct.Remove(typeProduct);
            await _context.SaveChangesAsync();
            return Ok("Tipo de produto deletado com sucesso!");
        }
    }
}
