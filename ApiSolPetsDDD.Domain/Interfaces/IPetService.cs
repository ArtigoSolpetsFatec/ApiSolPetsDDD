using ApiSolPetsDDD.Domain.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApiSolPetsDDD.Domain.Interfaces
{
    public interface IPetService
    {
        Task<List<Pet>> GetPetByNome(string nomePet);
        Task<Pet> PostPet(Pet pet, int idCliente);
        Task<int> PutPet(Pet petCliente);
        Task<int> DeletePet(int idCliente, int idPet);
    }
}
