using ProjetoWakeCommerce.Entidades;

namespace ProjetoWakeCommerce.Repositorio.Interfaces
{
    public interface IProdutoRepositorio : IRepositorioBase<Produto>
    {
        Task<Produto> ObterPorNome(string nome);
    }
}
