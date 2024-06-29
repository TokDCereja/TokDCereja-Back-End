using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TokDCereja_back_end.Data;
using TokDCereja_back_end.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TokDCereja_back_end.Controllers
{
    [Route("api/caixa")]
    [ApiController]
    public class CaixaController : ControllerBase
    {
        private readonly TokDCerejaDbContext _context;

        public CaixaController(TokDCerejaDbContext context)
        {
            _context = context;
        }

        // POST: api/caixa
        [HttpPost]
        public async Task<IActionResult> CriarCaixa(Caixa caixa)
        {
            caixa.Id = Guid.NewGuid();
            caixa.IsDeleted = false;
            caixa.DataUltimaAtualizacao = DateTime.UtcNow;

            _context.Caixas.Add(caixa);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(ObterCaixaPorId), new { id = caixa.Id }, caixa);
        }

        // GET: api/caixa
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Caixa>>> ObterCaixas()
        {
            return await _context.Caixas
               .Where(c => !c.IsDeleted)
               .ToListAsync();
        }

        // GET: api/caixa/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Caixa>> ObterCaixaPorId(Guid id)
        {
            var caixa = await _context.Caixas
                .Where(c => c.Id == id && !c.IsDeleted)
                .FirstOrDefaultAsync();

            if (caixa == null)
            {
                return NotFound();
            }

            return Ok(caixa);
        }

        // PUT: api/caixa/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> AtualizarCaixa(Guid id, Caixa caixaAtualizado)
        {
            if (id != caixaAtualizado.Id)
            {
                return BadRequest("IDs do caixa não coincidem.");
            }

            var caixa = await _context.Caixas
                .Where(c => c.Id == id && !c.IsDeleted)
                .FirstOrDefaultAsync();

            if (caixa == null)
            {
                return NotFound();
            }

            // Atualizar campos permitidos
            caixa.FerramentaId = caixaAtualizado.FerramentaId;
            caixa.CustoFixo = caixaAtualizado.CustoFixo;
            caixa.CustoUnidade = caixaAtualizado.CustoUnidade;
            caixa.TotalVenda = caixaAtualizado.TotalVenda;
            caixa.FundoReserva = caixaAtualizado.FundoReserva;
            caixa.CapitalDeGiro = caixaAtualizado.CapitalDeGiro;

            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/caixa/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DesativarCaixa(Guid id)
        {
            var caixa = await _context.Caixas
                .Where(c => c.Id == id && !c.IsDeleted)
                .FirstOrDefaultAsync();

            if (caixa == null)
            {
                return NotFound();
            }

            // Marcar o caixa como desativado (exclusão lógica)
            caixa.IsDeleted = true;
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
