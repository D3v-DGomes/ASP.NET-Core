using CadastroContatos.Repository;
using ContorleDeContatos.Models;
using Microsoft.AspNetCore.Mvc;

namespace ControleDeContatos.Controllers
{
    public class ContatoController : Controller
    {
        private readonly IContatoRepositorio _contatorepositorio;
        public ContatoController(IContatoRepositorio contatoRepositorio) 
        {
            _contatorepositorio = contatoRepositorio;
        }

        public IActionResult Index()
        {
            List<ContatoModel> contatos = _contatorepositorio.BuscarTodos();
            return View(contatos);
        }

        public IActionResult CriarContato()
        {
            return View();
        }

        public IActionResult EditarContato()
        {
            return View();
        }

        public IActionResult ApagarContatoConfirmacao()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Criar(ContatoModel contato)
        { 
            _contatorepositorio.Adicionar(contato);

            return RedirectToAction("Index");
        }
    }
}
