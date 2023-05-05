using ProjetoWakeCommerce.Entidades;
using ProjetoWakeCommerce.Repositorio.Interfaces;
using System;
using System.Linq;

namespace ProjetoWakeCommerce.Repositorio.Repositorios
{
    public class ProdutoRepositorio : RepositorioBase<Produto>, IProdutoRepositorio
    {
        public async Task<Produto> ObterPorNome(string nome)
        {
            return _ctx.Set<Produto>().Where(p => p.Nome == nome).FirstOrDefault();
        }
    }
}
