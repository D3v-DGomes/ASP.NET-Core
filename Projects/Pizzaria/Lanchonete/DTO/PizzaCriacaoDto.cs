namespace Pizzaria.DTO
{
    public class PizzaCriacaoDto
    {
        // Criação de uma pizza
        public int Id { get; set; }

        public string Sabor { get; set; } = string.Empty; 

        public string Capa { get; set; } = string.Empty;

        public string Descricao { get; set; } = string.Empty;

        public double Valor { get; set; }
    }
}
