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
    public class FinancialController : ControllerBase
    {
        private readonly DataContext _context;

        public FinancialController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("FinancialSummary")]
        public async Task<ActionResult<object>> ResumoCaixa()
        {
            var totalEntradas = await _context.Financials
                .Where(c => c.MoveType == "Entrada")
                .SumAsync(c => c.Value);

            var totalSaidas = await _context.Financials
                .Where(c => c.MoveType == "Saida")
                .SumAsync(c => c.Value);

            var Saldo = totalEntradas - totalSaidas;

            return new
            {
                totalEntradas,
                totalSaidas,
                Saldo
            };
        }

        [HttpGet]
        [Route("GetFinancials")]
        public async Task<ActionResult<List<Financial>>> GetCaixas()
        {
            var caixas = await _context.Financials.ToListAsync();
            return Ok(caixas);
        }

        [HttpGet]
        [Route("GetFinancial")]
        public async Task<ActionResult<Financial>> GetCaixa(int id)
        {
            var caixa = await _context.Financials.SingleOrDefaultAsync(c => c.Id == id);
            if (caixa is null)
                return NotFound("Transação não encontrada");
            return Ok(caixa);
        }

        [HttpPost]
        [Route("InsertFinancial")]
        public async Task<ActionResult<Financial>> InsertCaixa(CaixaDto caixa)
        {
            if (!await CaixaEstaAberto())
                return BadRequest("O caixa está fechado, não é possível adicionar transações");

            Financial newCaixa = new()
            {
                Date = caixa.Date,
                Value = caixa.Value,
                PaymentType = caixa.PaymentType,
                MoveType = caixa.MoveType
            };
            _context.Financials.Add(newCaixa);
            await _context.SaveChangesAsync();
            return Ok(caixa);
        }

        [HttpPut]
        [Route("EditFinancial")]
        public async Task<ActionResult<Financial>> EditCaixa(CaixaIdDto caixa)
        {
            if (!await CaixaEstaAberto())
                return BadRequest("O caixa está fechado, não é possível editar transações");

            Financial newCaixa = new()
            {
                Id = caixa.Id,
                Date = caixa.Date,
                Value = caixa.Value,
                PaymentType = caixa.PaymentType,
                MoveType = caixa.MoveType
            };
            var oldCaixa = _context.Financials.SingleOrDefault(c => c.Id == caixa.Id);
            if (oldCaixa is null)
                return NotFound("Transação não encontrada");
            _context.Entry(oldCaixa).CurrentValues.SetValues(newCaixa);
            await _context.SaveChangesAsync();
            return Ok(caixa);
        }

        [HttpDelete]
        [Route("DeleteFinancial")]
        public async Task<ActionResult<Financial>> DeleteCaixa(int id)
        {
            if (!await CaixaEstaAberto())
                return BadRequest("O caixa está fechado, não é possível excluir transações");

            var caixa = _context.Financials.SingleOrDefault(c => c.Id == id);
            if (caixa is null)
                return NotFound("Transação não encontrada");
            _context.Financials.Remove(caixa);
            await _context.SaveChangesAsync();
            return Ok("Transação deletada com sucesso!");
        }

        [HttpGet]
        [Route("FinancialStatus")]
        public async Task<ActionResult<StatusCaixaDto>> StatusCaixa()
        {

            var statusCaixa = await _context.FinancialStatus.OrderByDescending(c => c.Id).FirstOrDefaultAsync();
            if (statusCaixa == null)
                return NotFound("Nenhum caixa encontrado");

            var statusCaixaDto = new StatusCaixaDto
            {
                Id = statusCaixa.Id,
                Status = statusCaixa.Status,
                OpenDate = statusCaixa.OpenDate,
                CloseDate = statusCaixa.CloseDate
            };

            return Ok(statusCaixaDto);
        }


        [HttpPost]
        [Route("OpenFinancial")]
        public async Task<ActionResult> AbrirCaixa()
        {
            var caixaAberto = await _context.FinancialStatus.AnyAsync(c => c.Status);
            if (caixaAberto)
                return BadRequest("Já existe um caixa aberto");

            var statusCaixa = new FinancialStatus
            {
                Status = true,
                OpenDate = DateTime.Now,
                CloseDate = null
            };
            _context.FinancialStatus.Add(statusCaixa);
            await _context.SaveChangesAsync();
            return Ok("Caixa aberto com sucesso");
        }

        [HttpPost]
        [Route("CloseFinancial")]
        public async Task<ActionResult> FecharCaixa()
        {
            var caixaAberto = await _context.FinancialStatus.SingleOrDefaultAsync(c => c.Status);
            if (caixaAberto == null)
                return BadRequest("Não existe um caixa aberto");

            caixaAberto.Status = false;
            caixaAberto.CloseDate = DateTime.Now;
            await _context.SaveChangesAsync();
            return Ok("Caixa fechado com sucesso");
        }

        [HttpGet]
        [Route("History")]
        public async Task<ActionResult<List<StatusCaixaDto>>> History()
        {
            var statusCaixas = await _context.FinancialStatus.ToListAsync();
            var statusCaixasDto = statusCaixas.Select(statusCaixa => new StatusCaixaDto
            {
                Id = statusCaixa.Id,
                Status = statusCaixa.Status,
                OpenDate = statusCaixa.OpenDate,
                CloseDate = statusCaixa.CloseDate
            }).ToList();
            return Ok(statusCaixasDto);
        }


        private async Task<bool> CaixaEstaAberto()
        {
            var statusCaixa = await _context.FinancialStatus.SingleOrDefaultAsync();
            return statusCaixa?.Status ?? false;
        }
    }
}
