using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
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
        public async Task<List<Produto>> ObterProdutosOrdenados()
        {
            var listaProdutos = await dataContext.Produtos.OrderBy(p => p.Nome).ThenBy(p => p.Estoque).ToListAsync();
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

        public async Task<Produto> ObterProdutoPorNome(string nome)
        {
            if (nome.IsNullOrEmpty())
                throw new Exception("O nome deve ser informado para essa busca");

            var produto = await dataContext.Produtos.FirstOrDefaultAsync(p => p.Nome == nome);

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
            if (produto.Valor < 0)
                throw new Exception("O valor do produto não pode ser negativo");

            produtoBanco.Valor = produto.Valor;
            produtoBanco.Nome = produto.Nome;
            produtoBanco.Estoque = produto.Estoque;
            produtoBanco.Tipo = produto.Tipo;
            produtoBanco.DataAtualizacao = DateTime.Now;

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
