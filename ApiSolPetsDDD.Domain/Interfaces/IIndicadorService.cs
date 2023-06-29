using ApiSolPetsDDD.Domain.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApiSolPetsDDD.Domain.Interfaces
{
    public interface IIndicadorService
    {
        Task<List<Indicador>> GetIndicadoresByMeses(int meses);
        Task<List<Indicador>> GetIndicadoresDia();
    }
}
