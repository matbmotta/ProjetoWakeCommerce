using Moq;
using ProjetoWakeCommerce.Entidades;
using ProjetoWakeCommerce.Repositorio.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace WakeCommerce.Tests.Mocks
{
    public class ProdutoRepositorioMock
    {
        private readonly Mock<IProdutoRepositorio> mock = new Mock<IProdutoRepositorio>();

        public static ProdutoRepositorioMock Instacia()
        {
            return new ProdutoRepositorioMock();
        }

        public IProdutoRepositorio Mock()
        {
            return mock.Object;
        }

        public ProdutoRepositorioMock ObterTodos()
        {
            List<Produto> produtos = new List<Produto>()
            {
                new Produto
                {
                    Id = 1,
                    Nome = "Camiseta",
                    Estoque = 10,
                    Tipo = Utils.Enums.TipoEnum.Vestuario,
                    Valor = 55.99m,
                    DataCriacao = DateTime.Now,
                    DataAtualizacao = DateTime.Now,
                },
                new Produto
                {
                    Id = 2,
                    Nome = "Bermuda",
                    Estoque = 25,
                    Tipo = Utils.Enums.TipoEnum.Vestuario,
                    Valor = 35.99m,
                    DataCriacao = DateTime.Now,
                    DataAtualizacao = DateTime.Now,
                }
            };
            var enume = produtos.AsEnumerable();
            mock.Setup(mock => mock.ObterTodos()).Returns(Task.FromResult(enume));
            return this;
        }

        public ProdutoRepositorioMock ObterPorId()
        {
            var produto = new Produto()
            {
                Id = 1,
                Nome = "Camiseta",
                Estoque = 10,
                Tipo = Utils.Enums.TipoEnum.Vestuario,
                Valor = 55.99m,
                DataCriacao = DateTime.Now,
                DataAtualizacao = DateTime.Now,
            };

            mock.Setup(mock => mock.ObterPorId(It.IsAny<int>())).Returns(Task.FromResult(produto));
            return this;
        }

        public ProdutoRepositorioMock ObterPorNome()
        {
            var produto = new Produto()
            {
                Id = 1,
                Nome = "Camiseta",
                Estoque = 10,
                Tipo = Utils.Enums.TipoEnum.Vestuario,
                Valor = 55.99m,
                DataCriacao = DateTime.Now,
                DataAtualizacao = DateTime.Now,
            };

            mock.Setup(mock => mock.ObterPorNome(It.IsAny<string>())).Returns(Task.FromResult(produto));
            return this;
        }

        public ProdutoRepositorioMock Adicionar()
        {
            mock.Setup(mock => mock.Adicionar(It.IsAny<Produto>()));
            return this;
        }

        public ProdutoRepositorioMock Atualizar()
        {
            mock.Setup(mock => mock.Atualizar(It.IsAny<Produto>()));
            return this;
        }

        public ProdutoRepositorioMock Deletar()
        {
            mock.Setup(mock => mock.Deletar(It.IsAny<Produto>()));
            return this;
        }

    }
}
