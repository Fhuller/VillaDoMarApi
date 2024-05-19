using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VillaDoMarApi.Data;
using VillaDoMarApi.Dtos;
using VillaDoMarApi.Entities;

namespace VillaDoMarApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StorageController : ControllerBase
    {
        private readonly DataContext _context;

        public StorageController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("GetAllStorage")]
        public async Task<ActionResult<List<Storage>>> GetAllStorage()
        {
            var storage = await _context.Storage.ToListAsync();
            return Ok(storage);
        }

        [HttpGet]
        [Route("GetStorage")]
        public async Task<ActionResult<List<Storage>>> GetStorage(int id)
        {
            var storage = await _context.Storage.SingleOrDefaultAsync(p => p.Id == id);
            if (storage is null)
                return NotFound("Estoque não encontrado");
            return Ok(storage);
        }

        [HttpPost]
        [Route("InsertStorage")]
        public async Task<ActionResult<Product>> InsertStorage(StorageDto storage)
        {
            Storage newStorage= new() { ProductId = storage.ProductId, ClientId = storage.ClientId, Amount = storage.Amount };
            _context.Storage.Add(newStorage);
            await _context.SaveChangesAsync();
            return Ok(newStorage);
        }

        [HttpPut]
        [Route("EditStorage")]
        public async Task<ActionResult<Product>> EditStorage(Storage storage)
        {
            var oldStorage = _context.Storage.SingleOrDefault(p => p.Id == storage.Id);
            if (oldStorage is null)
                return NotFound("Estoque não encontrado");
            _context.Entry(oldStorage).CurrentValues.SetValues(storage);
            await _context.SaveChangesAsync();
            return Ok(storage);
        }

        [HttpDelete]
        [Route("DeleteStorage")]
        public async Task<ActionResult> DeleteStorage(int id)
        {
            var storage = _context.Storage.SingleOrDefault(p => p.Id == id);
            if (storage is null)
                return NotFound("Estoque não encontrado");
            _context.Storage.Remove(storage);
            await _context.SaveChangesAsync();
            return Ok("Estoque deletado com sucesso!");
        }
    }
}
