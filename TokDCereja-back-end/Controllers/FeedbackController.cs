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
    [Route("api/feedback")]
    [ApiController]
    public class FeedbackController : ControllerBase
    {
        private readonly TokDCerejaDbContext _context;

        public FeedbackController(TokDCerejaDbContext context)
        {
            _context = context;
        }

        // POST: api/feedback
        [HttpPost]
        public async Task<IActionResult> CriarFeedback(Feedback feedback)
        {
            feedback.Id = Guid.NewGuid();
            feedback.IsDeleted = false;
            feedback.DataFeedback = DateTime.UtcNow;

            _context.Feedbacks.Add(feedback);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(ObterFeedbackPorId), new { id = feedback.Id }, feedback);
        }

        // GET: api/feedback
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Feedback>>> ObterFeedbacks()
        {
            return await _context.Feedbacks
                .Where(f => !f.IsDeleted)
                .ToListAsync();
        }

        // GET: api/feedback/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Feedback>> ObterFeedbackPorId(Guid id)
        {
            var feedback = await _context.Feedbacks
                .Where(f => f.Id == id && !f.IsDeleted)
                .FirstOrDefaultAsync();

            if (feedback == null)
            {
                return NotFound();
            }

            return Ok(feedback);
        }

        // PUT: api/feedback/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> AtualizarFeedback(Guid id, Feedback feedbackAtualizado)
        {
            if (id != feedbackAtualizado.Id)
            {
                return BadRequest("IDs do feedback não coincidem.");
            }

            var feedback = await _context.Feedbacks
                .Where(f => f.Id == id && !f.IsDeleted)
                .FirstOrDefaultAsync();

            if (feedback == null)
            {
                return NotFound();
            }

            // Atualizar campos permitidos
            feedback.Avaliacao = feedbackAtualizado.Avaliacao;
            feedback.UsuarioId = feedbackAtualizado.UsuarioId;

            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/feedback/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DesativarFeedback(Guid id)
        {
            var feedback = await _context.Feedbacks
                .Where(f => f.Id == id && !f.IsDeleted)
                .FirstOrDefaultAsync();

            if (feedback == null)
            {
                return NotFound();
            }

            // Marcar o feedback como desativado (exclusão lógica)
            feedback.IsDeleted = true;
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
