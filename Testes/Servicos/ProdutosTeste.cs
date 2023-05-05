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
        private readonly Mock<Produto> mock = new Mock<Produto>();

        [TestMethod]
        public void ObterTodosProdutos()
        {

            // act
            var mock = new ProdutoRepositorioMock();
            var produtoServico = new ProdutosService((IProdutoRepositorio)mock);
            var servico = produtoServico.ObterProdutosOrdenados();

            // assert
            Assert.IsTrue(servico.IsCompletedSuccessfully);
        }
    }
}