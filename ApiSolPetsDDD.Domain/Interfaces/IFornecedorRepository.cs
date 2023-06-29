using ApiSolPetsDDD.Domain.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApiSolPetsDDD.Domain.Interfaces
{
    public interface IFornecedorRepository
    {
        Task<Fornecedor> CadastrarFornecedor(Fornecedor fornecedor);
        Task<Fornecedor> GetFornecedorById(int idFornecedor);
        Task<List<Fornecedor>> GetFornecedorByCnpj(string cnpjFornecedor);
        Task<int> AtualizaFornecedor(Fornecedor fornecedor);
        Task<int> ExcluirFornecedor(int idFornecedor);
        Task<List<Fornecedor>> GetAllFornecedores();
    }
}
