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
    [Route("api/estoque")]
    [ApiController]
    public class EstoqueController : ControllerBase
    {
        private readonly TokDCerejaDbContext _context;

        public EstoqueController(TokDCerejaDbContext context)
        {
            _context = context;
        }

        // POST: api/estoque
        [HttpPost]
        public async Task<IActionResult> CriarEstoque(Estoque estoque)
        {
            estoque.Id = Guid.NewGuid();
            estoque.IsDeleted = false;
            estoque.DataUltimaAtualizacao = DateTime.UtcNow;

            _context.Estoques.Add(estoque);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(ObterEstoquePorId), new { id = estoque.Id }, estoque);
        }

        // GET: api/estoque
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Estoque>>> ObterEstoques()
        {
            return await _context.Estoques
                .Where(e => !e.IsDeleted)
                .ToListAsync();
        }

        // GET: api/estoque/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Estoque>> ObterEstoquePorId(Guid id)
        {
            var estoque = await _context.Estoques
                .Where(e => e.Id == id && !e.IsDeleted)
                .FirstOrDefaultAsync();

            if (estoque == null)
            {
                return NotFound();
            }

            return Ok(estoque);
        }

        // PUT: api/estoque/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> AtualizarEstoque(Guid id, Estoque estoqueAtualizado)
        {
            if (id != estoqueAtualizado.Id)
            {
                return BadRequest("IDs do estoque não coincidem.");
            }

            var estoque = await _context.Estoques
                .Where(e => e.Id == id && !e.IsDeleted)
                .FirstOrDefaultAsync();

            if (estoque == null)
            {
                return NotFound();
            }

            // Atualizar campos permitidos
            estoque.Ingrediente = estoqueAtualizado.Ingrediente;
            estoque.QuantidadeMin = estoqueAtualizado.QuantidadeMin;
            estoque.QuantidadeMax = estoqueAtualizado.QuantidadeMax;
            estoque.QuantidadeAtual = estoqueAtualizado.QuantidadeAtual;
            estoque.Medida = estoqueAtualizado.Medida;

            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/estoque/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DesativarEstoque(Guid id)
        {
            var estoque = await _context.Estoques
                .Where(e => e.Id == id && !e.IsDeleted)
                .FirstOrDefaultAsync();

            if (estoque == null)
            {
                return NotFound();
            }

            // Marcar o estoque como desativado (exclusão lógica)
            estoque.IsDeleted = true;
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
