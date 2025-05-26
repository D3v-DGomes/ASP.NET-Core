namespace CrudWithDapper.DTO
{
    public class UsuarioListarDto   // Passo 4: Definição do DTO de Listagem de Usuário
    {
        public int Id { get; set; }
        public string NomeCompleto { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Cargo { get; set; } = string.Empty;
        public double Salario { get; set; }
        public bool Situacao { get; set; }  // 1 = Ativo, 0 = Inativo
    }
}
