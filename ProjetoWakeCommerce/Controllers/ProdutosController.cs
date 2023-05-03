using Microsoft.AspNetCore.Mvc;
using ProjetoWakeCommerce.Entidades;

namespace ProjetoWakeCommerce.Controllers
{
    [ApiController]
    [Route("Produtos")]
    public class ProdutosController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<ProdutosController> _logger;

        public ProdutosController(ILogger<ProdutosController> logger)
        {
            _logger = logger;
        }

        #region GET
        [Route("obter-produtos")]
        [HttpGet]
        public async Task<IEnumerable<Produto>> Get()
        {
            return (IEnumerable<Produto>)Ok();
        }

        [Route("obter-produto-por-id")]
        [HttpGet]
        public async Task<Produto> GetById(int id)
        {
            var produto = new Produto();            

            return produto;
        }
        #endregion
    }
}