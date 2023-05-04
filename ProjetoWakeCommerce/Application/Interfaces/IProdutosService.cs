using ProjetoWakeCommerce.Entidades;

namespace ProjetoWakeCommerce.Application.Interfaces
{
    public interface IProdutosService
    {
        Task<List<Produto>> ObterProdutosOrdenados();
        Task<Produto> ObterProdutoPorId(int id);
        Task<Produto> ObterProdutoPorNome(string nome);
        Task<List<Produto>> InserirProduto(Produto produto);
        Task<List<Produto>> AtualizarProdutoPorId(Produto produto);
        Task<List<Produto>> ExcluirProdutoPorId(int id);
    }
}
