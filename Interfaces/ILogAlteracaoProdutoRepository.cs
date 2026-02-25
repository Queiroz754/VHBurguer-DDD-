using VHBurguer3.Domains;

namespace VHBurguer3.Interfaces
{
    public interface ILogAlteracaoProdutoRepository
    {
        List<Log_AlteracaoProduto> Listar();
        List<Log_AlteracaoProduto> ListarPorProduto(int produtoId);
    }
}
