using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TokDCereja_back_end.Data;
using TokDCereja_back_end.Models;

namespace TokDCereja_back_end.Controllers
{
    [Route("api/agenda")]
    [ApiController]
    public class AgendaController : ControllerBase
    {
        private readonly TokDCerejaDbContext _context;

        public AgendaController(TokDCerejaDbContext context)
        {
            _context = context;
        }

        // POST: api/agenda
        [HttpPost]
        public async Task<IActionResult> CriarAgenda(Agenda agenda)
        {
            agenda.Id = Guid.NewGuid();
            agenda.IsDeleted = false;
            agenda.DataCompromisso = DateTime.UtcNow;

            _context.Agendas.Add(agenda);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(ObterAgendaPorId), new { id = agenda.Id }, agenda);
        }

        // GET: api/agenda
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Agenda>>> ObterAgendas()
        {
            return await _context.Agendas
                .Where(a => !a.IsDeleted)
                .ToListAsync();
        }

        // GET: api/agenda/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Agenda>> ObterAgendaPorId(Guid id)
        {
            var agenda = await _context.Agendas
                .Where(a => a.Id == id && !a.IsDeleted)
                .FirstOrDefaultAsync();

            if (agenda == null)
            {
                return NotFound();
            }

            return Ok(agenda);
        }

        // PUT: api/agenda/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> AtualizarAgenda(Guid id, Agenda agendaAtualizada)
        {
            if (id != agendaAtualizada.Id)
            {
                return BadRequest("IDs da agenda não coincidem.");
            }

            var agenda = await _context.Agendas
                .Where(a => a.Id == id && !a.IsDeleted)
                .FirstOrDefaultAsync();

            if (agenda == null)
            {
                return NotFound();
            }

            // Atualizar campos permitidos
            agenda.DataCompromisso = agendaAtualizada.DataCompromisso;
            agenda.Descricao = agendaAtualizada.Descricao;

            await _context.SaveChangesAsync();
            return NoContent();
        }

        // DELETE: api/agenda/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DesativarAgenda(Guid id)
        {
            var agenda = await _context.Agendas
                .Where(a => a.Id == id && !a.IsDeleted)
                .FirstOrDefaultAsync();

            if (agenda == null)
            {
                return NotFound();
            }

            // Marcar a agenda como desativada
            agenda.IsDeleted = true;

            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
