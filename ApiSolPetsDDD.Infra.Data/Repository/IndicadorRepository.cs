using ApiSolPetsDDD.Domain.Interfaces;
using ApiSolPetsDDD.Domain.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApiSolPetsDDD.Infra.Data.Repository
{
    public class IndicadorRepository : IIndicadorRepository
    {
        private readonly IPedidoRepository pedidoRepository;
        private readonly IContasRepository contasRepository;
        private readonly IComprasRepository comprasRepository;
        private readonly IProdutoRepository produtoRepository;

        public IndicadorRepository(IPedidoRepository pedidoRepository, IContasRepository contasRepository,
            IComprasRepository comprasRepository, IProdutoRepository produtoRepository)
        {
            this.pedidoRepository = pedidoRepository;
            this.contasRepository = contasRepository;
            this.comprasRepository = comprasRepository;
            this.produtoRepository = produtoRepository;
        }

        public async Task<List<Indicador>> GetIndicadoresAno()
        {
            var result = new List<Indicador>();
            var pedidosFinalizados = new List<Pedido>();
            var pedidosCancelados = new List<Pedido>();
            double totalFinalizados = 0.0;
            double totalCancelados = 0.0;
            double totalContas = 0.0;
            double totalEstoque = 0.0;
            try
            {
                var pedidos = await pedidoRepository.GetPedidosAno();
                foreach (var pedido in pedidos)
                {
                    if (pedido.StatusVenda == 'F')
                    {
                        pedidosFinalizados.Add(pedido);
                    }
                    else if (pedido.StatusVenda == 'C')
                    {
                        pedidosCancelados.Add(pedido);
                    }
                }
                var contasMes = await contasRepository.GetContasDoAno();
                var custoEstoque = await produtoRepository.GetCustoTotalEstoque();

                foreach (var pedidoFinalizado in pedidosFinalizados)
                {
                    totalFinalizados += pedidoFinalizado.TotalVenda;
                }
                var indicadorFinalizados = new Indicador()
                {
                    DescricaoIndicador = "Vendas Finalizadas",
                    ValorIndicador = totalFinalizados
                };
                result.Add(indicadorFinalizados);
                foreach (var pedidoCancelado in pedidosCancelados)
                {
                    totalCancelados += pedidoCancelado.TotalVenda;
                }
                var indicadorCancelados = new Indicador()
                {
                    DescricaoIndicador = "Vendas Canceladas",
                    ValorIndicador = totalCancelados
                };
                result.Add(indicadorCancelados);
                foreach (var contaFixa in contasMes)
                {
                    totalContas += contaFixa.ValorConta;
                }
                var indicadorContas = new Indicador()
                {
                    DescricaoIndicador = "Custo Contas",
                    ValorIndicador = totalContas
                };
                result.Add(indicadorContas);
                foreach (var produto in custoEstoque)
                {
                    totalEstoque += (produto.ValorUnitarioCusto * produto.QtdeEstoque);
                }
                var custoEstoqueAno = (totalEstoque / 365);

                var indicadorCompras = new Indicador()
                {
                    DescricaoIndicador = "Custo Estoque",
                    ValorIndicador = custoEstoqueAno
                };

                result.Add(indicadorCompras);
                var totalLucroAno = totalFinalizados - (totalContas + custoEstoqueAno);
                var indicadorLucro = new Indicador()
                {
                    DescricaoIndicador = "Lucro",
                    ValorIndicador = totalLucroAno
                };
                result.Add(indicadorLucro);

                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<Indicador>> GetIndicadoresDia()
        {
            var result = new List<Indicador>();
            var pedidosFinalizados = new List<Pedido>();
            var pedidosCancelados = new List<Pedido>();
            double totalFinalizados = 0.0;
            double totalCancelados = 0.0;
            double totalContas = 0.0;
            double totalEstoque = 0.0;
            try
            {
                var pedidos = await pedidoRepository.GetPedidosDia();
                foreach (var pedido in pedidos)
                {
                    if (pedido.StatusVenda == 'F')
                    {
                        pedidosFinalizados.Add(pedido);
                    }
                    else if (pedido.StatusVenda == 'C')
                    {
                        pedidosCancelados.Add(pedido);
                    }
                }
                var contasMes = await contasRepository.GetContasDoMes();
                var custoEstoque = await produtoRepository.GetCustoTotalEstoque();

                foreach (var pedidoFinalizado in pedidosFinalizados)
                {
                    totalFinalizados += pedidoFinalizado.TotalVenda;
                }
                var indicadorFinalizados = new Indicador()
                {
                    DescricaoIndicador = "Vendas Finalizadas",
                    ValorIndicador = totalFinalizados
                };
                result.Add(indicadorFinalizados);
                foreach (var pedidoCancelado in pedidosCancelados)
                {
                    totalCancelados += pedidoCancelado.TotalVenda;
                }
                var indicadorCancelados = new Indicador()
                {
                    DescricaoIndicador = "Vendas Canceladas",
                    ValorIndicador = totalCancelados
                };
                result.Add(indicadorCancelados);
                foreach (var contaFixa in contasMes)
                {
                    totalContas += contaFixa.ValorConta;
                }
                var indicadorContas = new Indicador()
                {
                    DescricaoIndicador = "Custo Contas",
                    ValorIndicador = totalContas / 30
                };
                result.Add(indicadorContas);
                foreach (var produto in custoEstoque)
                {
                    totalEstoque += (produto.ValorUnitarioCusto * produto.QtdeEstoque);
                }
                var custoEstoqueDia = totalEstoque / 30; // considerando que o estoque atual deverá ser vendido em 3 meses
                var indicadorEstoque = new Indicador()
                {
                    DescricaoIndicador = "Custo Estoque",
                    ValorIndicador = custoEstoqueDia
                };
                result.Add(indicadorEstoque);
                var totalLucroDia = totalFinalizados - (indicadorContas.ValorIndicador + indicadorEstoque.ValorIndicador);
                var indicadorLucro = new Indicador()
                {
                    DescricaoIndicador = "Lucro",
                    ValorIndicador = totalLucroDia
                };
                result.Add(indicadorLucro);
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<Indicador>> GetIndicadoresMes()
        {
            var result = new List<Indicador>();
            var pedidosFinalizados = new List<Pedido>();
            var pedidosCancelados = new List<Pedido>();
            double totalFinalizados = 0.0;
            double totalCancelados = 0.0;
            double totalContas = 0.0;
            double totalEstoque = 0.0;
            try
            {
                var pedidos = await pedidoRepository.GetPedidosMes();
                foreach (var pedido in pedidos)
                {
                    if (pedido.StatusVenda == 'F')
                    {
                        pedidosFinalizados.Add(pedido);
                    }
                    else if (pedido.StatusVenda == 'C')
                    {
                        pedidosCancelados.Add(pedido);
                    }
                }
                var contasMes = await contasRepository.GetContasDoMes();
                var custoEstoque = await produtoRepository.GetCustoTotalEstoque();

                foreach (var pedidoFinalizado in pedidosFinalizados)
                {
                    totalFinalizados += pedidoFinalizado.TotalVenda;
                }
                var indicadorFinalizados = new Indicador()
                {
                    DescricaoIndicador = "Vendas Finalizadas",
                    ValorIndicador = totalFinalizados
                };
                result.Add(indicadorFinalizados);
                foreach (var pedidoCancelado in pedidosCancelados)
                {
                    totalCancelados += pedidoCancelado.TotalVenda;
                }
                var indicadorCancelados = new Indicador()
                {
                    DescricaoIndicador = "Vendas Canceladas",
                    ValorIndicador = totalCancelados
                };
                result.Add(indicadorCancelados);
                foreach (var contaFixa in contasMes)
                {
                    totalContas += contaFixa.ValorConta;
                }
                var indicadorContas = new Indicador()
                {
                    DescricaoIndicador = "Custo Contas",
                    ValorIndicador = totalContas
                };
                result.Add(indicadorContas);
                foreach (var produto in custoEstoque)
                {
                    totalEstoque += (produto.ValorUnitarioCusto * produto.QtdeEstoque);
                }
                var custoEstoqueMes = (totalEstoque / 3);
                var indicadorCompras = new Indicador()
                {
                    DescricaoIndicador = "Custo Estoque",
                    ValorIndicador = custoEstoqueMes
                };
                result.Add(indicadorCompras);
                var totalLucroMes = totalFinalizados - (totalContas + custoEstoqueMes);
                var indicadorLucro = new Indicador()
                {
                    DescricaoIndicador = "Lucro",
                    ValorIndicador = totalLucroMes
                };
                result.Add(indicadorLucro);
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
