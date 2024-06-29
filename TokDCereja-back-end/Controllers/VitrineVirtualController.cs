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
    [Route("api/vitrine-virtual")]
    [ApiController]
    public class VitrineVirtualController : ControllerBase
    {
        private readonly TokDCerejaDbContext _context;

        public VitrineVirtualController(TokDCerejaDbContext context)
        {
            _context = context;
        }

        // POST: api/vitrine-virtual
        [HttpPost]
        public async Task<IActionResult> CriarVitrineVirtual(VitrineVirtual vitrineVirtual)
        {
            vitrineVirtual.Id = Guid.NewGuid();
            vitrineVirtual.IsDeleted = false;

            _context.VitrinesVirtuais.Add(vitrineVirtual);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(ObterVitrineVirtualPorId), new { id = vitrineVirtual.Id }, vitrineVirtual);
        }

        // GET: api/vitrine-virtual
        [HttpGet]
        public async Task<ActionResult<IEnumerable<VitrineVirtual>>> ObterVitrinesVirtuais()
        {
            return await _context.VitrinesVirtuais.ToListAsync();
        }

        // GET: api/vitrine-virtual/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<VitrineVirtual>> ObterVitrineVirtualPorId(Guid id)
        {
            var vitrineVirtual = await _context.VitrinesVirtuais.FindAsync(id);

            if (vitrineVirtual == null)
            {
                return NotFound();
            }

            return Ok(vitrineVirtual);
        }

        // PUT: api/vitrine-virtual/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> AtualizarVitrineVirtual(Guid id, VitrineVirtual vitrineVirtualAtualizada)
        {
            if (id != vitrineVirtualAtualizada.Id)
            {
                return BadRequest("IDs da vitrine virtual não coincidem.");
            }

            _context.Entry(vitrineVirtualAtualizada).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VitrineVirtualExists(id))
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

        // DELETE: api/vitrine-virtual/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> ExcluirVitrineVirtual(Guid id)
        {
            var vitrineVirtual = await _context.VitrinesVirtuais.FindAsync(id);
            if (vitrineVirtual == null)
            {
                return NotFound();
            }

            _context.VitrinesVirtuais.Remove(vitrineVirtual);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool VitrineVirtualExists(Guid id)
        {
            return _context.VitrinesVirtuais.Any(e => e.Id == id);
        }
    }
}
