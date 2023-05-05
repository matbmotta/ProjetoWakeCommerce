using Microsoft.EntityFrameworkCore;
using Moq;
using ProjetoWakeCommerce.Application.Interfaces;
using ProjetoWakeCommerce.Application.Services;
using ProjetoWakeCommerce.Data;
using ProjetoWakeCommerce.Entidades;
using WakeCommerce.Utils.Enums;
using Xunit.Sdk;

namespace Testes
{
    [TestClass]
    public class ProdutosTeste
    {
        private readonly Mock<Produto> mock = new Mock<Produto>();

        [TestMethod]
        public void ObterTodosProdutos()
        {
            Produto produto = new Produto()
            {
                Id = 1,
                Estoque = 10,
                Nome = "Camiseta",
                Tipo = TipoEnum.Vestuario,
                Valor = 15.99m,
                DataCriacao = DateTime.Now,
            };

            Moq.Mock<IProdutosService> mock = new Mock<IProdutosService>();
            mock.Setup(x => x.ObterProdutoPorId(It.IsAny<int>()).Result);

            // act

            var resultadoEsperado = mock.Object.ObterProdutoPorId(2);
            
            // assert
            Assert.IsTrue(resultadoEsperado.IsCompletedSuccessfully);
        }
    }
}