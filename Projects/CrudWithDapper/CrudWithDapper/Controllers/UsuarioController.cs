using CrudWithDapper.DTO;
using CrudWithDapper.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CrudWithDapper.Controllers    // Passo 3.2: Criação do Controller de Usuário
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioInterface _usuarioInterface;       // Passo 8: injeção de dependência para receber o IUsuarioInterface
        public UsuarioController(IUsuarioInterface IUsuarioInterface)
        {
            _usuarioInterface = IUsuarioInterface;
        }

        [HttpGet]       // Passo 8.1: Criar método para pegar a busca de usuários
        public async Task<IActionResult> BuscarUsuarios()
        {
            var usuarios = await _usuarioInterface.BuscarUsuarios();

            if (usuarios.Status == false)
            {
                return NotFound(usuarios);
            }

            return Ok(usuarios);
        }

        [HttpGet("{usuarioId}")]       // Passo 9.2: Criar método para buscar usuários por ID
        public async Task<IActionResult> BuscarUsuarioPorId(int usuarioId)
        {
            var usuarios = await _usuarioInterface.BuscarUsuarioPorId(usuarioId);

            if (usuarios.Status == false)
            {
                return NotFound(usuarios);
            }

            return Ok(usuarios);
        }

        [HttpPost]      // Passo 10.3: Criar método para criar usuário
        public async Task<IActionResult> CriarUsuario(UsuarioCriarDto usuarioCriarDto)
        {
            var usuarios = await _usuarioInterface.CriarUsuario(usuarioCriarDto);

            if (usuarios.Status == false)
            {
                return BadRequest(usuarios);    // Por se tratar de um cadastro
            }

            return Ok(usuarios);
        }

        [HttpPut]       // Passo 11.3: Criar método para editar usuário
        public async Task<IActionResult> EditarUsuario(UsuarioEditarDto usuarioEditarDto)
        {
            var usuarios = await _usuarioInterface.EditarUsuario(usuarioEditarDto);

            if (usuarios.Status == false)
            {
                return BadRequest(usuarios);    // Por se tratar de uma edição
            }

            return Ok(usuarios);
        }

        [HttpDelete]    // Passo 12.2: Criar método para excluir usuário
        public async Task<IActionResult> RemoverUsuario(int usuarioId)
        {
            var usuarios = await _usuarioInterface.RemoverUsuario(usuarioId);

            if (usuarios.Status == false)
            {
                return BadRequest(usuarios);    // Por se tratar de uma exclusão
            }

            return Ok(usuarios);
        }
    }
}
