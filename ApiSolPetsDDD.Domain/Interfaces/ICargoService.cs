using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApiSolPetsDDD.Domain.Interfaces
{
    public interface ICargoService
    {
        Task<Cargo> PostCargo(Cargo cargo);
        Task<Cargo> GetCargo(int idCargo);
        Task<Cargo> GetCargoByName(string descricaoCargo);
        Task<List<Cargo>> GetCargos();
        Task<int> PutCargo(Cargo cargo);
        Task<int> DeleteCargo(int idCargo);
    }
}
