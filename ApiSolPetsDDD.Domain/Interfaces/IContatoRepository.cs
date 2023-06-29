using ApiSolPetsDDD.Domain.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApiSolPetsDDD.Domain.Interfaces
{
    public interface IContatoRepository
    {
        Task<List<Contato>> GetContatos(int idCliente, int idFornecedor);
        Task<int> ExcluirContato(int idCliente, int idContato, int idFornecedor);
        Task<int> AtualizaContato(Contato contatoCliente);
        Task<Contato> CadastrarContato(Contato contato);
    }
}
