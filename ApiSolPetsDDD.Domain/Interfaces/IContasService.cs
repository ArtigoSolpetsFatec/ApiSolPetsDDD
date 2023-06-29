using ApiSolPetsDDD.Domain.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApiSolPetsDDD.Domain.Interfaces
{
    public interface IContasService
    {
        Task<PagamentoContasFixas> GetContaById(int idConta);
        Task<List<PagamentoContasFixas>> GetContasAVencer();
        Task<List<PagamentoContasFixas>> GetContasVencidas();
        Task<List<PagamentoContasFixas>> GetContasAVencerMesCorrente();
        Task<PagamentoContasFixas> GetContaPagaById(int idPagamentoConta);
        Task<ContasFixas> PostConta(ContasFixas contas);
        Task<PagamentoContasFixas> PostPagamentoConta(PagamentoContasFixas pagConta);
        Task<int> PutConta(ContasFixas contas);
        Task<int> PutPagamentoConta(PagamentoContasFixas pagConta);
        Task<int> DeleteConta(int idConta);
        Task<int> DeletePagamentoConta(int idPagConta);
    }
}
