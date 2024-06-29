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
    [Route("api/aprendizado")]
    [ApiController]
    public class AprendizadoController : ControllerBase
    {
        private readonly TokDCerejaDbContext _context;

        public AprendizadoController(TokDCerejaDbContext context)
        {
            _context = context;
        }

        // POST: api/aprendizado
        [HttpPost]
        public async Task<IActionResult> CriarAprendizado(Aprendizado aprendizado)
        {
            aprendizado.Id = Guid.NewGuid();
            aprendizado.IsDeleted = false;
            aprendizado.DataPublicacao = DateTime.UtcNow;

            _context.Aprendizados.Add(aprendizado);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(ObterAprendizadoPorId), new { id = aprendizado.Id }, aprendizado);
        }

        // GET: api/aprendizado
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Aprendizado>>> ObterAprendizados()
        {
            return await _context.Aprendizados
                .Where(a => !a.IsDeleted)
                .OrderBy(a => a.Categoria) // Ordenar por Categoria
                .ToListAsync();
        }

        // GET: api/aprendizado/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Aprendizado>> ObterAprendizadoPorId(Guid id)
        {
            var aprendizado = await _context.Aprendizados
                .Where(a => a.Id == id && !a.IsDeleted)
                .FirstOrDefaultAsync();

            if (aprendizado == null)
            {
                return NotFound();
            }

            return Ok(aprendizado);
        }

        // PUT: api/aprendizado/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> AtualizarAprendizado(Guid id, Aprendizado aprendizadoAtualizado)
        {
            if (id != aprendizadoAtualizado.Id)
            {
                return BadRequest("IDs do aprendizado não coincidem.");
            }

            var aprendizado = await _context.Aprendizados
                .Where(a => a.Id == id && !a.IsDeleted)
                .FirstOrDefaultAsync();

            if (aprendizado == null)
            {
                return NotFound();
            }

            // Atualizar campos permitidos
            aprendizado.Nome = aprendizadoAtualizado.Nome;
            aprendizado.Descricao = aprendizadoAtualizado.Descricao;
            aprendizado.Categoria = aprendizadoAtualizado.Categoria;
            aprendizado.Conteudo = aprendizadoAtualizado.Conteudo;

            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/aprendizado/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DesativarAprendizado(Guid id)
        {
            var aprendizado = await _context.Aprendizados
                .Where(a => a.Id == id && !a.IsDeleted)
                .FirstOrDefaultAsync();

            if (aprendizado == null)
            {
                return NotFound();
            }

            // Marcar o aprendizado como desativado (exclusão lógica)
            aprendizado.IsDeleted = true;
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
