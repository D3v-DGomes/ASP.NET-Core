using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Pizzaria.Services.Pizza;

namespace Pizzaria.Controllers
{
    public class HomeController : Controller
    {
        private readonly IPizzaInterface _pizzaInterface;

        public HomeController(IPizzaInterface pizzaInterface)
        {
            _pizzaInterface = pizzaInterface;
        }

        public async Task<IActionResult> Index(string? pesquisar)   // Barra de pesquisar na tela inicial
        {
            if (pesquisar == null)      
            {
                var pizzas = await _pizzaInterface.GetPizzas();
                return View(pizzas);
            }
            else
            {
                var pizzas = await _pizzaInterface.GetPizzasFiltro(pesquisar);
                return View(pizzas);
            }
        }
    }
}
