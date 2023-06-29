using APISolPets.Domain.Extensions;
using APISolPets.Domain.Interfaces;
using APISolPets.Domain.Models;
using ApiSolPetsDDD.Domain.Interfaces;
using ApiSolPetsDDD.Domain.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace ApiSolPetsDDD.Infra.Data.Repository
{
    public class PedidoRepository : IPedidoRepository
    {
        private readonly IDbConFactory DbConFactory;
        private readonly ISqlComWrapperFac ComWrapperFac;
        private readonly IProdutoRepository produtoRepository;

        public PedidoRepository(IDbConFactory dbConnectionFactory, ISqlComWrapperFac commandWrapperFactory, IProdutoRepository produtoRepository)
        {
            DbConFactory = dbConnectionFactory;
            ComWrapperFac = commandWrapperFactory;
            this.produtoRepository = produtoRepository;
        }

        public async Task<Pedido> GetPedidosById(int idPedido)
        {
            var result = new Pedido();
            try
            {
                using (var connection = DbConFactory.CreateConnection())
                {
                    using var command = ComWrapperFac.CreateCommand("sp_BUSCAR_INFO_PEDIDO_PRODUTO");
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@ID_PEDIDO", idPedido);
                    command.Connection = connection;
                    string commandString = command.SqlCommandToString();
                    connection.Open();

                    var reader = await command.ExecuteReaderAsync();
                    await reader.ReadAsync();
                    if (reader.HasRows)
                    {
                        result.Cliente = new();
                        result.Cliente.IdCliente = reader["ID_CLIENTE"] == DBNull.Value ? 0 : Convert.ToInt32(reader["ID_CLIENTE"]);
                        result.Cliente.NomeCliente = reader["NOME_CLIENTE"].ToString() ?? null;
                        result.Cliente.NomeEmpresaCliente = reader["NOME_EMPRESA_CLIENTE"].ToString() ?? null;
                        result.Cliente.CpfCliente = reader["CPF_CLIENTE"].ToString() ?? null;
                        result.Cliente.CnpjCliente = reader["CNPJ_CLIENTE"].ToString() ?? null;
                        result.Finalizado = reader["PEDIDO_FINALIZADO"] != DBNull.Value && Convert.ToBoolean(reader["PEDIDO_FINALIZADO"]);
                        result.IdPedido = reader["ID_PEDIDO"] == DBNull.Value ? 0 : Convert.ToInt32(reader["ID_PEDIDO"]);
                        result.QtdeProdutos = reader["QUANTIDADE"] == DBNull.Value ? 0 : Convert.ToInt32(reader["QUANTIDADE"]);
                        result.Entregar = reader["ENTREGAR"] != DBNull.Value && Convert.ToBoolean(reader["ID_PEDIDO"]);
                        result.StatusVenda = reader["STATUS_VENDA"] == DBNull.Value ? '\0' : Convert.ToChar(reader["STATUS_VENDA"]);
                        result.TotalVenda = reader["TOTAL_VENDA"] == DBNull.Value ? 0.0 : Convert.ToDouble(reader["TOTAL_VENDA"]);
                        result.DhVenda = reader["DATA_VENDA"] == DBNull.Value ? new DateTime() : Convert.ToDateTime(reader["DATA_VENDA"]);
                        result.ValorDesconto = reader["VLR_DESCONTO_PEDIDO"] == DBNull.Value ? 0.0 : Convert.ToDouble(reader["VLR_DESCONTO_PEDIDO"]);
                        result.Pedidos = await GetProdutosPedido(idPedido);
                    }

                    return result;
                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private async Task<List<PedidoProduto>> GetProdutosPedido(int idPedido)
        {
            var result = new List<PedidoProduto>();
            try
            {
                using (var connection = DbConFactory.CreateConnection())
                {
                    using var command = ComWrapperFac.CreateCommand("sp_BUSCAR_PRODUTOS_PEDIDO");
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@ID_PEDIDO", idPedido);
                    command.Connection = connection;
                    string commandString = command.SqlCommandToString();
                    connection.Open();

                    var reader = await command.ExecuteReaderAsync();
                    while (await reader.ReadAsync())
                    {
                        if (reader.HasRows)
                        {
                            var fornecedor = new Fornecedor()
                            {
                                IdFornecedor = reader["ID_FORNECEDOR"] == DBNull.Value ? 0 : Convert.ToInt32(reader["ID_FORNECEDOR"]),
                                NomeFornecedor = reader["NOME_FORNECEDOR"].ToString() ?? null,
                                CnpjFornecedor = reader["CNPJ_FORNECEDOR"].ToString() ?? null
                            };
                            var produto = new Produto()
                            {
                                IdProduto = reader["ID_PRODUTO"] == DBNull.Value ? 0 : Convert.ToInt32(reader["ID_PRODUTO"]),
                                IdCategoria = reader["ID_CATEGORIA"] == DBNull.Value ? 0 : Convert.ToInt32(reader["ID_CATEGORIA"]),
                                NomeProduto = reader["NOME_PRODUTO"].ToString() ?? null,
                                IsbnProduto = reader["ISBN_PRODUTO"].ToString() ?? null,
                                MarcaProduto = reader["MARCA_PRODUTO"].ToString() ?? null,
                                ValorUnitarioCusto = reader["VALOR_UNITARIO_CUSTO"] == DBNull.Value ? 0.0 : Convert.ToDouble(reader["VALOR_UNITARIO_CUSTO"]),
                                ValorUnitarioVenda = reader["VALOR_UNITARIO_VENDA"] == DBNull.Value ? 0.0 : Convert.ToDouble(reader["VALOR_UNITARIO_VENDA"]),
                                PesoProduto = reader["PESO_KG"] == DBNull.Value ? 0.0 : Convert.ToDouble(reader["PESO_KG"]),
                                QtdeEstoque = reader["QUANTIDADE_PRODUTO"] == DBNull.Value ? 0 : Convert.ToInt32(reader["QUANTIDADE_PRODUTO"]),
                                DescricaoCategoria = reader["DESCRICAO_CATEGORIA"].ToString() ?? null,
                                TipoAnimal = reader["TIPO_ANIMAL_APLICAVEL"].ToString() ?? null,
                                TipoCategoria = reader["CATEGORIA_PRODUTO"].ToString() ?? null,
                                DhUltimaAtualizacao = reader["DATA_HORA_ULT_ATUALIZACAO"] == DBNull.Value ? new DateTime() : Convert.ToDateTime(reader["DATA_HORA_ULT_ATUALIZACAO"]),
                                DataValidade = reader["DATA_VALIDADE"] == DBNull.Value ? new DateTime() : Convert.ToDateTime(reader["DATA_VALIDADE"]),
                                Fornecedor = fornecedor,
                                ValorProdutoPedido = reader["VLR_PRODUTO_PEDIDO"] == DBNull.Value ? 0.0 : Convert.ToDouble(reader["VLR_PRODUTO_PEDIDO"])
                            };
                            var pediProduto = new PedidoProduto()
                            {
                                Finalizado = reader["PEDIDO_FINALIZADO"] != DBNull.Value && Convert.ToBoolean(reader["PEDIDO_FINALIZADO"]),
                                Produto = produto,
                                QtdeProdutos = reader["QUANTIDADE"] == DBNull.Value ? 0 : Convert.ToInt32(reader["QUANTIDADE"])
                            };
                            result.Add(pediProduto);
                        }
                    }

                    return result;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<Pedido>> GetPedidosByIdCliente(int idCliente)
        {
            var result = new List<Pedido>();
            try
            {
                using (var connection = DbConFactory.CreateConnection())
                {
                    using var command = ComWrapperFac.CreateCommand("sp_BUSCAR_INFO_PEDIDOS_CLIENTE");
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@ID_CLIENTE", idCliente);
                    command.Connection = connection;
                    string commandString = command.SqlCommandToString();
                    connection.Open();

                    var reader = await command.ExecuteReaderAsync();
                    while (await reader.ReadAsync())
                    {
                        if (reader.HasRows)
                        {
                            var idPedido = reader["ID_PEDIDO"] == DBNull.Value ? 0 : Convert.ToInt32(reader["ID_PEDIDO"]);
                            var pedidoProdutos = idPedido > 0 ? await GetProdutosPedido(idPedido) : new List<PedidoProduto>();
                            var cliente = new Cliente()
                            {
                                IdCliente = reader["ID_CLIENTE"] == DBNull.Value ? 0 : Convert.ToInt32(reader["ID_CLIENTE"]),
                                NomeCliente = reader["NOME_CLIENTE"].ToString() ?? null,
                                NomeEmpresaCliente = reader["NOME_EMPRESA_CLIENTE"].ToString() ?? null,
                                CpfCliente = reader["CPF_CLIENTE"].ToString() ?? null,
                                CnpjCliente = reader["CNPJ_CLIENTE"].ToString() ?? null
                            };
                            var pedido = new Pedido()
                            {
                                Finalizado = reader["PEDIDO_FINALIZADO"] != DBNull.Value && Convert.ToBoolean(reader["PEDIDO_FINALIZADO"]),
                                IdPedido = reader["ID_PEDIDO"] == DBNull.Value ? 0 : Convert.ToInt32(reader["ID_PEDIDO"]),
                                QtdeProdutos = reader["QUANTIDADE"] == DBNull.Value ? 0 : Convert.ToInt32(reader["QUANTIDADE"]),
                                Entregar = reader["ENTREGAR"] != DBNull.Value && Convert.ToBoolean(reader["ID_PEDIDO"]),
                                StatusVenda = reader["STATUS_VENDA"] == DBNull.Value ? '\0' : Convert.ToChar(reader["STATUS_VENDA"]),
                                TotalVenda = reader["TOTAL_VENDA"] == DBNull.Value ? 0.0 : Convert.ToDouble(reader["TOTAL_VENDA"]),
                                DhVenda = reader["DATA_VENDA"] == DBNull.Value ? new DateTime() : Convert.ToDateTime(reader["DATA_VENDA"]),
                                Cliente = cliente,
                                Pedidos = pedidoProdutos
                            };

                            result.Add(pedido);
                        }
                    }

                    return result;
                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<Pedido>> GetPedidosDia()
        {
            var result = new List<Pedido>();
            try
            {
                using (var connection = DbConFactory.CreateConnection())
                {
                    using var command = ComWrapperFac.CreateCommand("sp_BUSCAR_INFO_PEDIDOS_DIA_ATUAL");
                    command.CommandType = CommandType.StoredProcedure;
                    command.Connection = connection;
                    string commandString = command.SqlCommandToString();
                    connection.Open();

                    var reader = await command.ExecuteReaderAsync();
                    while (await reader.ReadAsync())
                    {
                        if (reader.HasRows)
                        {
                            var idPedido = reader["ID_PEDIDO"] == DBNull.Value ? 0 : Convert.ToInt32(reader["ID_PEDIDO"]);
                            var pedidoProdutos = idPedido > 0 ? await GetProdutosPedido(idPedido) : new List<PedidoProduto>();
                            var cliente = new Cliente()
                            {
                                IdCliente = reader["ID_CLIENTE"] == DBNull.Value ? 0 : Convert.ToInt32(reader["ID_CLIENTE"]),
                                NomeCliente = reader["NOME_CLIENTE"].ToString() ?? null,
                                NomeEmpresaCliente = reader["NOME_EMPRESA_CLIENTE"].ToString() ?? null,
                                CpfCliente = reader["CPF_CLIENTE"].ToString() ?? null,
                                CnpjCliente = reader["CNPJ_CLIENTE"].ToString() ?? null
                            };
                            var pedido = new Pedido()
                            {
                                Finalizado = reader["PEDIDO_FINALIZADO"] != DBNull.Value && Convert.ToBoolean(reader["PEDIDO_FINALIZADO"]),
                                IdPedido = reader["ID_PEDIDO"] == DBNull.Value ? 0 : Convert.ToInt32(reader["ID_PEDIDO"]),
                                QtdeProdutos = reader["QUANTIDADE"] == DBNull.Value ? 0 : Convert.ToInt32(reader["QUANTIDADE"]),
                                Entregar = reader["ENTREGAR"] != DBNull.Value && Convert.ToBoolean(reader["ID_PEDIDO"]),
                                StatusVenda = reader["STATUS_VENDA"] == DBNull.Value ? '\0' : Convert.ToChar(reader["STATUS_VENDA"]),
                                TotalVenda = reader["TOTAL_VENDA"] == DBNull.Value ? 0.0 : Convert.ToDouble(reader["TOTAL_VENDA"]),
                                DhVenda = reader["DATA_VENDA"] == DBNull.Value ? new DateTime() : Convert.ToDateTime(reader["DATA_VENDA"]),
                                Cliente = cliente,
                                Pedidos = pedidoProdutos
                            };

                            result.Add(pedido);
                        }
                    }

                    return result;
                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<Pedido>> GetPedidosMes()
        {
            var result = new List<Pedido>();
            try
            {
                using (var connection = DbConFactory.CreateConnection())
                {
                    using var command = ComWrapperFac.CreateCommand("sp_BUSCAR_INFO_PEDIDOS_MES_ATUAL");
                    command.CommandType = CommandType.StoredProcedure;
                    command.Connection = connection;
                    string commandString = command.SqlCommandToString();
                    connection.Open();

                    var reader = await command.ExecuteReaderAsync();
                    while (await reader.ReadAsync())
                    {
                        if (reader.HasRows)
                        {
                            var idPedido = reader["ID_PEDIDO"] == DBNull.Value ? 0 : Convert.ToInt32(reader["ID_PEDIDO"]);
                            var pedidoProdutos = idPedido > 0 ? await GetProdutosPedido(idPedido) : new List<PedidoProduto>();
                            var cliente = new Cliente()
                            {
                                IdCliente = reader["ID_CLIENTE"] == DBNull.Value ? 0 : Convert.ToInt32(reader["ID_CLIENTE"]),
                                NomeCliente = reader["NOME_CLIENTE"].ToString() ?? null,
                                NomeEmpresaCliente = reader["NOME_EMPRESA_CLIENTE"].ToString() ?? null,
                                CpfCliente = reader["CPF_CLIENTE"].ToString() ?? null,
                                CnpjCliente = reader["CNPJ_CLIENTE"].ToString() ?? null
                            };
                            var pedido = new Pedido()
                            {
                                Finalizado = reader["PEDIDO_FINALIZADO"] != DBNull.Value && Convert.ToBoolean(reader["PEDIDO_FINALIZADO"]),
                                IdPedido = reader["ID_PEDIDO"] == DBNull.Value ? 0 : Convert.ToInt32(reader["ID_PEDIDO"]),
                                QtdeProdutos = reader["QUANTIDADE"] == DBNull.Value ? 0 : Convert.ToInt32(reader["QUANTIDADE"]),
                                Entregar = reader["ENTREGAR"] != DBNull.Value && Convert.ToBoolean(reader["ID_PEDIDO"]),
                                StatusVenda = reader["STATUS_VENDA"] == DBNull.Value ? '\0' : Convert.ToChar(reader["STATUS_VENDA"]),
                                TotalVenda = reader["TOTAL_VENDA"] == DBNull.Value ? 0.0 : Convert.ToDouble(reader["TOTAL_VENDA"]),
                                DhVenda = reader["DATA_VENDA"] == DBNull.Value ? new DateTime() : Convert.ToDateTime(reader["DATA_VENDA"]),
                                Cliente = cliente,
                                Pedidos = pedidoProdutos
                            };

                            result.Add(pedido);
                        }
                    }

                    return result;
                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<Pedido>> GetPedidosAno()
        {
            var result = new List<Pedido>();
            try
            {
                using (var connection = DbConFactory.CreateConnection())
                {
                    using var command = ComWrapperFac.CreateCommand("sp_BUSCAR_INFO_PEDIDOS_ANO_ATUAL");
                    command.CommandType = CommandType.StoredProcedure;
                    command.Connection = connection;
                    string commandString = command.SqlCommandToString();
                    connection.Open();

                    var reader = await command.ExecuteReaderAsync();
                    while (await reader.ReadAsync())
                    {
                        if (reader.HasRows)
                        {
                            var idPedido = reader["ID_PEDIDO"] == DBNull.Value ? 0 : Convert.ToInt32(reader["ID_PEDIDO"]);
                            var pedidoProdutos = idPedido > 0 ? await GetProdutosPedido(idPedido) : new List<PedidoProduto>();
                            var cliente = new Cliente()
                            {
                                IdCliente = reader["ID_CLIENTE"] == DBNull.Value ? 0 : Convert.ToInt32(reader["ID_CLIENTE"]),
                                NomeCliente = reader["NOME_CLIENTE"].ToString() ?? null,
                                NomeEmpresaCliente = reader["NOME_EMPRESA_CLIENTE"].ToString() ?? null,
                                CpfCliente = reader["CPF_CLIENTE"].ToString() ?? null,
                                CnpjCliente = reader["CNPJ_CLIENTE"].ToString() ?? null
                            };
                            var pedido = new Pedido()
                            {
                                Finalizado = reader["PEDIDO_FINALIZADO"] != DBNull.Value && Convert.ToBoolean(reader["PEDIDO_FINALIZADO"]),
                                IdPedido = reader["ID_PEDIDO"] == DBNull.Value ? 0 : Convert.ToInt32(reader["ID_PEDIDO"]),
                                QtdeProdutos = reader["QUANTIDADE"] == DBNull.Value ? 0 : Convert.ToInt32(reader["QUANTIDADE"]),
                                Entregar = reader["ENTREGAR"] != DBNull.Value && Convert.ToBoolean(reader["ID_PEDIDO"]),
                                StatusVenda = reader["STATUS_VENDA"] == DBNull.Value ? '\0' : Convert.ToChar(reader["STATUS_VENDA"]),
                                TotalVenda = reader["TOTAL_VENDA"] == DBNull.Value ? 0.0 : Convert.ToDouble(reader["TOTAL_VENDA"]),
                                DhVenda = reader["DATA_VENDA"] == DBNull.Value ? new DateTime() : Convert.ToDateTime(reader["DATA_VENDA"]),
                                Cliente = cliente,
                                Pedidos = pedidoProdutos
                            };

                            result.Add(pedido);
                        }
                    }

                    return result;
                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Pedido> CadastrarPedido(Pedido pedido)
        {
            var result = new Pedido();
            try
            {
                using (var connection = DbConFactory.CreateConnection())
                {
                    using var command = ComWrapperFac.CreateCommand("sp_CADASTRAR_INFO_PEDIDO");
                    command.CommandType = CommandType.StoredProcedure;
                    command.Connection = connection;

                    var dhVenda = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                    command.Parameters.AddWithValue("@DATA_VENDA", DateTime.Parse(dhVenda));
                    command.Parameters.AddWithValue("@ENTREGAR", pedido.Entregar);
                    command.Parameters.AddWithValue("@ID_CLIENTE", pedido.Cliente?.IdCliente);
                    command.Parameters.AddWithValue("@STATUS_VENDA", pedido.StatusVenda);
                    command.Parameters.AddWithValue("@TOTAL_VENDA", pedido.TotalVenda);
                    command.Parameters.AddWithValue("@VLR_DESCONTO_PEDIDO", pedido.ValorDesconto > 0.0 ?
                        pedido.ValorDesconto : DBNull.Value);
                    string commandString = command.SqlCommandToString();
                    connection.Open();

                    var idNovoPedido = Convert.ToInt32(await command.ExecuteScalarAsync());

                    pedido.IdPedido = idNovoPedido;

                    foreach (Produto produto in pedido.Produtos)
                    {
                        result = await CadastrarPedidoProduto(pedido, produto);
                    }

                    return result;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private async Task<Pedido> CadastrarPedidoProduto(Pedido pedido, Produto produto)
        {
            try
            {
                using (var connection = DbConFactory.CreateConnection())
                {
                    using var command = ComWrapperFac.CreateCommand("sp_CADASTRAR_INFO_PEDIDO_PRODUTO");
                    command.CommandType = CommandType.StoredProcedure;
                    command.Connection = connection;

                    command.Parameters.AddWithValue("@ID_PEDIDO", pedido.IdPedido);
                    command.Parameters.AddWithValue("@ID_PRODUTO", produto.IdProduto);
                    command.Parameters.AddWithValue("@QUANTIDADE", (int)produto.QtdeProduto);
                    command.Parameters.AddWithValue("@PEDIDO_FINALIZADO", pedido.Finalizado);
                    command.Parameters.AddWithValue("@VLR_PRODUTO_PEDIDO", produto.ValorProdutoPedido > 0.0 ?
                    produto.ValorProdutoPedido : DBNull.Value);
                    string commandString = command.SqlCommandToString();
                    connection.Open();
                    await command.ExecuteScalarAsync();
                    await produtoRepository.AtualizarQtdeProduto((int)produto.QtdeProduto, produto.IdProduto, false);
                    var result = await GetPedidosById(pedido.IdPedido);

                    return result;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Pedido> FinalizarPedido(char statusVenda, int idPedido, bool finalizado)
        {
            var result = new Pedido();
            try
            {
                using (var connection = DbConFactory.CreateConnection())
                {
                    using var command = ComWrapperFac.CreateCommand("sp_EDITAR_FINALIZACAO_INFO_PEDIDO");
                    command.CommandType = CommandType.StoredProcedure;
                    command.Connection = connection;
                    command.Parameters.AddWithValue("@STATUS_VENDA", statusVenda);
                    command.Parameters.AddWithValue("@ID_PEDIDO", idPedido);
                    string commandString = command.SqlCommandToString();
                    connection.Open();

                    var linhasAfetadas = Convert.ToInt32(await command.ExecuteNonQueryAsync());

                    if (linhasAfetadas > 0)
                        result = await FinalizarPedidoProduto(idPedido, finalizado);

                    return result;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<int> CancelarPedido(char statusVenda, int idPedido, bool finalizado)
        {
            int estoqueAtualizado = 0;
            try
            {
                using (var connection = DbConFactory.CreateConnection())
                {
                    using var command = ComWrapperFac.CreateCommand("sp_EDITAR_FINALIZACAO_INFO_PEDIDO");
                    command.CommandType = CommandType.StoredProcedure;
                    command.Connection = connection;
                    command.Parameters.AddWithValue("@STATUS_VENDA", statusVenda);
                    command.Parameters.AddWithValue("@ID_PEDIDO", idPedido);
                    string commandString = command.SqlCommandToString();
                    connection.Open();

                    var linhasAfetadas = Convert.ToInt32(await command.ExecuteNonQueryAsync());

                    if (linhasAfetadas > 0)
                    {
                        var pedido = await GetPedidosById(idPedido);
                        foreach (var pedidoProd in pedido.Pedidos)
                        {
                            estoqueAtualizado += await produtoRepository.AtualizarQtdeProduto(pedidoProd.QtdeProdutos, pedidoProd.Produto.IdProduto, true);
                        }
                    }

                    return estoqueAtualizado;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Pedido> VincularCliente(int idPedido, int idCliente)
        {
            var result = new Pedido();
            try
            {
                using (var connection = DbConFactory.CreateConnection())
                {
                    string query = @"UPDATE PEDIDO
                                    SET ID_CLIENTE=@ID_CLIENTE
                                    WHERE ID_PEDIDO=@ID_PEDIDO";
                    using var command = ComWrapperFac.CreateCommand(query);
                    command.CommandType = CommandType.Text;
                    command.Connection = connection;
                    command.Parameters.AddWithValue("@ID_CLIENTE", idCliente);
                    command.Parameters.AddWithValue("@ID_PEDIDO", idPedido);
                    string commandString = command.SqlCommandToString();
                    connection.Open();

                    var linhasAfetadas = Convert.ToInt32(await command.ExecuteNonQueryAsync());

                    if (linhasAfetadas > 0)
                        result = await GetPedidosById(idPedido);

                    return result;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Pedido> RemoveProdutoPedido(int idPedido, int idProduto, int qtdeProduto, double totalVenda)
        {
            var result = new Pedido();
            try
            {
                using (var connection = DbConFactory.CreateConnection())
                {
                    var atualizaEstoqueProd = await produtoRepository.AtualizarQtdeProduto(qtdeProduto, idProduto, true);

                    string query = @"DELETE PEDIDO_PRODUTO
                                    WHERE ID_PEDIDO=@ID_PEDIDO AND ID_PRODUTO=@ID_PRODUTO";
                    using var command = ComWrapperFac.CreateCommand(query);
                    command.CommandType = CommandType.Text;
                    command.Connection = connection;
                    command.Parameters.AddWithValue("@ID_PRODUTO", idProduto);
                    command.Parameters.AddWithValue("@ID_PEDIDO", idPedido);
                    string commandString = command.SqlCommandToString();
                    connection.Open();

                    var linhasAfetadas = Convert.ToInt32(await command.ExecuteNonQueryAsync());

                    if (linhasAfetadas > 0)
                    {
                        await AtualizaValorDescontoTotalVenda(idPedido, 0.0, totalVenda);
                        result = await GetPedidosById(idPedido);
                    }

                    return result;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Pedido> AtualizaQtdeProdutoPedido(int idPedido, int idProduto, int qtdeProduto, double totalVenda)
        {
            var result = new Pedido();
            try
            {
                using (var connection = DbConFactory.CreateConnection())
                {
                    var pedido = await GetPedidosById(idPedido);
                    var produtoSelected = pedido.Pedidos.Find(_ => _.Produto.IdProduto == idProduto);
                    await produtoRepository.AtualizarQtdeProduto(qtdeProduto - produtoSelected.QtdeProdutos, idProduto, produtoSelected.QtdeProdutos > qtdeProduto);

                    string query = @"UPDATE PEDIDO_PRODUTO
                                    SET QUANTIDADE=@QTDE_PRODUTO
                                    WHERE ID_PEDIDO=@ID_PEDIDO AND ID_PRODUTO=@ID_PRODUTO";
                    using var command = ComWrapperFac.CreateCommand(query);
                    command.CommandType = CommandType.Text;
                    command.Connection = connection;
                    command.Parameters.AddWithValue("@ID_PRODUTO", idProduto);
                    command.Parameters.AddWithValue("@ID_PEDIDO", idPedido);
                    command.Parameters.AddWithValue("@QTDE_PRODUTO", qtdeProduto);
                    string commandString = command.SqlCommandToString();
                    connection.Open();

                    var linhasAfetadas = Convert.ToInt32(await command.ExecuteNonQueryAsync());

                    if (linhasAfetadas > 0)
                    {
                        await AtualizaValorDescontoTotalVenda(idPedido, 0.0, totalVenda);
                        result = await GetPedidosById(idPedido);
                    }

                    return result;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Pedido> AdicionarProdutoPedido(PedidoProduto pedidoProduto)
        {
            var result = new Pedido();
            try
            {
                using (var connection = DbConFactory.CreateConnection())
                {
                    await produtoRepository.AtualizarQtdeProduto(pedidoProduto.QtdeProdutos, pedidoProduto.Produto.IdProduto, false);

                    string query = @"INSERT INTO PEDIDO_PRODUTO (ID_PEDIDO, ID_PRODUTO, QUANTIDADE, 
                                    PEDIDO_FINALIZADO, VLR_PRODUTO_PEDIDO)
                                    VALUES(@ID_PEDIDO, @ID_PRODUTO, @QTDE_PRODUTO, @PEDIDO_FINALIZADO,
                                    @VLR_PRODUTO_PEDIDO)";
                    using var command = ComWrapperFac.CreateCommand(query);
                    command.CommandType = CommandType.Text;
                    command.Connection = connection;
                    command.Parameters.AddWithValue("@ID_PRODUTO", pedidoProduto.Produto.IdProduto);
                    command.Parameters.AddWithValue("@ID_PEDIDO", (int)pedidoProduto.IdPedido);
                    command.Parameters.AddWithValue("@QTDE_PRODUTO", pedidoProduto.QtdeProdutos);
                    command.Parameters.AddWithValue("@PEDIDO_FINALIZADO", false);
                    command.Parameters.AddWithValue("@VLR_PRODUTO_PEDIDO", pedidoProduto.Produto.ValorProdutoPedido);
                    string commandString = command.SqlCommandToString();
                    connection.Open();

                    var linhasAfetadas = Convert.ToInt32(await command.ExecuteNonQueryAsync());

                    if (linhasAfetadas > 0)
                    {
                        await AtualizaValorDescontoTotalVenda((int)pedidoProduto.IdPedido, 0.0, (double)pedidoProduto.TotalVenda);
                        result = await GetPedidosById((int)pedidoProduto.IdPedido);
                    }

                    return result;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Pedido> AtualizaValorDescontoTotalVenda(int idPedido, double valorDesconto, double totalVenda)
        {
            try
            {
                using (var connection = DbConFactory.CreateConnection())
                {
                    string query = @"IF(@VLR_DESCONTO_PEDIDO > 0.0)
									UPDATE PEDIDO
                                        SET 
                                        VLR_DESCONTO_PEDIDO=@VLR_DESCONTO_PEDIDO, 
                                        TOTAL_VENDA=@TOTAL_VENDA
                                    WHERE ID_PEDIDO=@ID_PEDIDO
                                    ELSE
									UPDATE PEDIDO
                                        SET
                                        TOTAL_VENDA=@TOTAL_VENDA
                                    WHERE ID_PEDIDO=@ID_PEDIDO";
                    using var command = ComWrapperFac.CreateCommand(query);
                    command.CommandType = CommandType.Text;
                    command.Connection = connection;
                    command.Parameters.AddWithValue("@VLR_DESCONTO_PEDIDO", valorDesconto > 0.0 ? 
                        valorDesconto : DBNull.Value);
                    command.Parameters.AddWithValue("@TOTAL_VENDA", totalVenda);
                    command.Parameters.AddWithValue("@ID_PEDIDO", idPedido);
                    string commandString = command.SqlCommandToString();
                    connection.Open();
                    await command.ExecuteNonQueryAsync();
                    return await GetPedidosById(idPedido);              
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Pedido> AtualizaValorProdutoPedido(int idPedido, int idProduto, double totalVenda, double valorProduto)
        {
            try
            {
                using (var connection = DbConFactory.CreateConnection())
                {
                    string query = @"UPDATE PEDIDO_PRODUTO
                                      SET 
                                      VLR_PRODUTO_PEDIDO=@VLR_PRODUTO_PEDIDO, 
                                      TOTAL_VENDA=@TOTAL_VENDA
                                    WHERE ID_PEDIDO=@ID_PEDIDO AND ID_PRODUTO=@ID_PRODUTO";
                    using var command = ComWrapperFac.CreateCommand(query);
                    command.CommandType = CommandType.Text;
                    command.Connection = connection;
                    command.Parameters.AddWithValue("@VLR_PRODUTO_PEDIDO", valorProduto);
                    command.Parameters.AddWithValue("@TOTAL_VENDA", totalVenda);
                    command.Parameters.AddWithValue("@ID_PEDIDO", idPedido);
                    command.Parameters.AddWithValue("@ID_PRODUTO", idProduto);
                    string commandString = command.SqlCommandToString();
                    connection.Open();
                    await command.ExecuteNonQueryAsync();
                    return await GetPedidosById(idPedido);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private async Task<Pedido> FinalizarPedidoProduto(int idPedido, bool finalizado)
        {
            var result = new Pedido();
            try
            {
                using (var connection = DbConFactory.CreateConnection())
                {
                    using var command = ComWrapperFac.CreateCommand("sp_EDITAR_FINALIZACAO_INFO_PEDIDO_PRODUTO");
                    command.CommandType = CommandType.StoredProcedure;
                    command.Connection = connection;
                    command.Parameters.AddWithValue("@ID_PEDIDO", idPedido);
                    command.Parameters.AddWithValue("@PEDIDO_FINALIZADO", finalizado);
                    string commandString = command.SqlCommandToString();
                    connection.Open();

                    var linhasAfetadas = Convert.ToInt32(await command.ExecuteNonQueryAsync());

                    if (linhasAfetadas > 0)
                        result = await GetPedidosById(idPedido);

                    return result;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<int> AtualizarPedido(Pedido pedido, bool cancelamento = false)
        {
            var result = 0;
            try
            {
                using (var connection = DbConFactory.CreateConnection())
                {
                    using var command = ComWrapperFac.CreateCommand("sp_EDITAR_INFO_PEDIDO");
                    command.CommandType = CommandType.StoredProcedure;
                    command.Connection = connection;
                    command.Parameters.AddWithValue("@STATUS_VENDA", pedido.StatusVenda);
                    command.Parameters.AddWithValue("@ID_PEDIDO", pedido.IdPedido);
                    command.Parameters.AddWithValue("@ENTREGAR", pedido.Entregar);
                    command.Parameters.AddWithValue("@ID_CLIENTE", pedido.Cliente.IdCliente);
                    command.Parameters.AddWithValue("@TOTAL_VENDA", pedido.TotalVenda);
                    command.Parameters.AddWithValue("@DATA_VENDA", pedido.DhVenda);
                    command.Parameters.AddWithValue("@VLR_DESCONTO_PEDIDO", pedido.ValorDesconto > 0.0 ?
                        pedido.ValorDesconto : DBNull.Value);
                    string commandString = command.SqlCommandToString();
                    connection.Open();

                    var linhasAfetadas = Convert.ToInt32(await command.ExecuteNonQueryAsync());

                    if (linhasAfetadas > 0)
                    {
                        foreach (Produto produto in pedido.Produtos)
                        {
                            result += await AtualizarPedidoProduto(pedido, produto.IdProduto, produto.ValorProdutoPedido);
                        }
                    }

                    return result;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private async Task<int> AtualizarPedidoProduto(Pedido pedido, int idProduto, double valorProdutoPedido)
        {
            try
            {
                using (var connection = DbConFactory.CreateConnection())
                {
                    using var command = ComWrapperFac.CreateCommand("sp_EDITAR_INFO_PEDIDO_PRODUTO");
                    command.CommandType = CommandType.StoredProcedure;
                    command.Connection = connection;
                    command.Parameters.AddWithValue("@ID_PRODUTO", idProduto);
                    command.Parameters.AddWithValue("@ID_PEDIDO", pedido.IdPedido);
                    command.Parameters.AddWithValue("@QUANTIDADE", pedido.QtdeProdutos);
                    command.Parameters.AddWithValue("@PEDIDO_FINALIZADO", pedido.Finalizado);
                    if(valorProdutoPedido > 0.0)
                        command.Parameters.AddWithValue("@VLR_PRODUTO_PEDIDO", valorProdutoPedido);
                    string commandString = command.SqlCommandToString();
                    connection.Open();

                    var result = Convert.ToInt32(await command.ExecuteNonQueryAsync());

                    if (result > 0)
                    {
                        var produto = await produtoRepository.GetProdutoById(idProduto);
                        int qtdeEstoqueProd = produto.QtdeEstoque;

                        if (qtdeEstoqueProd <= pedido.QtdeProdutos)
                        {
                            int novoQtdeEstoque = qtdeEstoqueProd - pedido.QtdeProdutos;
                            var atualizaEstoque = await produtoRepository.AtualizarQtdeProduto(novoQtdeEstoque, idProduto, false);
                        }
                    }

                    return result;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<int> ExcluirPedido(int idPedido)
        {
            try
            {
                using (var connection = DbConFactory.CreateConnection())
                {
                    using var command = ComWrapperFac.CreateCommand("DELETE FROM PEDIDO_PRODUTO WHERE ID_PEDIDO=@ID_PEDIDO");
                    command.CommandType = CommandType.Text;
                    command.Parameters.AddWithValue("@ID_PEDIDO", idPedido);
                    command.Connection = connection;
                    string commandString = command.SqlCommandToString();
                    connection.Open();

                    var result = Convert.ToInt32(await command.ExecuteNonQueryAsync());
                    if (result > 0)
                        result += await ExcluirPedidoProduto(idPedido);
                    return result;
                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private async Task<int> ExcluirPedidoProduto(int idPedido)
        {
            try
            {
                using (var connection = DbConFactory.CreateConnection())
                {
                    using var command = ComWrapperFac.CreateCommand("DELETE FROM PEDIDO WHERE ID_PEDIDO=@ID_PEDIDO");
                    command.CommandType = CommandType.Text;
                    command.Parameters.AddWithValue("@ID_PEDIDO", idPedido);
                    command.Connection = connection;
                    string commandString = command.SqlCommandToString();
                    connection.Open();

                    var result = Convert.ToInt32(await command.ExecuteNonQueryAsync());

                    return result;
                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
