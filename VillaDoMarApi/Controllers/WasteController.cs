using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VillaDoMarApi.Data;
using VillaDoMarApi.Dtos;
using VillaDoMarApi.Entities;


namespace VillaDoMarApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WasteController : Controller
    {
        private readonly DataContext _context;

        public WasteController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("GetWastes")]
        public async Task<ActionResult<List<Waste>>> GetWaste()
        {
            var wastes = await _context.Wastes.ToListAsync();
            return Ok(wastes);
        }

        [HttpGet]
        [Route("GetWaste")]
        public async Task<ActionResult<List<Waste>>> GetWaste(int id)
        {
           var waste = await _context.Wastes.SingleOrDefaultAsync(w => w.Id == id );
           if (waste is null)
                { return NotFound("Residuo não encontrado"); }
           return Ok(waste);
        }

        [HttpPost]
        [Route("InsertWaste")]
        public async Task<ActionResult<Waste>> InsertWaste(WasteDto dto)
        {
            Waste newWaste = new() { Name = dto.Name };
            _context.Wastes.Add(newWaste);
            await _context.SaveChangesAsync();
            return Ok(newWaste);
        }

        [HttpPut]
        [Route("EditWaste")]
        public async Task<ActionResult<Waste>> EditWaste(Waste waste)
        {
            var oldWasteValue = _context.Wastes.SingleOrDefault(w => w.Id == waste.Id);
            if(oldWasteValue is null)
                {
                return NotFound("Residuo não encontrado");
                }
            _context.Entry(oldWasteValue).CurrentValues.SetValues(waste);
            await _context.SaveChangesAsync();
            return Ok(waste);
        }

        [HttpDelete]
        [Route("DeletWaste")]
        public async Task<ActionResult> DeleteWaste(int id)
        {
            var Waste = _context.Wastes.SingleOrDefault(w => w.Id == id);
            if(Waste is null)
            {
                return NotFound("Residuo não encontrado");
            }
            _context.Wastes.Remove(Waste);
            await _context.SaveChangesAsync();
            return Ok("Residuo deletado com sucesso");
        }  
    }
}
