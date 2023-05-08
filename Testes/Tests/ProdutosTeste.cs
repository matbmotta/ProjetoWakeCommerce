using Microsoft.EntityFrameworkCore;
using Moq;
using ProjetoWakeCommerce.Application.Interfaces;
using ProjetoWakeCommerce.Application.Services;
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
        private ProdutosService? _produtoService;
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
                new Produto()
                {
                    Id = 1,
                    Nome = "Produto A",
                    Estoque = 10,
                    Tipo = TipoEnum.Vestuario,
                    Valor = 50,
                    DataCriacao = DateTime.Today,
                    DataAtualizacao = DateTime.Today
                },
                new Produto()
                {
                    Id = 2,
                    Nome = "Produto B",
                    Estoque = 5,
                    Tipo = TipoEnum.CamaMesaBanho,
                    Valor = 55.2m,
                    DataCriacao = DateTime.Today,
                    DataAtualizacao = DateTime.Today
                },
                new Produto()
                {
                    Id = 3,
                    Nome = "Produto C",
                    Estoque = 20,
                    Tipo = TipoEnum.Eletronico,
                    Valor = 999.99m,
                    DataCriacao = DateTime.Today,
                    DataAtualizacao = DateTime.Today
                },
            };

            // Configurção do mock para retornar uma lista de produtos não ordenada
            var produtos = new List<Produto>()
            {
               new Produto()
                {
                    Id = 2,
                    Nome = "Produto B",
                    Estoque = 5,
                    Tipo = TipoEnum.CamaMesaBanho,
                    Valor = 55.2m,
                    DataCriacao = DateTime.Today,
                    DataAtualizacao = DateTime.Today
                },
                new Produto()
                {
                    Id = 3,
                    Nome = "Produto C",
                    Estoque = 20,
                    Tipo = TipoEnum.Eletronico,
                    Valor = 999.99m,
                    DataCriacao = DateTime.Today,
                    DataAtualizacao = DateTime.Today
                },
                new Produto()
                {
                    Id = 1,
                    Nome = "Produto A",
                    Estoque = 10,
                    Tipo = TipoEnum.Vestuario,
                    Valor = 50,
                    DataCriacao = DateTime.Today,
                    DataAtualizacao = DateTime.Today
                },
            };

            _produtoRepositorioMock.Setup(x => x.ObterTodos()).ReturnsAsync(produtos);
            var produtoService = new ProdutosService(_produtoRepositorioMock.Object);

            // Act
            var resultado = await produtoService.ObterProdutosOrdenados();

            // Assert
            Assert.IsTrue(resultado[0].Id == produtosEsperados[0].Id);
            Assert.IsTrue(resultado[0].Nome == produtosEsperados[0].Nome);
            Assert.IsTrue(resultado[0].Estoque == produtosEsperados[0].Estoque);
            Assert.IsTrue(resultado[1].Id == produtosEsperados[1].Id);
            Assert.IsTrue(resultado[1].Nome == produtosEsperados[1].Nome);
            Assert.IsTrue(resultado[1].Estoque == produtosEsperados[1].Estoque);
            Assert.IsTrue(resultado[2].Id == produtosEsperados[2].Id);
            Assert.IsTrue(resultado[2].Nome == produtosEsperados[2].Nome);
            Assert.IsTrue(resultado[2].Estoque == produtosEsperados[2].Estoque);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception), "Nenhum produto encontrado")]
        public async Task ObterProdutosOrdenados_SemProdutos_DeveLancarExcecao()
        {
            // Configurção do mock para retornar uma lista de produtos vazia
            _produtoRepositorioMock.Setup(x => x.ObterTodos()).ReturnsAsync(new List<Produto>());

            var produtoService = new ProdutosService(_produtoRepositorioMock.Object);

            // Act
            var resultado = await produtoService.ObterProdutosOrdenados();

            // Assert - Expects Exception
        }

        [TestMethod]
        public async Task DeveObterProdutoPorNome()
        {
            // Arrange
            var nomeProduto = "Nome do Produto";
            var produtoEsperado = new Produto 
            {
                Id = 1,
                Nome = "Nome do Produto",
                Estoque = 10,
                Tipo = TipoEnum.Vestuario,
                Valor = 50,
                DataCriacao = DateTime.Today,
                DataAtualizacao = DateTime.Today
            };

            _produtoRepositorioMock
                .Setup(x => x.ObterPorNome(nomeProduto))
                .ReturnsAsync(produtoEsperado);

            var produtoService = new ProdutosService(_produtoRepositorioMock.Object);

            // Act
            var produtoObtido = await produtoService.ObterProdutoPorNome(nomeProduto);

            // Assert
            Assert.IsNotNull(produtoObtido);
            Assert.AreEqual(produtoEsperado, produtoObtido);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception), "O nome deve ser informado para essa busca")]
        public async Task DeveLancarExcecaoQuandoNomeNulo()
        {
            // Arrange
            string nomeProduto = "";
            var produtoService = new ProdutosService(_produtoRepositorioMock.Object);

            // Act
            await produtoService.ObterProdutoPorNome(nomeProduto);

            // Assert - Expects Exception
        }

        [TestMethod]
        [ExpectedException(typeof(Exception), "Produto não encontrado")]
        public async Task DeveLancarExcecaoQuandoProdutoNaoEncontrado()
        {
            // Arrange
            var nomeProduto = "Produto não encontrado";
            _produtoRepositorioMock.Setup(x => x.ObterPorNome(nomeProduto)).ReturnsAsync((Produto)null);

            var produtoService = new ProdutosService(_produtoRepositorioMock.Object);

            // Act
            var produtoObtido = await produtoService.ObterProdutoPorNome(nomeProduto);

            // Assert - Expects Exception
        }

        [TestMethod]
        public async Task DeveInserirProdutoComSucesso()
        {
            // Arrange
            var produto = new Produto { Nome = "Produto Teste", Valor = 10 };
            var listaProdutosEsperados = new List<Produto>
            {
                produto
            };

            _produtoRepositorioMock.Setup(x => x.Adicionar(produto)).Verifiable();

            _produtoRepositorioMock.Setup(x => x.ObterTodos()).ReturnsAsync(listaProdutosEsperados);

            var produtoService = new ProdutosService(_produtoRepositorioMock.Object);

            // Act
            var listaProdutosObtidos = await produtoService.InserirProduto(produto);

            // Assert
            Assert.IsNotNull(listaProdutosObtidos);
            CollectionAssert.AreEqual(listaProdutosEsperados, listaProdutosObtidos);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception), "O valor do produto não pode ser negativo")]
        public async Task DeveLancarExcecaoQuandoValorNegativo()
        {
            // Arrange
            var produto = new Produto { Nome = "Produto Teste", Valor = -10 };
            var produtoService = new ProdutosService(_produtoRepositorioMock.Object);

            // Act

            await produtoService.InserirProduto(produto);

            // Assert - Expects Exception
        }

        [TestMethod]
        public async Task DeveAtualizarProdutoComSucesso()
        {
            // Arrange
            var produtoAtualizar = new Produto
            {
                Id = 1,
                Nome = "Produto Teste Atualizado",
                Valor = 20,
                Estoque = 5,
                Tipo = TipoEnum.Eletronico,
                DataAtualizacao = DateTime.Now
            };
            var produtoBanco = new Produto
            {
                Id = 1,
                Nome = "Produto Teste",
                Valor = 10,
                Estoque = 10,
                Tipo = TipoEnum.Eletronico,
                DataAtualizacao = DateTime.Now.AddDays(-1)
            };
            var listaProdutosEsperados = new List<Produto>
            {
                produtoBanco
            };

            _produtoRepositorioMock.Setup(x => x.ObterPorId(1)).ReturnsAsync(produtoBanco);

            _produtoRepositorioMock.Setup(x => x.Atualizar(produtoBanco)).Verifiable();

            _produtoRepositorioMock.Setup(x => x.ObterTodos()).ReturnsAsync(listaProdutosEsperados);

            var produtoService = new ProdutosService(_produtoRepositorioMock.Object);

            // Act
            var listaProdutosObtidos = await produtoService.AtualizarProdutoPorId(produtoAtualizar);

            // Assert
            Assert.IsNotNull(listaProdutosObtidos);
            CollectionAssert.AreEqual(listaProdutosEsperados, listaProdutosObtidos);
            Assert.AreEqual(produtoAtualizar.Nome, produtoBanco.Nome);
            Assert.AreEqual(produtoAtualizar.Valor, produtoBanco.Valor);
            Assert.AreEqual(produtoAtualizar.Estoque, produtoBanco.Estoque);
            Assert.AreEqual(produtoAtualizar.Tipo, produtoBanco.Tipo);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception), "Erro ao atualizar o produto")]
        public async Task DeveLancarExcecaoQuandoProdutoNaoExiste()
        {
            // Arrange
            var produtoAtualizar = new Produto
            {
                Id = 1,
                Nome = "Produto Teste Atualizado",
                Valor = 20,
                Estoque = 5,
                Tipo = TipoEnum.Eletronico,
                DataAtualizacao = DateTime.Now
            };

            _produtoRepositorioMock.Setup(x => x.ObterPorId(1)).ReturnsAsync((Produto)null);

            var produtoService = new ProdutosService(_produtoRepositorioMock.Object);

            // Act
            var listaProdutosObtidos = await produtoService.AtualizarProdutoPorId(produtoAtualizar);

            // Assert - Expects Exception
        }

        [TestMethod]
        public async Task ExcluirProdutoPorId_Deve_Excluir_Produto_E_Retornar_Todos_Os_Produtos()
        {
            // Arrange
            var id = 1;
            var produto = new Produto 
            { 
                Id = id, 
                Nome = "Produto Teste", 
                Valor = 10.5m 
            };
            var produtos = new List<Produto>();
            _produtoRepositorioMock.Setup(pr => pr.ObterPorId(id)).ReturnsAsync(produto);
            _produtoRepositorioMock.Setup(pr => pr.ObterTodos()).ReturnsAsync(produtos);

            var produtoService = new ProdutosService(_produtoRepositorioMock.Object);

            // Act
            var resultado = await produtoService.ExcluirProdutoPorId(id);

            // Assert
            Assert.AreEqual(produtos, resultado);
        }

        [TestMethod]
        public async Task ExcluirProdutoPorId_Deve_Lancar_Excecao_Quando_Produto_Nao_Existir()
        {
            // Arrange
            var id = 1;

            Produto? produto = null;
            _produtoRepositorioMock.Setup(pr => pr.ObterPorId(id)).ReturnsAsync(produto);

            var produtoService = new ProdutosService(_produtoRepositorioMock.Object);

            // Assert
            await Assert.ThrowsExceptionAsync<Exception>(() => produtoService.ExcluirProdutoPorId(id));            
        }
    }
}