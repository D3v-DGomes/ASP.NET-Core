using Microsoft.EntityFrameworkCore;    
using formularioDeCadastro.Models;

namespace formularioDeCadastro.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {  
        }

        public DbSet<Usuario> DB_Usuario{ get; set; }
    }
}
