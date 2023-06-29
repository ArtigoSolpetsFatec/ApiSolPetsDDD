using ApiSolPetsDDD.Domain.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApiSolPetsDDD.Domain.Interfaces
{
    public interface IContasRepository
    {
        Task<PagamentoContasFixas> GetContaById(int idConta);
        Task<List<PagamentoContasFixas>> GetContasAVencer();
        Task<List<PagamentoContasFixas>> GetContasVencidas();
        Task<List<PagamentoContasFixas>> GetContasAVencerMesCorrente();
        Task<PagamentoContasFixas> GetContaPagaById(int idPagamentoConta);
        Task<ContasFixas> CadastrarConta(ContasFixas contas);
        Task<PagamentoContasFixas> CadastrarPagamentoConta(PagamentoContasFixas pagConta);
        Task<int> AtualizarConta(ContasFixas contas);
        Task<int> AtualizarPagamentoConta(PagamentoContasFixas pagConta);
        Task<int> ExcluirConta(int idConta);
        Task<int> ExcluirPagamentoConta(int idPagConta);
        Task<List<ContasFixas>> GetContasDoMes();
        Task<List<ContasFixas>> GetContasDoDia();
        Task<List<ContasFixas>> GetContasDoAno();
    }
}
