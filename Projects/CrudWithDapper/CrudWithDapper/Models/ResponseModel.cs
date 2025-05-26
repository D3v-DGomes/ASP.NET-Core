namespace CrudWithDapper.Models
{
    public class ResponseModel<T>  // Passo 2: Definição do Modelo de Resposta
    {
        public T? Dados { get; set; }  // Dados genéricos do tipo T
        public string Mensagem { get; set; } = string.Empty;  // Mensagem de resposta
        public bool Status { get; set; } = true; // Status da operação (true = sucesso, false = falha)
    }
}
