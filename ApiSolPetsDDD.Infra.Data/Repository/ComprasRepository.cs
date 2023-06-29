using APISolPets.Domain.Extensions;
using APISolPets.Domain.Interfaces;
using ApiSolPetsDDD.Domain.Interfaces;
using ApiSolPetsDDD.Domain.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace ApiSolPetsDDD.Infra.Data.Repository
{
    public class ComprasRepository : IComprasRepository
    {
        private readonly IDbConFactory DbConFactory;
        private readonly ISqlComWrapperFac ComWrapperFac;
        private readonly IProdutoRepository produtoRepository;

        public ComprasRepository(IDbConFactory dbConFactory, ISqlComWrapperFac comWrapperFac, IProdutoRepository produtoRepository)
        {
            DbConFactory = dbConFactory;
            ComWrapperFac = comWrapperFac;
            this.produtoRepository = produtoRepository;
        }

        public async Task<List<Compra>> GetInfoCompras(DateTime dataInicio, DateTime dataFim)
        {
            var result = new List<Compra>();
            try
            {
                using (var connection = DbConFactory.CreateConnection())
                {
                    using var command = ComWrapperFac.CreateCommand("sp_BUSCAR_INFO_COMPRAS_ABASTECIMENTO");
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@DATA_COMPRA_INICIO", dataInicio);
                    command.Parameters.AddWithValue("@DATA_COMPRA_FIM", dataFim);
                    command.Connection = connection;
                    string commandString = command.SqlCommandToString();
                    connection.Open();

                    var reader = await command.ExecuteReaderAsync();
                    while (await reader.ReadAsync())
                    {
                        if (reader.HasRows)
                        {
                            var funcionario = new Funcionario()
                            {
                                IdFuncionario = Convert.ToInt32(reader["ID_FUNCIONARIO"]),
                                NomeCompleto = reader["NOME_FUNCIONARIO"].ToString() ?? null,
                                Cpf = reader["CPF_FUNCIONARIO"].ToString() ?? null,
                            };
                            var fornecedor = new Fornecedor()
                            {
                                IdFornecedor = Convert.ToInt32(reader["ID_FORNECEDOR"]),
                                CnpjFornecedor = reader["CNPJ_FORNECEDOR"].ToString() ?? null,
                                NomeFornecedor = reader["NOME_FORNECEDOR"].ToString() ?? null
                            };
                            var compra = new Compra()
                            {
                                IdCompra = Convert.ToInt32(reader["ID_COMPRA"]),
                                Fornecedor = fornecedor,
                                DataCompra = reader["DATA_COMPRA"] == DBNull.Value ? new DateTime() : Convert.ToDateTime(reader["DATA_COMPRA"]),
                                ValorCompra = reader["VALOR_COMPRA"] == DBNull.Value ? 0.0 : Convert.ToDouble(reader["VALOR_COMPRA"]),
                                QuantidadeCompra = reader["QUANTIDADE"] == DBNull.Value ? 0 : Convert.ToInt32(reader["QUANTIDADE"]),
                                IdProduto = Convert.ToInt32(reader["ID_PRODUTO"]),
                                IsbnProduto = reader["CNPJ_FORNECEDOR"].ToString() ?? null,
                                IdCategoria = Convert.ToInt32(reader["ID_CATEGORIA"]),
                                TipoCategoria = reader["CATEGORIA_PRODUTO"].ToString() ?? null,
                                DescricaoCategoria = reader["DESCRICAO_CATEGORIA"].ToString() ?? null,
                                TipoAnimal = reader["TIPO_ANIMAL_APLICAVEL"].ToString() ?? null,
                                MarcaProduto = reader["MARCA_PRODUTO"].ToString() ?? null,
                                NomeProduto = reader["NOME_PRODUTO"].ToString() ?? null,
                                QtdeEstoque = reader["QUANTIDADE_PRODUTO"] == DBNull.Value ? 0 : Convert.ToInt32(reader["QUANTIDADE_PRODUTO"]),
                                Funcionario = funcionario
                            };
                            result.Add(compra);
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

        public async Task<List<Compra>> GetComprasMes()
        {
            var result = new List<Compra>();
            try
            {
                using (var connection = DbConFactory.CreateConnection())
                {
                    using var command = ComWrapperFac.CreateCommand("sp_BUSCAR_INFO_COMPRAS_MES_CORRENTE");
                    command.CommandType = CommandType.StoredProcedure;
                    command.Connection = connection;
                    string commandString = command.SqlCommandToString();
                    connection.Open();

                    var reader = await command.ExecuteReaderAsync();
                    while (await reader.ReadAsync())
                    {
                        if (reader.HasRows)
                        {
                            var funcionario = new Funcionario()
                            {
                                IdFuncionario = Convert.ToInt32(reader["ID_FUNCIONARIO"]),
                                NomeCompleto = reader["NOME_FUNCIONARIO"].ToString() ?? null,
                                Cpf = reader["CPF_FUNCIONARIO"].ToString() ?? null,
                            };
                            var fornecedor = new Fornecedor()
                            {
                                IdFornecedor = Convert.ToInt32(reader["ID_FORNECEDOR"]),
                                CnpjFornecedor = reader["CNPJ_FORNECEDOR"].ToString() ?? null,
                                NomeFornecedor = reader["NOME_FORNECEDOR"].ToString() ?? null
                            };
                            var compra = new Compra()
                            {
                                IdCompra = Convert.ToInt32(reader["ID_COMPRA"]),
                                Fornecedor = fornecedor,
                                DataCompra = reader["DATA_COMPRA"] == DBNull.Value ? new DateTime() : Convert.ToDateTime(reader["DATA_COMPRA"]),
                                ValorCompra = reader["VALOR_COMPRA"] == DBNull.Value ? 0.0 : Convert.ToDouble(reader["VALOR_COMPRA"]),
                                QuantidadeCompra = reader["QUANTIDADE"] == DBNull.Value ? 0 : Convert.ToInt32(reader["QUANTIDADE"]),
                                IdProduto = Convert.ToInt32(reader["ID_PRODUTO"]),
                                IsbnProduto = reader["CNPJ_FORNECEDOR"].ToString() ?? null,
                                IdCategoria = Convert.ToInt32(reader["ID_CATEGORIA"]),
                                TipoCategoria = reader["CATEGORIA_PRODUTO"].ToString() ?? null,
                                DescricaoCategoria = reader["DESCRICAO_CATEGORIA"].ToString() ?? null,
                                TipoAnimal = reader["TIPO_ANIMAL_APLICAVEL"].ToString() ?? null,
                                MarcaProduto = reader["MARCA_PRODUTO"].ToString() ?? null,
                                NomeProduto = reader["NOME_PRODUTO"].ToString() ?? null,
                                QtdeEstoque = reader["QUANTIDADE_PRODUTO"] == DBNull.Value ? 0 : Convert.ToInt32(reader["QUANTIDADE_PRODUTO"]),
                                Funcionario = funcionario
                            };
                            result.Add(compra);
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

        public async Task<List<Compra>> GetComprasDia()
        {
            var result = new List<Compra>();
            try
            {
                using (var connection = DbConFactory.CreateConnection())
                {
                    using var command = ComWrapperFac.CreateCommand("sp_BUSCAR_INFO_COMPRAS_DIA_CORRENTE");
                    command.CommandType = CommandType.StoredProcedure;
                    command.Connection = connection;
                    string commandString = command.SqlCommandToString();
                    connection.Open();

                    var reader = await command.ExecuteReaderAsync();
                    while (await reader.ReadAsync())
                    {
                        if (reader.HasRows)
                        {
                            var funcionario = new Funcionario()
                            {
                                IdFuncionario = Convert.ToInt32(reader["ID_FUNCIONARIO"]),
                                NomeCompleto = reader["NOME_FUNCIONARIO"].ToString() ?? null,
                                Cpf = reader["CPF_FUNCIONARIO"].ToString() ?? null,
                            };
                            var fornecedor = new Fornecedor()
                            {
                                IdFornecedor = Convert.ToInt32(reader["ID_FORNECEDOR"]),
                                CnpjFornecedor = reader["CNPJ_FORNECEDOR"].ToString() ?? null,
                                NomeFornecedor = reader["NOME_FORNECEDOR"].ToString() ?? null
                            };
                            var compra = new Compra()
                            {
                                IdCompra = Convert.ToInt32(reader["ID_COMPRA"]),
                                Fornecedor = fornecedor,
                                DataCompra = reader["DATA_COMPRA"] == DBNull.Value ? new DateTime() : Convert.ToDateTime(reader["DATA_COMPRA"]),
                                ValorCompra = reader["VALOR_COMPRA"] == DBNull.Value ? 0.0 : Convert.ToDouble(reader["VALOR_COMPRA"]),
                                QuantidadeCompra = reader["QUANTIDADE"] == DBNull.Value ? 0 : Convert.ToInt32(reader["QUANTIDADE"]),
                                IdProduto = Convert.ToInt32(reader["ID_PRODUTO"]),
                                IsbnProduto = reader["CNPJ_FORNECEDOR"].ToString() ?? null,
                                IdCategoria = Convert.ToInt32(reader["ID_CATEGORIA"]),
                                TipoCategoria = reader["CATEGORIA_PRODUTO"].ToString() ?? null,
                                DescricaoCategoria = reader["DESCRICAO_CATEGORIA"].ToString() ?? null,
                                TipoAnimal = reader["TIPO_ANIMAL_APLICAVEL"].ToString() ?? null,
                                MarcaProduto = reader["MARCA_PRODUTO"].ToString() ?? null,
                                NomeProduto = reader["NOME_PRODUTO"].ToString() ?? null,
                                QtdeEstoque = reader["QUANTIDADE_PRODUTO"] == DBNull.Value ? 0 : Convert.ToInt32(reader["QUANTIDADE_PRODUTO"]),
                                Funcionario = funcionario
                            };
                            result.Add(compra);
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

        public async Task<List<Compra>> GetComprasAno()
        {
            var result = new List<Compra>();
            try
            {
                using (var connection = DbConFactory.CreateConnection())
                {
                    using var command = ComWrapperFac.CreateCommand("sp_BUSCAR_INFO_COMPRAS_ANO_CORRETE");
                    command.CommandType = CommandType.StoredProcedure;
                    command.Connection = connection;
                    string commandString = command.SqlCommandToString();
                    connection.Open();

                    var reader = await command.ExecuteReaderAsync();
                    while (await reader.ReadAsync())
                    {
                        if (reader.HasRows)
                        {
                            var funcionario = new Funcionario()
                            {
                                IdFuncionario = Convert.ToInt32(reader["ID_FUNCIONARIO"]),
                                NomeCompleto = reader["NOME_FUNCIONARIO"].ToString() ?? null,
                                Cpf = reader["CPF_FUNCIONARIO"].ToString() ?? null,
                            };
                            var fornecedor = new Fornecedor()
                            {
                                IdFornecedor = Convert.ToInt32(reader["ID_FORNECEDOR"]),
                                CnpjFornecedor = reader["CNPJ_FORNECEDOR"].ToString() ?? null,
                                NomeFornecedor = reader["NOME_FORNECEDOR"].ToString() ?? null
                            };
                            var compra = new Compra()
                            {
                                IdCompra = Convert.ToInt32(reader["ID_COMPRA"]),
                                Fornecedor = fornecedor,
                                DataCompra = reader["DATA_COMPRA"] == DBNull.Value ? new DateTime() : Convert.ToDateTime(reader["DATA_COMPRA"]),
                                ValorCompra = reader["VALOR_COMPRA"] == DBNull.Value ? 0.0 : Convert.ToDouble(reader["VALOR_COMPRA"]),
                                QuantidadeCompra = reader["QUANTIDADE"] == DBNull.Value ? 0 : Convert.ToInt32(reader["QUANTIDADE"]),
                                IdProduto = Convert.ToInt32(reader["ID_PRODUTO"]),
                                IsbnProduto = reader["CNPJ_FORNECEDOR"].ToString() ?? null,
                                IdCategoria = Convert.ToInt32(reader["ID_CATEGORIA"]),
                                TipoCategoria = reader["CATEGORIA_PRODUTO"].ToString() ?? null,
                                DescricaoCategoria = reader["DESCRICAO_CATEGORIA"].ToString() ?? null,
                                TipoAnimal = reader["TIPO_ANIMAL_APLICAVEL"].ToString() ?? null,
                                MarcaProduto = reader["MARCA_PRODUTO"].ToString() ?? null,
                                NomeProduto = reader["NOME_PRODUTO"].ToString() ?? null,
                                QtdeEstoque = reader["QUANTIDADE_PRODUTO"] == DBNull.Value ? 0 : Convert.ToInt32(reader["QUANTIDADE_PRODUTO"]),
                                Funcionario = funcionario
                            };
                            result.Add(compra);
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

        private async Task<Compra> GetComprasById(int idCompra)
        {
            var result = new Compra();
            try
            {
                using (var connection = DbConFactory.CreateConnection())
                {
                    using var command = ComWrapperFac.CreateCommand("sp_BUSCAR_INFO_COMPRAS_ABASTECIMENTO_BY_ID");
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@ID_COMPRA", idCompra);
                    command.Connection = connection;
                    string commandString = command.SqlCommandToString();
                    connection.Open();

                    var reader = await command.ExecuteReaderAsync();
                    if (reader.HasRows)
                    {
                        var funcionario = new Funcionario()
                        {
                            IdFuncionario = Convert.ToInt32(reader["ID_FUNCIONARIO"]),
                            NomeCompleto = reader["NOME_FUNCIONARIO"].ToString() ?? null,
                            Cpf = reader["CPF_FUNCIONARIO"].ToString() ?? null,
                        };
                        var fornecedor = new Fornecedor()
                        {
                            IdFornecedor = Convert.ToInt32(reader["ID_FORNECEDOR"]),
                            CnpjFornecedor = reader["CNPJ_FORNECEDOR"].ToString() ?? null,
                            NomeFornecedor = reader["NOME_FORNECEDOR"].ToString() ?? null
                        };
                        var compra = new Compra()
                        {
                            IdCompra = Convert.ToInt32(reader["ID_COMPRA"]),
                            Fornecedor = fornecedor,
                            DataCompra = reader["DATA_COMPRA"] == DBNull.Value ? new DateTime() : Convert.ToDateTime(reader["DATA_COMPRA"]),
                            ValorCompra = reader["VALOR_COMPRA"] == DBNull.Value ? 0.0 : Convert.ToDouble(reader["VALOR_COMPRA"]),
                            QuantidadeCompra = reader["QUANTIDADE"] == DBNull.Value ? 0 : Convert.ToInt32(reader["QUANTIDADE"]),
                            IdProduto = Convert.ToInt32(reader["ID_PRODUTO"]),
                            IsbnProduto = reader["CNPJ_FORNECEDOR"].ToString() ?? null,
                            IdCategoria = Convert.ToInt32(reader["ID_CATEGORIA"]),
                            TipoCategoria = reader["CATEGORIA_PRODUTO"].ToString() ?? null,
                            DescricaoCategoria = reader["DESCRICAO_CATEGORIA"].ToString() ?? null,
                            TipoAnimal = reader["TIPO_ANIMAL_APLICAVEL"].ToString() ?? null,
                            MarcaProduto = reader["MARCA_PRODUTO"].ToString() ?? null,
                            NomeProduto = reader["NOME_PRODUTO"].ToString() ?? null,
                            QtdeEstoque = reader["QUANTIDADE_PRODUTO"] == DBNull.Value ? 0 : Convert.ToInt32(reader["QUANTIDADE_PRODUTO"]),
                            Funcionario = funcionario
                        };
                    }

                    return result;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Compra> CadastrarCompra(Compra compra)
        {
            try
            {
                using (var connection = DbConFactory.CreateConnection())
                {
                    using var command = ComWrapperFac.CreateCommand("sp_CADASTRAR_INFO_COMPRAS_ABASTECIMENTO");
                    command.CommandType = CommandType.StoredProcedure;
                    command.Connection = connection;

                    // verificar se o output funciona, se não funcionar separar em duas procedures e dois metodos, sendo o segundo private
                    command.Parameters.Add("@ID_COMPRA_OUTPUT");
                    command.Parameters.AddWithValue("@ID_FORNECEDOR", compra.Fornecedor.IdFornecedor);
                    command.Parameters.AddWithValue("@VALOR_COMPRA", compra.ValorCompra);
                    command.Parameters.AddWithValue("@DATA_COMPRA", compra.DataCompra);
                    command.Parameters.AddWithValue("@ID_PRODUTO", compra.IdProduto);
                    command.Parameters.AddWithValue("@ID_FUNCIONARIO", compra.Funcionario.IdFuncionario);
                    command.Parameters.AddWithValue("@QUANTIDADE", compra.QuantidadeCompra);

                    await produtoRepository.AtualizarQtdeProduto(compra.QuantidadeCompra, compra.IdProduto, true);

                    string commandString = command.SqlCommandToString();
                    connection.Open();

                    var idNovaCompra = Convert.ToInt32(await command.ExecuteScalarAsync());

                    var resultCompra = await GetComprasById(idNovaCompra);

                    return resultCompra;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<int> AtualizarCompra(Compra compra)
        {
            bool soma = false;
            bool atualizaQtdeProd = false;
            try
            {
                using (var connection = DbConFactory.CreateConnection())
                {
                    using var command = ComWrapperFac.CreateCommand("sp_EDITAR_INFO_COMPRAS_ABASTECIMENTO");
                    command.CommandType = CommandType.StoredProcedure;
                    command.Connection = connection;

                    command.Parameters.AddWithValue("@ID_COMPRA", compra.IdCompra);
                    command.Parameters.AddWithValue("@ID_FORNECEDOR", compra.Fornecedor.IdFornecedor);
                    command.Parameters.AddWithValue("@VALOR_COMPRA", compra.ValorCompra);
                    command.Parameters.AddWithValue("@DATA_COMPRA", compra.DataCompra);
                    command.Parameters.AddWithValue("@ID_PRODUTO", compra.IdProduto);
                    command.Parameters.AddWithValue("@ID_FUNCIONARIO", compra.Funcionario.IdFuncionario);
                    command.Parameters.AddWithValue("@QUANTIDADE", compra.QuantidadeCompra);

                    var dadosCompraBase = await GetComprasById(compra.IdCompra);

                    if (dadosCompraBase?.IdCompra > 0)
                    {
                        soma = dadosCompraBase.QuantidadeCompra < compra.QuantidadeCompra;
                        atualizaQtdeProd = dadosCompraBase.QuantidadeCompra != compra.QuantidadeCompra;
                    }

                    if (atualizaQtdeProd)
                        await produtoRepository.AtualizarQtdeProduto(compra.QuantidadeCompra, compra.IdProduto, soma);

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

        public async Task<int> ExcluirCompra(int idCompra)
        {
            try
            {
                using (var connection = DbConFactory.CreateConnection())
                {
                    using var command = ComWrapperFac.CreateCommand("sp_EXCLUIR_INFO_COMPRAS_ABASTECIMENTO");
                    command.CommandType = CommandType.StoredProcedure;
                    command.Connection = connection;
                    command.Parameters.AddWithValue("@ID_COMPRA", idCompra);

                    var dadosCompra = await GetComprasById(idCompra);
                    if (dadosCompra.IdCompra > 0)
                        await produtoRepository.AtualizarQtdeProduto(dadosCompra.QuantidadeCompra, dadosCompra.IdProduto, false);

                    string commandString = command.SqlCommandToString();
                    connection.Open();

                    var linhasAfetadas = Convert.ToInt32(await command.ExecuteNonQueryAsync());

                    return linhasAfetadas;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
