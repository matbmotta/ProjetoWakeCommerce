using Microsoft.EntityFrameworkCore;
using ProjetoWakeCommerce.Application.Interfaces;
using ProjetoWakeCommerce.Data;
using ProjetoWakeCommerce.Entidades;
using ProjetoWakeCommerce.Repositorio.Interfaces;
using ProjetoWakeCommerce.Repositorio.Repositorios;

namespace ProjetoWakeCommerce.Application.Services
{
    public class ProdutosService : IProdutosService
    {
        private readonly IProdutoRepositorio ProdutoRepositorio;

        public ProdutosService(IProdutoRepositorio produtoRepositorio)
        {
            ProdutoRepositorio = produtoRepositorio;
        }
        #region OBTER
        public async Task<List<Produto>> ObterProdutosOrdenados()
        {
            var listaProdutos = await ProdutoRepositorio.ObterTodos();
            if (listaProdutos == null)
                throw new Exception("Nenhum produto encontrado");

            listaProdutos.OrderBy(p => p.Nome).ThenBy(p => p.Estoque);
            return (List<Produto>)listaProdutos;
        }

        public async Task<Produto> ObterProdutoPorId(int id)
        {
            if (id == 0)
                throw new Exception("O id informado está incorreto");

            var produto = await ProdutoRepositorio.ObterPorId(id);

            if (produto == null)
                throw new Exception("Produto não encontrado");

            return produto;
        }

        public async Task<Produto> ObterProdutoPorNome(string nome)
        {
            if (nome == null)
                throw new Exception("O nome deve ser informado para essa busca");

            var produto = await ProdutoRepositorio.ObterPorNome(nome);

            if (produto == null)
                throw new Exception("Produto não encontrado");  

            return produto;
        }
        #endregion

        #region INSERIR
        public async Task<List<Produto>> InserirProduto(Produto produto)
        {
            if (produto.Valor < 0)
                throw new Exception("O valor do produto não pode ser negativo");

            ProdutoRepositorio.Adicionar(produto);
            ProdutoRepositorio.Commit();

            return (List<Produto>)await ProdutoRepositorio.ObterTodos();
        }
        #endregion

        #region ATUALIZAR
        public async Task<List<Produto>> AtualizarProdutoPorId(Produto produto)
        {
            var produtoBanco = await ProdutoRepositorio.ObterPorId(produto.Id);

            if (produtoBanco == null)
                throw new Exception("Erro ao atualizar o produto");
            if (produto.Valor < 0)
                throw new Exception("O valor do produto não pode ser negativo");

            produtoBanco.Valor = produto.Valor;
            produtoBanco.Nome = produto.Nome;
            produtoBanco.Estoque = produto.Estoque;
            produtoBanco.Tipo = produto.Tipo;
            produtoBanco.DataAtualizacao = DateTime.Now;

            ProdutoRepositorio.Atualizar(produtoBanco);
            ProdutoRepositorio.Commit();

            return (List<Produto>)await ProdutoRepositorio.ObterTodos();
        }
        #endregion

        #region EXCLUIR
        public async Task<List<Produto>> ExcluirProdutoPorId(int id)
        {
            var produto = await ProdutoRepositorio.ObterPorId(id);

            if (produto == null)
                throw new Exception("Erro ao excluir o produto");


            ProdutoRepositorio.Deletar(produto);
            ProdutoRepositorio.Commit();

            return (List<Produto>)await ProdutoRepositorio.ObterTodos();
        }
        #endregion
    }
}
