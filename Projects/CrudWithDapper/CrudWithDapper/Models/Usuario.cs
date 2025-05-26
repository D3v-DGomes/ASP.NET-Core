namespace CrudWithDapper.Models
{
    public class Usuario    // Passo 1: Definição do Modelo de Usuário
    {
        public int Id { get; set; }
        public string NomeCompleto { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Cargo { get; set; } = string.Empty;
        public double Salario { get; set; }
        public string CPF { get; set; } = string.Empty;
        public bool Situacao { get; set; }  // 1 = Ativo, 0 = Inativo
        public string Senha { get; set; } = string.Empty;
    }
}
