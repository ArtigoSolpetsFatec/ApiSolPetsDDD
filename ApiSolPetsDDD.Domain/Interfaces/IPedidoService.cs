using ApiSolPetsDDD.Domain.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApiSolPetsDDD.Domain.Interfaces
{
    public interface IPedidoService
    {
        Task<Pedido> GetPedidosById(int idPedido);
        Task<List<Pedido>> GetPedidosByIdCliente(int idCliente);
        Task<List<Pedido>> GetPedidosDia();
        Task<List<Pedido>> GetPedidosMes();
        Task<List<Pedido>> GetPedidosAno();
        Task<Pedido> PostPedido(Pedido pedido);
        Task<Pedido> PatchFinalizarPedido(char statusVenda, int idPedido, bool finalizado);
        Task<int> PatchCancelarPedido(char statusVenda, int idPedido, bool finalizado);
        Task<Pedido> PatchVincularClientePedido(int idPedido, int idCliente);
        Task<Pedido> PatchRemoveProdutoPedido(int idPedido, int idProduto, int qtdeProduto);
        Task<Pedido> PatchAtualizaQtdeProdutoPedido(int idPedido, int idProduto, int qtdeProduto, double totalVenda);
        Task<Pedido> PatchAdicionarProdutoPedido(PedidoProduto pedidoProduto);
        Task<Pedido> PatchAtualizaDesconTotalVenda(int idPedido, double totalVenda, double valorDesconto);
        Task<Pedido> PatchAtualizaValorProduto(int idPedido, int idProduto, double totalVenda, double valorProduto);
        Task<int> PutPedido(Pedido pedido);
        Task<int> DeletePedido(int idPedido);
    }
}
