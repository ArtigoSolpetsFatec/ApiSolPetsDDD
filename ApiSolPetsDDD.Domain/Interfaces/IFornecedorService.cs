using ApiSolPetsDDD.Domain.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApiSolPetsDDD.Domain.Interfaces
{
    public interface IFornecedorService
    {
        Task<Fornecedor> PostFornecedor(Fornecedor fornecedor);
        Task<Fornecedor> GetInfoFornecedor(int idFornecedor);
        Task<List<Fornecedor>> GetInfoFornecedorByCnpj(string cnpjFornecedor);
        Task<int> PutFornecedor(Fornecedor fornecedor);
        Task<int> DeleteFornecedor(int idFornecedor);
        Task<List<Fornecedor>> GetFornecedores();
    }
}
