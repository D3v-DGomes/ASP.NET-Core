using Microsoft.EntityFrameworkCore;
using Pizzaria.Models;

namespace Pizzaria.Data
{
    public class AppDbContext : DbContext   // Herdando DbContext do EF
    {
        // Criando construtor para receber as opções de configuração do DbContext:
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        
        // Criar uma tabela usando a pizza modelo:
        public DbSet<PizzaModel> Pizzas { get; set; }
    }
}
