using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;
using Pizzaria.DTO;
using Pizzaria.Models;
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

        public async Task<IActionResult> Editar(int id)
        {
            var pizza = await _pizzaInterface.GetPizzaPorId(id);
            return View(pizza);
        }

        [HttpPost]
        public async Task<IActionResult> Cadastrar(PizzaCriacaoDto pizzaCriacaoDto, IFormFile foto)
        {
            if (ModelState.IsValid)
            {
                await _pizzaInterface.CriarPizza(pizzaCriacaoDto, foto);
                return RedirectToAction("Index", "Pizza");
            }
            else
            {
                return View(pizzaCriacaoDto);
            }
        }

        public async Task<IActionResult> Remover(int id)
        {
            await _pizzaInterface.RemoverPizza(id);
            return RedirectToAction("Index", "Pizza");
        }

        [HttpPost]
        public async Task<IActionResult> Editar(PizzaModel pizzaModel, IFormFile? foto)
        {
            if (ModelState.IsValid)
            {
                await _pizzaInterface.EditarPizza(pizzaModel, foto);
                return RedirectToAction("Index", "Pizza");
            }
            else
            {
                return View(pizzaModel);
            }
        }
    }
}
