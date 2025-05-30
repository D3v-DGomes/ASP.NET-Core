using formularioDeCadastro.Data;
using formularioDeCadastro.Models;
using Microsoft.AspNetCore.Mvc;
using System;   
using System.Threading.Tasks;

namespace formularioDeCadastro.Controllers
{
    [ApiController]
    [Route("[controller]")]

    public class UsuariosController : ControllerBase
    {
        private readonly AppDbContext _context;

        public UsuariosController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost("Cadastrar")]
        public async Task<IActionResult> Cadastrar([FromBody] Usuario usuario)
        {
            if (ModelState.IsValid)
            {
               _context.DB_Usuario.Add(usuario);
                await _context.SaveChangesAsync();
                return Ok(new { message = "Usuário cadastrado com sucesso." });
            }

            return BadRequest(ModelState);
        }
    }
}
