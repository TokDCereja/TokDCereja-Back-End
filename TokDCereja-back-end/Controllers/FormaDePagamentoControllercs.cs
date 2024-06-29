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
    [Route("api/forma-pagamento")]
    [ApiController]
    public class FormaDePagamentoController : ControllerBase
    {
        private readonly TokDCerejaDbContext _context;

        public FormaDePagamentoController(TokDCerejaDbContext context)
        {
            _context = context;
        }

        // POST: api/forma-pagamento
        [HttpPost]
        public async Task<IActionResult> CriarFormaDePagamento(FormaDePagamento formaDePagamento)
        {
            formaDePagamento.Id = Guid.NewGuid();
            formaDePagamento.IsDeleted = false;

            _context.FormasDePagamentos.Add(formaDePagamento);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(ObterFormaDePagamentoPorId), new { id = formaDePagamento.Id }, formaDePagamento);
        }

        // GET: api/forma-pagamento
        [HttpGet]
        public async Task<ActionResult<IEnumerable<FormaDePagamento>>> ObterFormasDePagamento()
        {
            return await _context.FormasDePagamentos
                .Where(f => !f.IsDeleted)
                .ToListAsync();
        }

        // GET: api/forma-pagamento/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<FormaDePagamento>> ObterFormaDePagamentoPorId(Guid id)
        {
            var formaDePagamento = await _context.FormasDePagamentos
                .Where(f => f.Id == id && !f.IsDeleted)
                .FirstOrDefaultAsync();

            if (formaDePagamento == null)
            {
                return NotFound();
            }

            return Ok(formaDePagamento);
        }

        // PUT: api/forma-pagamento/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> AtualizarFormaDePagamento(Guid id, FormaDePagamento formaDePagamentoAtualizada)
        {
            if (id != formaDePagamentoAtualizada.Id)
            {
                return BadRequest("IDs da forma de pagamento não coincidem.");
            }

            var formaDePagamento = await _context.FormasDePagamentos
                .Where(f => f.Id == id && !f.IsDeleted)
                .FirstOrDefaultAsync();

            if (formaDePagamento == null)
            {
                return NotFound();
            }

            // Atualizar campos permitidos
            formaDePagamento.NomeTitular = formaDePagamentoAtualizada.NomeTitular;
            formaDePagamento.CPF = formaDePagamentoAtualizada.CPF;
            formaDePagamento.TipoPlano = formaDePagamentoAtualizada.TipoPlano;
            formaDePagamento.Valor = formaDePagamentoAtualizada.Valor;
            formaDePagamento.Banco = formaDePagamentoAtualizada.Banco;
            formaDePagamento.TipoPagamento = formaDePagamentoAtualizada.TipoPagamento;
            formaDePagamento.NumeroParcelas = formaDePagamentoAtualizada.NumeroParcelas;

            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/forma-pagamento/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DesativarFormaDePagamento(Guid id)
        {
            var formaDePagamento = await _context.FormasDePagamentos
                .Where(f => f.Id == id && !f.IsDeleted)
                .FirstOrDefaultAsync();

            if (formaDePagamento == null)
            {
                return NotFound();
            }

            // Marcar a forma de pagamento como desativada (exclusão lógica)
            formaDePagamento.IsDeleted = true;
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
