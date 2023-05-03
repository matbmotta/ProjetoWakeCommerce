using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjetoWakeCommerce.Application.Interfaces;
using ProjetoWakeCommerce.Entidades;

namespace ProjetoWakeCommerce.Application.Services
{
    public class ProdutosService : IProdutosService
    {
        private readonly DataContext dataContext;

        public ProdutosService(DataContext context)
        {
            this.dataContext = context;
        }
        #region OBTER
        public async Task<List<Produto>> ObterProdutosPorFiltro()
        {
            var listaProdutos = await dataContext.Produtos.ToListAsync();
            if (listaProdutos == null)
                throw new Exception("Nenhum produto encontrado");

            return listaProdutos;
        }

        public async Task<Produto> ObterProdutoPorId(int id)
        {
            if (id == 0)
                throw new Exception("O id informado está incorreto");

            var produto = await dataContext.Produtos.FirstOrDefaultAsync(p => p.Id == id);

            if (produto == null)
                throw new Exception("Produto não encontrado");

            return produto;
        }
        #endregion

        #region INSERIR
        public async Task<List<Produto>> InserirProduto(Produto produto)
        {
            dataContext.Produtos.Add(produto);
            await dataContext.SaveChangesAsync();

            return (await dataContext.Produtos.ToListAsync());
        }
        #endregion

        #region ATUALIZAR
        public async Task<List<Produto>> AtualizarProdutoPorId(Produto produto)
        {
            var produtoBanco = await dataContext.Produtos.FirstOrDefaultAsync(p => p.Id == produto.Id);

            if (produtoBanco == null)
                throw new Exception("Erro ao atualizar o produto");

            produtoBanco.Valor = produto.Valor;
            produtoBanco.Nome = produto.Nome;
            produtoBanco.Estoque = produto.Estoque;

            await dataContext.SaveChangesAsync();

            return (await dataContext.Produtos.ToListAsync());
        }
        #endregion

        #region EXCLUIR
        public async Task<List<Produto>> ExcluirProdutoPorId(int id)
        {
            var produto = await dataContext.Produtos.FirstOrDefaultAsync(p => p.Id == id);

            if (produto == null)
                throw new Exception("Erro ao excluir o produto");


            dataContext.Produtos.Remove(produto);
            await dataContext.SaveChangesAsync();

            return (await dataContext.Produtos.ToListAsync());
        }
        #endregion
    }
}
