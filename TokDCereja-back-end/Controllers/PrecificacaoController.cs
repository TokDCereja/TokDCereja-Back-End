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
    [Route("api/precificacao")]
    [ApiController]
    public class PrecificacaoController : ControllerBase
    {
        private readonly TokDCerejaDbContext _context;

        public PrecificacaoController(TokDCerejaDbContext context)
        {
            _context = context;
        }

        // POST: api/precificacao
        [HttpPost]
        public async Task<IActionResult> CriarPrecificacao(Precificacao precificacao)
        {
            precificacao.Id = Guid.NewGuid();
            precificacao.IsDeleted = false;

            _context.Precificacoes.Add(precificacao);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(ObterPrecificacaoPorId), new { id = precificacao.Id }, precificacao);
        }

        // GET: api/precificacao
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Precificacao>>> ObterPrecificacoes()
        {
            return await _context.Precificacoes
                .Where(p => !p.IsDeleted)
                .ToListAsync();
        }

        // GET: api/precificacao/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Precificacao>> ObterPrecificacaoPorId(Guid id)
        {
            var precificacao = await _context.Precificacoes
                .Where(p => p.Id == id && !p.IsDeleted)
                .FirstOrDefaultAsync();

            if (precificacao == null)
            {
                return NotFound();
            }

            return Ok(precificacao);
        }

        // PUT: api/precificacao/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> AtualizarPrecificacao(Guid id, Precificacao precificacaoAtualizada)
        {
            if (id != precificacaoAtualizada.Id)
            {
                return BadRequest("IDs da precificação não coincidem.");
            }

            var precificacao = await _context.Precificacoes
                .Where(p => p.Id == id && !p.IsDeleted)
                .FirstOrDefaultAsync();

            if (precificacao == null)
            {
                return NotFound();
            }

            // Atualizar campos permitidos
            precificacao.FerramentaId = precificacaoAtualizada.FerramentaId;
            precificacao.CustoReceita = precificacaoAtualizada.CustoReceita;
            precificacao.MaoDeObra = precificacaoAtualizada.MaoDeObra;
            precificacao.CustoFixoUnitario = precificacaoAtualizada.CustoFixoUnitario;
            precificacao.CustoVariavelUnitario = precificacaoAtualizada.CustoVariavelUnitario;
            precificacao.PrecoVenda = precificacaoAtualizada.PrecoVenda;
            precificacao.Margemlucro = precificacaoAtualizada.Margemlucro;

            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/precificacao/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DesativarPrecificacao(Guid id)
        {
            var precificacao = await _context.Precificacoes
                .Where(p => p.Id == id && !p.IsDeleted)
                .FirstOrDefaultAsync();

            if (precificacao == null)
            {
                return NotFound();
            }

            // Marcar a precificação como desativada (exclusão lógica)
            precificacao.IsDeleted = true;
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
