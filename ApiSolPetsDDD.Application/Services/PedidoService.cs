using ApiSolPetsDDD.Domain.Exceptions;
using ApiSolPetsDDD.Domain.Interfaces;
using ApiSolPetsDDD.Domain.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApiSolPetsDDD.Application.Services
{
    public class PedidoService : IPedidoService
    {
        private readonly IPedidoRepository pedidoRepository;
        private readonly IProdutoRepository produtoRepository;

        public PedidoService(IPedidoRepository pedidoRepository, IProdutoRepository produtoRepository)
        {
            this.pedidoRepository = pedidoRepository;
            this.produtoRepository = produtoRepository;
        }

        public async Task<Pedido> GetPedidosById(int idPedido)
        {
            try
            {
                if (idPedido == 0)
                    throw new ExcecaoNegocio("[Exceção de Negócio] - Obrigatório informar o id do pedido!");
                var result = await pedidoRepository.GetPedidosById(idPedido);

                return result;
            }
            catch (ExcecaoNegocio ex)
            {
                throw new ExcecaoNegocio(ex.Message);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<Pedido>> GetPedidosByIdCliente(int idCliente)
        {
            try
            {
                if (idCliente == 0)
                    throw new ExcecaoNegocio("[Exceção de Negócio] - Obrigatório informar o id do cliente!");
                var result = await pedidoRepository.GetPedidosByIdCliente(idCliente);

                return result;
            }
            catch (ExcecaoNegocio ex)
            {
                throw new ExcecaoNegocio(ex.Message);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<Pedido>> GetPedidosDia()
        {
            try
            {
                var result = await pedidoRepository.GetPedidosDia();
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<Pedido>> GetPedidosMes()
        {
            try
            {
                var result = await pedidoRepository.GetPedidosMes();
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<Pedido>> GetPedidosAno()
        {
            try
            {
                var result = await pedidoRepository.GetPedidosAno();
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Pedido> PostPedido(Pedido pedido)
        {
            try
            {
                pedido.StatusVenda = pedido.StatusVenda != '\0' ? pedido.StatusVenda :
                    throw new ExcecaoNegocio("[Exceção de Negócio] - Obrigatório informar o status da venda!");
                pedido.TotalVenda = pedido.TotalVenda > 0.0 ? pedido.TotalVenda :
                    throw new ExcecaoNegocio("[Exceção de Negócio] - Obrigatório informar o total da venda!");
                foreach (Produto prod in pedido.Produtos)
                {
                    prod.IdProduto = prod.IdProduto > 0 ? prod.IdProduto :
                        throw new ExcecaoNegocio("[Exceção de Negócio] - Obrigatório informar o id do produto!");

                    if (prod.QtdeEstoque < pedido.QtdeProdutos)
                    {
                        throw new ExcecaoNegocio($"[Exceção de Negócio] - A quantidade informada do produto {prod.NomeProduto} excede a quantidade em estoque ({prod.QtdeEstoque})!");
                    }
                }

                var result = await pedidoRepository.CadastrarPedido(pedido);
                return result;
            }
            catch (ExcecaoData ex)
            {
                throw new ExcecaoData(ex.Message);
            }
            catch (ExcecaoNegocio ex)
            {
                throw new ExcecaoNegocio(ex.Message);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Pedido> PatchFinalizarPedido(char statusVenda, int idPedido, bool finalizado)
        {
            try
            {
                if (idPedido == 0)
                    throw new ExcecaoNegocio("[Exceção de Negócio] - Obrigatório informar o id do pedido!");
                if (statusVenda == '\0')
                    throw new ExcecaoNegocio("[Exceção de Negócio] - Obrigatório informar o status da venda!");
                var result = await pedidoRepository.FinalizarPedido(statusVenda, idPedido, finalizado);

                return result;
            }
            catch (ExcecaoNegocio ex)
            {
                throw new ExcecaoNegocio(ex.Message);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<int> PatchCancelarPedido(char statusVenda, int idPedido, bool finalizado)
        {
            try
            {
                if (idPedido == 0)
                    throw new ExcecaoNegocio("[Exceção de Negócio] - Obrigatório informar o id do pedido!");
                var result = await pedidoRepository.CancelarPedido(statusVenda, idPedido, finalizado);

                return result;
            }
            catch (ExcecaoNegocio ex)
            {
                throw new ExcecaoNegocio(ex.Message);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Pedido> PatchVincularClientePedido(int idPedido, int idCliente)
        {
            try
            {
                if (idPedido == 0)
                    throw new ExcecaoNegocio("[Exceção de Negócio] - Obrigatório informar o código do pedido!");
                if (idCliente == 0)
                    throw new ExcecaoNegocio("[Exceção de Negócio] - Obrigatório informar o código do cliente!");
                var result = await pedidoRepository.VincularCliente(idPedido, idCliente);

                return result;
            }
            catch (ExcecaoNegocio ex)
            {
                throw new ExcecaoNegocio(ex.Message);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Pedido> PatchRemoveProdutoPedido(int idPedido, int idProduto, int qtdeProduto)
        {
            try
            {
                if (idPedido == 0)
                    throw new ExcecaoNegocio("[Exceção de Negócio] - Obrigatório informar o código do pedido!");
                if (idProduto == 0)
                    throw new ExcecaoNegocio("[Exceção de Negócio] - Obrigatório informar o código do produto!");

                var pedido = await pedidoRepository.GetPedidosById(idPedido);
                var produto = pedido.Pedidos.Find(_ => _.Produto.IdProduto == idProduto);
                double total = 0.0;
                int qtdeProdutos = 0;
                if (pedido?.Pedidos?.Count > 0)
                {
                    foreach (var produtoPedido in pedido.Pedidos)
                    {
                        var valorPedidoProduto = produtoPedido.Produto.ValorUnitarioVenda * produtoPedido.QtdeProdutos;
                        total += (double)valorPedidoProduto;
                        if (produtoPedido.Produto.IdProduto == idProduto)
                            qtdeProdutos = produtoPedido.QtdeProdutos;
                    }
                }

                total -= (produto.Produto.ValorUnitarioVenda * qtdeProdutos);

                return await pedidoRepository.RemoveProdutoPedido(idPedido, idProduto, qtdeProduto, total);
            }
            catch (ExcecaoNegocio ex)
            {
                throw new ExcecaoNegocio(ex.Message);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Pedido> PatchAtualizaQtdeProdutoPedido(int idPedido, int idProduto, int qtdeProduto, double totalVenda)
        {
            try
            {
                if (idPedido == 0)
                    throw new ExcecaoNegocio("[Exceção de Negócio] - Obrigatório informar o código do pedido!");
                if (idProduto == 0)
                    throw new ExcecaoNegocio("[Exceção de Negócio] - Obrigatório informar o código do produto!");
                var result = await pedidoRepository.AtualizaQtdeProdutoPedido(idPedido, idProduto, qtdeProduto, totalVenda);

                return result;
            }
            catch (ExcecaoNegocio ex)
            {
                throw new ExcecaoNegocio(ex.Message);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Pedido> PatchAdicionarProdutoPedido(PedidoProduto pedidoProduto)
        {
            try
            {
                if ((int)pedidoProduto.IdPedido == 0)
                    throw new ExcecaoNegocio("[Exceção de Negócio] - Obrigatório informar o código do pedido!");
                if (pedidoProduto.Produto.IdProduto == 0)
                    throw new ExcecaoNegocio("[Exceção de Negócio] - Obrigatório informar o código do produto!");
                var result = await pedidoRepository.AdicionarProdutoPedido(pedidoProduto);

                return result;
            }
            catch (ExcecaoNegocio ex)
            {
                throw new ExcecaoNegocio(ex.Message);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Pedido> PatchAtualizaDesconTotalVenda(int idPedido, double totalVenda, double valorDesconto)
        {
            try
            {
                if (idPedido == 0)
                    throw new ExcecaoNegocio("[Exceção de Negócio] - Obrigatório informar o código do pedido!");
                var result = await pedidoRepository.AtualizaValorDescontoTotalVenda(idPedido, valorDesconto, totalVenda);

                return result;
            }
            catch (ExcecaoNegocio ex)
            {
                throw new ExcecaoNegocio(ex.Message);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Pedido> PatchAtualizaValorProduto(int idPedido, int idProduto, double totalVenda, double valorProduto)
        {
            try
            {
                if (idPedido == 0)
                    throw new ExcecaoNegocio("[Exceção de Negócio] - Obrigatório informar o código do pedido!");
                if (idProduto == 0)
                    throw new ExcecaoNegocio("[Exceção de Negócio] - Obrigatório informar o código do produto!");
                if (valorProduto == 0.0)
                    throw new ExcecaoNegocio($"[Exceção de Negócio] - Obrigatório informar o valor do produto! \nValor informado R$ {valorProduto}");
                var result = await pedidoRepository.AtualizaValorProdutoPedido(idPedido, idProduto, totalVenda, valorProduto);

                return result;
            }
            catch (ExcecaoNegocio ex)
            {
                throw new ExcecaoNegocio(ex.Message);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<int> PutPedido(Pedido pedido)
        {
            var dataMaxima = DateTime.MaxValue;
            var dataMinima = DateTime.MinValue;
            try
            {
                pedido.IdPedido = pedido.IdPedido > 0 ? pedido.IdPedido :
                    throw new ExcecaoNegocio("[Exceção de Negócio] - Obrigatório informar o id do pedido!");
                pedido.StatusVenda = pedido.StatusVenda != '\0' ? pedido.StatusVenda :
                    throw new ExcecaoNegocio("[Exceção de Negócio] - Obrigatório informar o status da venda!");
                pedido.TotalVenda = pedido.TotalVenda > 0.0 ? pedido.TotalVenda :
                    throw new ExcecaoNegocio("[Exceção de Negócio] - Obrigatório informar o total da venda!");
                foreach (Produto prod in pedido.Produtos)
                {
                    prod.IdProduto = prod.IdProduto > 0 ? prod.IdProduto :
                        throw new ExcecaoNegocio("[Exceção de Negócio] - Obrigatório informar o id do produto!");
                }

                var result = await pedidoRepository.AtualizarPedido(pedido);
                return result;
            }
            catch (ExcecaoData ex)
            {
                throw new ExcecaoData(ex.Message);
            }
            catch (ExcecaoNegocio ex)
            {
                throw new ExcecaoNegocio(ex.Message);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<int> DeletePedido(int idPedido)
        {
            try
            {
                if (idPedido == 0)
                    throw new ExcecaoNegocio("[Exceção de Negócio] - Obrigatório informar o id do pedido!");
                var result = await pedidoRepository.ExcluirPedido(idPedido);

                return result;
            }
            catch (ExcecaoNegocio ex)
            {
                throw new ExcecaoNegocio(ex.Message);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
