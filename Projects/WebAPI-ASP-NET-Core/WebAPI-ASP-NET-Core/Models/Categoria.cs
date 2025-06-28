using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAPI_ASP_NET_Core.Models;

[Table("Categorias")]
public class Categoria
{
    public Categoria() 
    {
        Produtos = new Collection<Produto>();   // Inicializando a coleção
    }

    [Key]
    public int CategoriaId { get; set; }

    [Required]
    [StringLength(80)]
    public string? Nome { get; set; }

    [Required]
    [StringLength(300)]
    public string? ImagemUrl { get; set; }

    // Propriedade de navegação:
    public ICollection<Produto>? Produtos { get; set; }
}

