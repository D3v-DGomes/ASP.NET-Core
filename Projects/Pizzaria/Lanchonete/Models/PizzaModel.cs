namespace Pizzaria.Models
{
    public class PizzaModel
    {
        // Inserindo as propriedades da coluna da tabela de pizzas:

        public int Id { get; set; }

        public string Sabor { get; set; } = string.Empty;   // Iniciando a variável com string vazia para evitar erro de referência nula

        public string Capa { get; set; } = string.Empty;

        public string Descricao { get; set; } = string.Empty;

        public double Valor { get; set; }
    }
}
