using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApiSolPetsDDD.Domain.Interfaces
{
    public interface ICargoRepository
    {
        Task<Cargo> GetCargoById(int idCargo);
        Task<Cargo> GetCargoByName(string descricaoCargo);
        Task<Cargo> CadastrarCargo(Cargo cargo);
        Task<List<Cargo>> GetCargos();
        Task<int> ExcluirCargo(int idCargo);
        Task<int> AtualizarCargo(Cargo cargo);
    }
}
