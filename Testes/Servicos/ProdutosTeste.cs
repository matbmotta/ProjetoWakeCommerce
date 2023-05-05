using Microsoft.EntityFrameworkCore;
using Moq;
using ProjetoWakeCommerce.Application.Interfaces;
using ProjetoWakeCommerce.Application.Services;
using ProjetoWakeCommerce.Data;
using ProjetoWakeCommerce.Entidades;
using ProjetoWakeCommerce.Repositorio.Interfaces;
using ProjetoWakeCommerce.Repositorio.Repositorios;
using WakeCommerce.Tests.Mocks;
using WakeCommerce.Utils.Enums;
using Xunit.Sdk;

namespace WakeCommerce.Tests.Servicos
{
    [TestClass]
    public class ProdutosTeste
    {
        private ProdutosService _produtoService;
        private Mock<IProdutoRepositorio> _produtoRepositorioMock;

        [TestInitialize]
        public void InicializarTeste()
        {
            _produtoRepositorioMock = new Mock<IProdutoRepositorio>();
            _produtoService = new ProdutosService(_produtoRepositorioMock.Object);
        }

        [TestMethod]
        public async Task ObterProdutosOrdenados_DeveRetornarListaOrdenada()
        {
            // Arrange
            var produtosEsperados = new List<Produto>()
            {
            new Produto() { Nome = "Produto A", Estoque = 10 },
            new Produto() { Nome = "Produto B", Estoque = 5 },
            new Produto() { Nome = "Produto C", Estoque = 20 },
            };

            // Configuração do mock para retornar uma lista de produtos não ordenada
            _produtoRepositorioMock.Setup(x => x.ObterTodos()).ReturnsAsync(new List<Produto>()
            {
            new Produto() { Nome = "Produto B", Estoque = 5 },
            new Produto() { Nome = "Produto C", Estoque = 20 },
            new Produto() { Nome = "Produto A", Estoque = 10 },
            });

            // Act
            var resultado = await _produtoService.ObterProdutosOrdenados();

            // Assert
            CollectionAssert.AreEqual(produtosEsperados, resultado, new ProdutoComparer());
        }

        [TestMethod]
        [ExpectedException(typeof(Exception), "Nenhum produto encontrado")]
        public async Task ObterProdutosOrdenados_SemProdutos_DeveLancarExcecao()
        {
            // Configuração do mock para retornar uma lista de produtos vazia
            _produtoRepositorioMock.Setup(x => x.ObterTodos()).ReturnsAsync(new List<Produto>());

            // Act
            var resultado = await _produtoService.ObterProdutosOrdenados();
        }

        private class ProdutoComparer : Comparer<Produto>
        {
            public override int Compare(Produto x, Produto y)
            {
                if (x.Nome == y.Nome)
                {
                    return x.Estoque.CompareTo(y.Estoque);
                }
                return x.Nome.CompareTo(y.Nome);
            }
        }
    }
}