using ApiSolPetsDDD.Domain.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApiSolPetsDDD.Domain.Interfaces
{
    public interface IPedidoRepository
    {
        Task<Pedido> GetPedidosById(int idPedido);
        Task<List<Pedido>> GetPedidosByIdCliente(int idCliente);
        Task<List<Pedido>> GetPedidosDia();
        Task<List<Pedido>> GetPedidosMes();
        Task<List<Pedido>> GetPedidosAno();
        Task<Pedido> CadastrarPedido(Pedido pedido);
        Task<Pedido> FinalizarPedido(char statusVenda, int idPedido, bool finalizado);
        Task<int> CancelarPedido(char statusVenda, int idPedido, bool finalizado);
        Task<Pedido> VincularCliente(int idPedido, int idCliente);
        Task<Pedido> RemoveProdutoPedido(int idPedido, int idProduto, int qtdeProduto, double totalVenda);
        Task<Pedido> AtualizaQtdeProdutoPedido(int idPedido, int idProduto, int qtdeProduto, double totalVenda);
        Task<Pedido> AtualizaValorProdutoPedido(int idPedido, int idProduto, double totalVenda, double valorProduto);
        Task<Pedido> AdicionarProdutoPedido(PedidoProduto pedidoProduto);
        Task<int> AtualizarPedido(Pedido pedido, bool cancelamento = false);
        Task<int> ExcluirPedido(int idPedido);
        Task<Pedido> AtualizaValorDescontoTotalVenda(int idPedido, double valorDesconto, double totalVenda);
    }
}
