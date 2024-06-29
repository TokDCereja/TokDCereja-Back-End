using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TokDCereja_back_end.Data;
using TokDCereja_back_end.Models;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;

namespace TokDCereja_back_end.Controllers
{
    [Route("api/usuario")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly TokDCerejaDbContext _context;
        private readonly IConfiguration _configuration;

        public UsuarioController(TokDCerejaDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        // api/usuario/login
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginModel login)
        {
            var usuario = await _context.Usuarios
                .Where(u => u.Email == login.Email && u.Senha == login.Senha && !u.IsDeleted)
                .FirstOrDefaultAsync();

            if (usuario == null)
            {
                return Unauthorized("Você não é açucarado o suficiente");
            }

            var token = GenerateJwtToken(usuario);
            return Ok(new { token });
        }

        private string GenerateJwtToken(Usuario usuario)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, usuario.Email), // email usuario
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()), // id token
                new Claim(ClaimTypes.NameIdentifier, usuario.Id.ToString()) // id usuario
            };

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(60),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }


        // POST: api/usuario
        [HttpPost]
        public async Task<IActionResult> CriarUsuario(Usuario usuario)
        {
            usuario.Id = Guid.NewGuid();
            usuario.IsDeleted = false;
            usuario.DataCadastro = DateTime.UtcNow;
            usuario.TipoPlano="Gratuito";

            _context.Usuarios.Add(usuario);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(CriarUsuario), new { id = usuario.Id }, usuario);
        }

        // GET: api/usuario
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Usuario>>> ObterUsuarios()
        {
            return await _context.Usuarios
                //.Where(u => !u.IsDeleted)
                .ToListAsync();
        }

        // GET: api/usuario/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Usuario>> ObterUsuarioPorId(Guid id)
        {
            var usuario = await _context.Usuarios
                .Where(u => u.Id == id && !u.IsDeleted)
                .FirstOrDefaultAsync();

            if (usuario == null)
            {
                return NotFound();
            }

            return Ok(usuario);
        }

        // PUT: api/usuario/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> AtualizarUsuario(Guid id, Usuario usuarioAtualizado)
        {
            if (id != usuarioAtualizado.Id)
            {
                return BadRequest("IDs do usuário não coincidem.");
            }

            var usuario = await _context.Usuarios
                .Where(u => u.Id == id && !u.IsDeleted)
                .FirstOrDefaultAsync();

            if (usuario == null)
            {
                return NotFound();
            }

            // Atualizar campos permitidos
            usuario.Nome = usuarioAtualizado.Nome;
            usuario.Email = usuarioAtualizado.Email;
            usuario.Senha = usuarioAtualizado.Senha;
            usuario.TipoPlano = usuarioAtualizado.TipoPlano;
            usuario.Telefone = usuarioAtualizado.Telefone;

            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]

        public async Task<IActionResult> DesativarUsuario(Guid id)
        {
            var usuario = await _context.Usuarios
            .Where(d => d.Id == id && !d.IsDeleted)
            .FirstOrDefaultAsync();

            if (usuario == null)
            {
                return NotFound();
            }

            // Marcar o usuário como desativado
            usuario.IsDeleted = true;

            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}