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
    [Route("api/tabela-nutricional")]
    [ApiController]
    public class TabelaNutricionalController : ControllerBase
    {
        private readonly TokDCerejaDbContext _context;

        public TabelaNutricionalController(TokDCerejaDbContext context)
        {
            _context = context;
        }

        // POST: api/tabela-nutricional
        [HttpPost]
        public async Task<IActionResult> CriarTabelaNutricional(TabelaNutricional tabelaNutricional)
        {
            tabelaNutricional.Id = Guid.NewGuid();

            _context.TabelasNutricionais.Add(tabelaNutricional);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(ObterTabelaNutricionalPorId), new { id = tabelaNutricional.Id }, tabelaNutricional);
        }

        // GET: api/tabela-nutricional
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TabelaNutricional>>> ObterTabelasNutricionais()
        {
            return await _context.TabelasNutricionais.ToListAsync();
        }

        // GET: api/tabela-nutricional/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<TabelaNutricional>> ObterTabelaNutricionalPorId(Guid id)
        {
            var tabelaNutricional = await _context.TabelasNutricionais.FindAsync(id);

            if (tabelaNutricional == null)
            {
                return NotFound();
            }

            return Ok(tabelaNutricional);
        }

        // PUT: api/tabela-nutricional/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> AtualizarTabelaNutricional(Guid id, TabelaNutricional tabelaNutricionalAtualizada)
        {
            if (id != tabelaNutricionalAtualizada.Id)
            {
                return BadRequest("IDs da tabela nutricional não coincidem.");
            }

            _context.Entry(tabelaNutricionalAtualizada).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TabelaNutricionalExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // DELETE: api/tabela-nutricional/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> ExcluirTabelaNutricional(Guid id)
        {
            var tabelaNutricional = await _context.TabelasNutricionais.FindAsync(id);
            if (tabelaNutricional == null)
            {
                return NotFound();
            }

            _context.TabelasNutricionais.Remove(tabelaNutricional);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TabelaNutricionalExists(Guid id)
        {
            return _context.TabelasNutricionais.Any(e => e.Id == id);
        }
    }
}
