using Microsoft.EntityFrameworkCore;
using Pizzaria.Data;
using Pizzaria.DTO;
using Pizzaria.Models;

namespace Pizzaria.Services.Pizza
{
    public class PizzaService : IPizzaInterface         // Implementação de métodos da interface IPizzaInterface
    {
        // Criando conexão com o banco de dados:
        private readonly AppDbContext _context;
        private readonly string _sistema;

        public PizzaService(AppDbContext context, IWebHostEnvironment sistema)
        {
            _context = context;
            _sistema = sistema.WebRootPath;     // Caminho do www
        }

        public string GeraCaminhoArquivo(IFormFile foto)
        {
            var codigoUnico = Guid.NewGuid().ToString();    // Gera uma cadeia de caracteres aleatórios
            var nomeCaminhoImagem = foto.FileName.Replace(" ", "").ToLower() + codigoUnico + ".png";

            // Criando o caminho para salvar a imagem:
            var caminhoParaSalvarImagens = _sistema + "\\imagem\\";

            // Verificando a existência da pasta imagem:
            if (!Directory.Exists(caminhoParaSalvarImagens))
            {
                Directory.CreateDirectory(caminhoParaSalvarImagens);
            }

            // Criando o nome do caminho da imagem:
            using (var stream = File.Create(caminhoParaSalvarImagens + nomeCaminhoImagem))
            {
                foto.CopyToAsync(stream).Wait();    // Criando cópia da imagem
            }

            return nomeCaminhoImagem;
        }

        public async Task<PizzaModel> CriarPizza(PizzaCriacaoDto pizzaCriacaoDto, IFormFile foto)
        {
            try
            {
                var nomeCaminhoImagem = GeraCaminhoArquivo(foto);

                var pizza = new PizzaModel
                {
                    Sabor = pizzaCriacaoDto.Sabor,
                    Descricao = pizzaCriacaoDto.Descricao,
                    Valor = pizzaCriacaoDto.Valor,
                    Capa = nomeCaminhoImagem
                };

                _context.Add(pizza);
                await _context.SaveChangesAsync();

                return pizza;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<PizzaModel>> GetPizzas()
        {
            try
            {
                return await _context.Pizzas.ToListAsync();    // Retorna a lista de pizzas
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public Task<PizzaModel> GetPizzaPorId(int id)
        {
            throw new NotImplementedException();
        }
    }
}
