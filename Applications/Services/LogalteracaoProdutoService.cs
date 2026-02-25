using VHBurguer3.Domains;
using VHBurguer3.DTOs.AutentificacaoDTO;
using VHBurguer3.DTOs.CategoriaDTO;
using VHBurguer3.DTOs.LogProdutoDto;
using VHBurguer3.Interfaces;

namespace VHBurguer3.Applications.Services
{
    public class LogAlteracaoProdutoService
    {
        private readonly ILogAlteracaoProdutoRepository _repository;
        public LogAlteracaoProdutoService(ILogAlteracaoProdutoRepository repository)
        {
            _repository = repository;
        }

        public List<LerLogProdutoDto> Listar()
        {
            List<Log_AlteracaoProduto> log = _repository.Listar();

            List<LerLogProdutoDto> listaLogProduto = log.Select(log => new LerLogProdutoDto
                {
                LogID = log.Log_AlteracaoProdutoID,
                ProdutoID = log.ProdutoID,
                NomeAnterior = log.NomeAnterior,
                PrecoAnterior = log.PrecoAnterior,
                DataAlteracao= log.DataAlteracao
                }).ToList();

            return listaLogProduto;
       } 
        public List<LerLogProdutoDto> ListarPorProduto(int produtoId)
        {
            List<Log_AlteracaoProduto> logs = _repository.ListarPorProduto(produtoId);

            List<LerLogProdutoDto> listarLogProduto = logs.Select(log => new LerLogProdutoDto
            {
                LogID = log.Log_AlteracaoProdutoID,
                ProdutoID = log.ProdutoID,
                NomeAnterior = log.NomeAnterior,
                PrecoAnterior = log.PrecoAnterior,
                DataAlteracao = log.DataAlteracao,
            }).ToList();

            return listarLogProduto;
        }
    }
}
