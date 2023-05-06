using Microsoft.EntityFrameworkCore;
using ProjetoWakeCommerce.Entidades;
using ProjetoWakeCommerce.Repositorio.Interfaces;
using System;
using System.Linq;
using WakeCommerce.Database;

namespace ProjetoWakeCommerce.Repositorio.Repositorios
{
    public class ProdutoRepositorio : RepositorioBase<Produto>, IProdutoRepositorio
    {
        public ProdutoRepositorio(DataContext ctx) : base(ctx)
        {
        }

        public async Task<Produto> ObterPorNome(string nome)
        {
            var produto = await _ctx.Set<Produto>().FirstOrDefaultAsync(p => p.Nome == nome);

            if (produto == null)
                throw new KeyNotFoundException("Produto não encontrado");

            return produto;
        }
    }
}
