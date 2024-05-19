using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VillaDoMarApi.Data;
using VillaDoMarApi.Dtos;
using VillaDoMarApi.Entities;

namespace VillaDoMarApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CaixasController : ControllerBase
    {
        private readonly DataContext _context;

        public CaixasController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("ResumoCaixa")]
        public async Task<ActionResult<object>> ResumoCaixa()
        {
            var totalEntradas = await _context.Caixas
                .Where(c => c.TipoMovimento == "Entrada")
                .SumAsync(c => c.Valor);

            var totalSaidas = await _context.Caixas
                .Where(c => c.TipoMovimento == "Saida")
                .SumAsync(c => c.Valor);

            var Saldo = totalEntradas - totalSaidas;

            return new
            {
                totalEntradas,
                totalSaidas,
                Saldo
            };
        }

        [HttpGet]
        [Route("GetCaixas")]
        public async Task<ActionResult<List<Caixa>>> GetCaixas()
        {
            var caixas = await _context.Caixas.ToListAsync();
            return Ok(caixas);
        }

        [HttpGet]
        [Route("GetCaixa")]
        public async Task<ActionResult<Caixa>> GetCaixa(int id)
        {
            var caixa = await _context.Caixas.SingleOrDefaultAsync(c => c.Id == id);
            if (caixa is null)
                return NotFound("Transação não encontrada");
            return Ok(caixa);
        }

        [HttpPost]
        [Route("InsertCaixa")]
        public async Task<ActionResult<Caixa>> InsertCaixa(CaixaDto caixa)
        {
            if (!await CaixaEstaAberto())
                return BadRequest("O caixa está fechado, não é possível adicionar transações");

            Caixa newCaixa = new()
            {
                Name = caixa.Name,
                Cliente = caixa.Cliente,
                Data = caixa.Data,
                Valor = caixa.Valor,
                TipoPagamento = caixa.TipoPagamento,
                TipoMovimento = caixa.TipoMovimento
            };
            _context.Caixas.Add(newCaixa);
            await _context.SaveChangesAsync();
            return Ok(caixa);
        }

        [HttpPut]
        [Route("EditCaixa")]
        public async Task<ActionResult<Caixa>> EditCaixa(CaixaIdDto caixa)
        {
            if (!await CaixaEstaAberto())
                return BadRequest("O caixa está fechado, não é possível editar transações");

            Caixa newCaixa = new()
            {
                Id = caixa.Id,
                Name = caixa.Name,
                Cliente = caixa.Cliente,
                Data = caixa.Data,
                Valor = caixa.Valor,
                TipoPagamento = caixa.TipoPagamento,
                TipoMovimento = caixa.TipoMovimento
            };
            var oldCaixa = _context.Caixas.SingleOrDefault(c => c.Id == caixa.Id);
            if (oldCaixa is null)
                return NotFound("Transação não encontrada");
            _context.Entry(oldCaixa).CurrentValues.SetValues(newCaixa);
            await _context.SaveChangesAsync();
            return Ok(caixa);
        }

        [HttpDelete]
        [Route("DeleteCaixa")]
        public async Task<ActionResult<Caixa>> DeleteCaixa(int id)
        {
            if (!await CaixaEstaAberto())
                return BadRequest("O caixa está fechado, não é possível excluir transações");

            var caixa = _context.Caixas.SingleOrDefault(c => c.Id == id);
            if (caixa is null)
                return NotFound("Transação não encontrada");
            _context.Caixas.Remove(caixa);
            await _context.SaveChangesAsync();
            return Ok("Transação deletada com sucesso!");
        }

        [HttpGet]
        [Route("StatusCaixa")]
        public async Task<ActionResult<StatusCaixaDto>> StatusCaixa()
        {

            var statusCaixa = await _context.StatusCaixas.OrderByDescending(c => c.Id).FirstOrDefaultAsync();
            if (statusCaixa == null)
                return NotFound("Nenhum caixa encontrado");

            var statusCaixaDto = new StatusCaixaDto
            {
                Id = statusCaixa.Id,
                Aberto = statusCaixa.Aberto,
                DataHoraAbertura = statusCaixa.DataHoraAbertura,
                DataHoraFechamento = statusCaixa.DataHoraFechamento
            };

            return Ok(statusCaixaDto);
        }


        [HttpPost]
        [Route("AbrirCaixa")]
        public async Task<ActionResult> AbrirCaixa()
        {
            var caixaAberto = await _context.StatusCaixas.AnyAsync(c => c.Aberto);
            if (caixaAberto)
                return BadRequest("Já existe um caixa aberto");

            var statusCaixa = new StatusCaixa
            {
                Aberto = true,
                DataHoraAbertura = DateTime.Now,
                DataHoraFechamento = null
            };
            _context.StatusCaixas.Add(statusCaixa);
            await _context.SaveChangesAsync();
            return Ok("Caixa aberto com sucesso");
        }

        [HttpPost]
        [Route("FecharCaixa")]
        public async Task<ActionResult> FecharCaixa()
        {
            var caixaAberto = await _context.StatusCaixas.SingleOrDefaultAsync(c => c.Aberto);
            if (caixaAberto == null)
                return BadRequest("Não existe um caixa aberto");

            caixaAberto.Aberto = false;
            caixaAberto.DataHoraFechamento = DateTime.Now;
            await _context.SaveChangesAsync();
            return Ok("Caixa fechado com sucesso");
        }

        [HttpGet]
        [Route("HistóricoCaixas")]
        public async Task<ActionResult<List<StatusCaixaDto>>> HistóricoCaixas()
        {
            var statusCaixas = await _context.StatusCaixas.ToListAsync();
            var statusCaixasDto = statusCaixas.Select(statusCaixa => new StatusCaixaDto
            {
                Id = statusCaixa.Id,
                Aberto = statusCaixa.Aberto,
                DataHoraAbertura = statusCaixa.DataHoraAbertura,
                DataHoraFechamento = statusCaixa.DataHoraFechamento
            }).ToList();
            return Ok(statusCaixasDto);
        }


        private async Task<bool> CaixaEstaAberto()
        {
            var statusCaixa = await _context.StatusCaixas.SingleOrDefaultAsync();
            return statusCaixa?.Aberto ?? false;
        }
    }
}
