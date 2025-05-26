using CrudWithDapper.DTO;
using CrudWithDapper.Models;

namespace CrudWithDapper.Services
{
    public interface IUsuarioInterface // Passo 3.1: Definição da Interface de Usuário
    {
        Task<ResponseModel<List<UsuarioListarDto>>> BuscarUsuarios(); // Passo 4.1: Método para Listar Usuários
        Task<ResponseModel<UsuarioListarDto>> BuscarUsuarioPorId(int usuarioId);     // Passo 9: Método para buscar usuários pelo ID
        Task<ResponseModel<List<UsuarioListarDto>>> CriarUsuario(UsuarioCriarDto usuarioCriarDto);  // Passo 10.1: Método para criar um usuário
        Task<ResponseModel<List<UsuarioListarDto>>> EditarUsuario(UsuarioEditarDto usuarioEditarDto);  // Passo 11.1: Método para editar um usuário
        Task<ResponseModel<List<UsuarioListarDto>>> RemoverUsuario(int usuarioId);    // Passo 12: Método para remover usuário
    }
}