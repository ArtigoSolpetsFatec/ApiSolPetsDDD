using ApiSolPetsDDD.Domain.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApiSolPetsDDD.Domain.Interfaces
{
    public interface IContatoService
    {
        Task<List<Contato>> GetContatosByIdClienteOrFonecedor(int idCliente, int idFornecedor);
        Task<Contato> PostContato(Contato contato);
        Task<int> PutContato(Contato contatoCliente);
        Task<int> DeleteContato(int idContato, int idCliente, int idFornecedor);
    }
}
