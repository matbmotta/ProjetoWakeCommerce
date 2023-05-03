using Microsoft.AspNetCore.Mvc;
using ProjetoWakeCommerce.Application.Interfaces;
using ProjetoWakeCommerce.Entidades;

namespace ProjetoWakeCommerce.Controllers
{
    [ApiController]
    [Route("Produtos")]
    public class ProdutosController : ControllerBase
    {
        private readonly ILogger<ProdutosController> _logger;
        protected IProdutosService ProdutosService;


        public ProdutosController(ILogger<ProdutosController> logger, IProdutosService produtosService)
        {
            _logger = logger;
            ProdutosService = produtosService;
        }

        #region GET
        /// <summary>
        /// Obtém todos os produtos por filtro
        /// </summary>
        /// <returns></returns>
        [Route("obter-produtos")]
        [HttpGet]
        [Produces("application/json", Type = typeof(List<Produto>))]
        public async Task<IActionResult> ObterProdutosPorFiltro()
        {
            var produtos = await ProdutosService.ObterProdutosPorFiltro();

            return Ok(produtos);
        }
        /// <summary>
        /// Obtém um produto específico pelo id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Route("obter-produto-por-id")]
        [HttpGet]
        [Produces("application/json", Type = typeof(Produto))]
        public async Task<IActionResult> ObterPorId(int id)
        {
            var produto = await ProdutosService.ObterProdutoPorId(id);            

            return Ok(produto);
        }
        #endregion

        #region POST
        /// <summary>
        /// Insere um novo produto
        /// </summary>
        /// <param name="produto"></param>
        /// <returns></returns>
        [Route("inserir-produto")]
        [HttpPost]
        [Produces("application/json", Type = typeof(List<Produto>))]
        public async Task<IActionResult> InserirProduto(Produto produto)
        {
            var inserirProduto = await ProdutosService.InserirProduto(produto);

            return Ok(inserirProduto);
        }

        #endregion

        #region PUT
        /// <summary>
        /// Atualiza um produto existente
        /// </summary>
        /// <param name="produto"></param>
        /// <returns></returns>
        [Route("atualizar-produto-por-id")]
        [HttpPut]
        [Produces("application/json", Type = typeof(List<Produto>))]
        public async Task<IActionResult> AtualizarProdutoPorId(Produto produto)
        {
            var atualizarProduto = await ProdutosService.AtualizarProdutoPorId(produto);

            return Ok(atualizarProduto);
        }
        #endregion

        #region DELETE
        /// <summary>
        /// Exclui um produto existente
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Route("excluir-produto-por-id")]
        [HttpDelete]
        [Produces("application/json", Type = typeof(List<Produto>))]
        public async Task<IActionResult> ExcluirProdutoPorId(int id)
        {
            var produto = await ProdutosService.ExcluirProdutoPorId(id);

            return Ok(produto);
        }
        #endregion
    }
}