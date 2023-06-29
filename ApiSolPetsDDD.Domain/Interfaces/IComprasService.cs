using ApiSolPetsDDD.Domain.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApiSolPetsDDD.Domain.Interfaces
{
    public interface IComprasService
    {
        Task<List<Compra>> GetCompras(string dataInicio, string dataFim);
        Task<Compra> PostCompra(Compra compra);
        Task<int> PutCompra(Compra compra);
        Task<int> DeleteCompra(int idCompra);
    }
}
