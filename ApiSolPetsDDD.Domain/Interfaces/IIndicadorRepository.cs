using ApiSolPetsDDD.Domain.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApiSolPetsDDD.Domain.Interfaces
{
    public interface IIndicadorRepository
    {
        Task<List<Indicador>> GetIndicadoresDia();
        Task<List<Indicador>> GetIndicadoresMes();
        Task<List<Indicador>> GetIndicadoresAno();
    }
}
