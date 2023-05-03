using ProjetoWakeCommerce.Entidades;

namespace ProjetoWakeCommerce.Application.Interfaces
{
    public interface IProdutosService
    {
        Task<List<Produto>> ObterProdutosPorFiltro();
        Task<Produto> ObterProdutoPorId(int id);
        Task<List<Produto>> AtualizarProdutoPorId(Produto produto);
        Task<List<Produto>> ExcluirProdutoPorId(int id);
        Task<List<Produto>> InserirProduto(Produto produto);
    }
}
