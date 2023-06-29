using APISolPets.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApiSolPetsDDD.Domain.Interfaces
{
    public interface IClienteService
    {
        Task<List<Cliente>> GetClienteByCpf(string cpf);
        Task<List<Cliente>> GetClienteByNome(string nomeCliente);
        Task<List<Cliente>> GetClientes(int limit);
        Task<Cliente> GetClienteById(int idCliente);
        Task<Cliente> PostCliente(Cliente cliente);
        Task<int> PutCliente(Cliente cliente);
        Task<int> DeleteCliente(int idCliente);
    }
}
