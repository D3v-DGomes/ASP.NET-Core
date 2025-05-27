using ContorleDeContatos.Models;

namespace CadastroContatos.Repository
{
    public interface IContatoRepositorio
    {
        List<ContatoModel> BuscarTodos();
        ContatoModel Adicionar(ContatoModel contato);       // Adiciona um novo contato
    }
}
