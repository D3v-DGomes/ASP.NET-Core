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

        public PizzaService(AppDbContext context,
                IWebHostEnvironment sistema)
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
                return await _context.Pizzas.ToListAsync();    // Entrar no banco de dados, pegar a tabela de pizzas e transformar em uma lista
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<PizzaModel> GetPizzaPorId(int id)
        {
            try
            {
                return await _context.Pizzas.FirstOrDefaultAsync(pizza => pizza.Id == id);    // Entrar no banco de dados, pegar a pizza com o id informado e retornar a primeira ocorrência
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<PizzaModel> EditarPizza(PizzaModel pizza, IFormFile? foto)
        {
            try
            {
                var pizzaBanco = await _context.Pizzas.AsNoTracking().FirstOrDefaultAsync(pizzaBD => pizzaBD.Id == pizza.Id);   // A pizza que está no banco de dados tem que ser igual  a pizza que está sendo editada

                if (pizzaBanco == null)
                {
                    throw new Exception("Pizza não encontrada no banco de dados!");
                }

                var nomeCaminhoImagem = "";

                if (foto != null)
                {
                    string caminhoCapaExistente = _sistema + "\\imagem\\" + pizzaBanco.Capa;    // Caminho da imagem existente

                    // Garantindo que pizzaBanco.Capa não seja nulo ou vazio antes de tentar excluir o arquivo correspondente:
                    if (!string.IsNullOrEmpty(pizzaBanco.Capa) && File.Exists(caminhoCapaExistente))
                    {
                        File.Delete(caminhoCapaExistente);    // Deletando a imagem existente
                    }

                    nomeCaminhoImagem = GeraCaminhoArquivo(foto);    // Gerando o novo caminho da imagem
                }

                // Atualizando os dados da pizza:
                pizzaBanco.Sabor = pizza.Sabor;
                pizzaBanco.Descricao = pizza.Descricao;
                pizzaBanco.Valor = pizza.Valor;

                if (!string.IsNullOrEmpty(nomeCaminhoImagem))
                {
                    pizzaBanco.Capa = nomeCaminhoImagem;    // Atualizando o caminho da imagem
                }

                _context.Update(pizzaBanco);    // Atualizando os dados da pizza no banco de dados
                await _context.SaveChangesAsync();    // Salvando as alterações no banco de dados

                return pizza;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<PizzaModel> RemoverPizza(int id)
        {
            try
            {
                PizzaModel? pizza = await _context.Pizzas.FirstOrDefaultAsync(pizzabanco => pizzabanco.Id == id);

                if (pizza == null)
                {
                    throw new Exception("Pizza não encontrada!");
                }

                _context.Remove(pizza);
                await _context.SaveChangesAsync();

                return pizza;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<PizzaModel>> GetPizzasFiltro(string? pesquisar)
        {
            try
            {
                var pizzas = await _context.Pizzas.Where(pizzaBanco => pizzaBanco.Sabor.Contains(pesquisar)).ToListAsync();   // Vá até o banco e pegue os sabores que contêm o que está no pesquisar
                return pizzas;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
