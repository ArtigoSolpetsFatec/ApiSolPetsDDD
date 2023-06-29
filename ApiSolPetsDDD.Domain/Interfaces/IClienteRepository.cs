using APISolPets.Domain.Models;
using ApiSolPetsDDD.Domain.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApiSolPetsDDD.Domain.Interfaces
{
    public interface IClienteRepository
    {
        Task<Cliente> GetInfoCliente(int idCliente);
        Task<List<Pet>> GetPetNome(string nomePet);
        Task<List<Cliente>> GetClienteNome(string nomeCliente);
        Task<List<Cliente>> GetClienteCpf(string cpf);
        Task<List<Cliente>> GetInfoClientes(int limit);
        Task<Cliente> CadastrarCliente(Cliente cliente);
        Task<Endereco> CadastrarEnderecoCliente(Endereco endereco, int idCliente);
        Task<Pet> CadastrarPetCliente(Pet pet, int idCliente);
        Task<int> AtualizaCliente(Cliente cliente);
        Task<int> AtualizaPetCliente(Pet petCliente);
        Task<int> AtualizaEnderecoCliente(Endereco enderCliente);
        Task<int> ExcluirCliente(int idCliente);
        Task<int> ExcluirEndeCliente(int idCliente, int idEndereco);
        Task<int> ExcluirPetCliente(int idCliente, int idPet);
    }
}
