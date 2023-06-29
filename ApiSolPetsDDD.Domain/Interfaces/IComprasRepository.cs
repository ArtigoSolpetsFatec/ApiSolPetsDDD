using ApiSolPetsDDD.Domain.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApiSolPetsDDD.Domain.Interfaces
{
    public interface IComprasRepository
    {
        Task<List<Compra>> GetInfoCompras(DateTime dataInicio, DateTime dataFim);
        Task<List<Compra>> GetComprasMes();
        Task<List<Compra>> GetComprasDia();
        Task<List<Compra>> GetComprasAno();
        Task<Compra> CadastrarCompra(Compra compra);
        Task<int> AtualizarCompra(Compra compra);
        Task<int> ExcluirCompra(int idCompra);
    }
}
