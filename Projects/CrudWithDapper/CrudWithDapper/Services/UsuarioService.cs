using AutoMapper;
using CrudWithDapper.DTO;
using CrudWithDapper.Models;
using Dapper;
using Microsoft.Data.SqlClient;

namespace CrudWithDapper.Services
{
    public class UsuarioService : IUsuarioInterface // Passo 3: Implementação do Serviço de Usuário
    {
        private readonly IConfiguration _configuration;       // Passo 4.3: Injeção de Dependência para Configuração
        private readonly IMapper _mapper;    // Passo 7.3: Injeção de Dependência para configuração
        public UsuarioService(IConfiguration configuration, IMapper mapper) // Adicionado IMapper no construtor
        {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(_configuration));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper)); // Garante que _mapper seja inicializado
        }

        public async Task<ResponseModel<UsuarioListarDto>> BuscarUsuarioPorId(int usuarioId)         // Passo 9.1: implementar o método
        {
            ResponseModel<UsuarioListarDto> response = new ResponseModel<UsuarioListarDto>();

            using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                var usuarioBanco = await connection.QueryFirstOrDefaultAsync<Usuario>("select * from Usuarios where id = @Id", new { Id = usuarioId });    // Busque um usuário no banco que tenha o ID igual ao digitado

                if (usuarioBanco == null)
                {
                    response.Mensagem = "Nenhum usuário encontrado.";
                    response.Status = false;
                    return response;
                }

                var usuarioMapeado = _mapper.Map<UsuarioListarDto>(usuarioBanco);

                response.Dados = usuarioMapeado;
                response.Mensagem = "Usuário localizado com sucesso.";
            }

            return response;

        }

        public async Task<ResponseModel<List<UsuarioListarDto>>> BuscarUsuarios()          // Passo 4.2: Implementação do Método para Listar Usuários
        {

            ResponseModel<List<UsuarioListarDto>> response = new ResponseModel<List<UsuarioListarDto>>(); // Passo 6.3: Criação do Modelo de Resposta

            // Passo 6.1: Criar conexão com o banco de dados
            using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                var usuariosBanco = await connection.QueryAsync<Usuario>("select * from Usuarios");    // Passo 6.2: Executar a consulta SQL para buscar usuários

                if (!usuariosBanco.Any()) // Melhor verificação se a lista está vazia
                {
                    response.Mensagem = "Nenhum usuário encontrado.";  // Passo 6.4: Mensagem de resposta quando não há usuários
                    response.Status = false;
                    return response;
                }

                // Passo 7.2: Transformação Mapper:
                var usuariosMapeados = _mapper.Map<List<UsuarioListarDto>>(usuariosBanco);    // Mapear a lista de Usuario para List<UsuarioListarDto>

                response.Dados = usuariosMapeados;
                response.Mensagem = "Usuários encontrados com sucesso.";  // Passo 6.5: Mensagem de sucesso
            }

            return response;  // Passo 6.6: Retornar o modelo de resposta com os dados dos usuários
        }

        public async Task<ResponseModel<List<UsuarioListarDto>>> CriarUsuario(UsuarioCriarDto usuarioCriarDto)    // Passo 10.3: Implementação do Método para Criar Usuário
        {
            ResponseModel<List<UsuarioListarDto>> response = new ResponseModel<List<UsuarioListarDto>>();

            using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                var usuarioBanco = await connection.ExecuteAsync("insert into Usuarios (NomeCompleto, Email, Cargo, Salario, CPF, Senha, Situacao) " +
                        "VALUES (@NomeCompleto, @Email, @Cargo, @Salario, @CPF, @Senha, @Situacao)", usuarioCriarDto);

                if(usuarioBanco == 0)
                {
                    response.Mensagem = "Ocorreu um erro ao realizar o registro.";
                    response.Status = false;
                    return response;
                }

                var usuarios = await ListarUsuarios(connection);    // Mostrando a lista atualizada após o cadastro do novo usuário

                var usuariosMapeados = _mapper.Map<List<UsuarioListarDto>>(usuarios);

                response.Dados = usuariosMapeados;
                response.Mensagem = "Usuários listados com sucesso.";
            }

            return response;
        }

        private static async Task<IEnumerable<Usuario>> ListarUsuarios(SqlConnection connection)    // Passo 10.4: Criar listagem de usuários
        {
            return await connection.QueryAsync<Usuario>("select * from Usuarios");
        }

        public async Task<ResponseModel<List<UsuarioListarDto>>> EditarUsuario(UsuarioEditarDto usuarioEditarDto)   // Passo 11.2: Implementação do método para editar usuário
        {
            ResponseModel<List<UsuarioListarDto>> response = new ResponseModel<List<UsuarioListarDto>>();

            using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                var usuarioBanco = await connection.ExecuteAsync("update Usuarios set NomeCompleto = @NomeCompleto, Email = @Email, Cargo = @Cargo, " +
                    "Salario = @Salario, CPF = @CPF, Situacao = @Situacao where Id = @Id", usuarioEditarDto);

                if (usuarioBanco == 0)
                {
                    response.Mensagem = "Ocorreu um erro ao realizar a edição do usuário.";
                    response.Status = false;
                    return response;
                }

                var usuarios = await ListarUsuarios(connection);    // Mostrando a lista atualizada após a edição do usuário

                var usuariosMapeados = _mapper.Map<List<UsuarioListarDto>>(usuarios);

                response.Dados = usuariosMapeados;
                response.Mensagem = "Usuários listados com sucesso.";
            }

            return response;
        }

        public async Task<ResponseModel<List<UsuarioListarDto>>> RemoverUsuario(int usuarioId)    // Passo 12.1: Implementar o método de remover usuário
        {
            ResponseModel<List<UsuarioListarDto>> response = new ResponseModel<List<UsuarioListarDto>>();

            using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                var usuarioBanco = await connection.ExecuteAsync("delete from Usuarios where id = @Id", new {Id = usuarioId});

                if (usuarioBanco == 0)
                {
                    response.Mensagem = "Ocorreu um erro ao realizar a exclusão.";
                    response.Status = false;
                    return response;
                }

                var usuarios = await ListarUsuarios(connection);

                var usuariosMapeados = _mapper.Map<List<UsuarioListarDto>>(usuarios);

                response.Dados = usuariosMapeados;
                response.Mensagem = "Usuários listados com sucesso.";
            }

            return response;
        }
    }
}


