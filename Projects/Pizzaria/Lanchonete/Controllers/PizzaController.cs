using Microsoft.AspNetCore.Mvc;
using Pizzaria.DTO;
using Pizzaria.Services.Pizza;
using System.Threading.Tasks;

namespace Pizzaria.Controllers
{
    public class PizzaController : Controller
    {
        private readonly IPizzaInterface _pizzaInterface;

        public PizzaController(IPizzaInterface pizzaInterface)
        {
            _pizzaInterface = pizzaInterface;
        }

        public async Task<IActionResult> Index()
        {
            var pizzas = await _pizzaInterface.GetPizzas();
            return View(pizzas);
        }

        public IActionResult Cadastrar()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Cadastrar(PizzaCriacaoDto pizzaCriacaoDto, IFormFile foto)
        {
            if (ModelState.IsValid)
            {
                var pizza = await _pizzaInterface.CriarPizza(pizzaCriacaoDto, foto);
                return RedirectToAction("Index", "Pizza");
            }
            else
            {
                return View(pizzaCriacaoDto);
            }
        }
    }
}
