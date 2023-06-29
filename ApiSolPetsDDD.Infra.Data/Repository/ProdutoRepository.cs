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
    public class ProdutoRepository : IProdutoRepository
    {
        private readonly IDbConFactory DbConFactory;
        private readonly ISqlComWrapperFac ComWrapperFac;

        public ProdutoRepository(IDbConFactory dbConnectionFactory, ISqlComWrapperFac commandWrapperFactory)
        {
            DbConFactory = dbConnectionFactory;
            ComWrapperFac = commandWrapperFactory;
        }

        public async Task<Produto> GetProdutoById(int idProduto)
        {
            var result = new Produto();
            try
            {
                using (var connection = DbConFactory.CreateConnection())
                {
                    using var command = ComWrapperFac.CreateCommand("sp_BUSCAR_INFO_PRODUTO_BY_ID");
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@ID_PRODUTO", idProduto);
                    command.Connection = connection;
                    string commandString = command.SqlCommandToString();
                    connection.Open();

                    var reader = await command.ExecuteReaderAsync();
                    await reader.ReadAsync();
                    if (reader.HasRows)
                    {
                        result.Fornecedor = new();
                        result.IdProduto = reader["ID_PRODUTO"] == DBNull.Value ? 0 : Convert.ToInt32(reader["ID_PRODUTO"]); ;
                        result.IsbnProduto = reader["ISBN_PRODUTO"].ToString() ?? null;
                        result.NomeProduto = reader["NOME_PRODUTO"].ToString() ?? null;
                        result.MarcaProduto = reader["MARCA_PRODUTO"].ToString() ?? null;
                        result.ValorUnitarioCusto = reader["VALOR_UNITARIO_CUSTO"] == DBNull.Value ? 0.0 : Convert.ToDouble(reader["VALOR_UNITARIO_CUSTO"]);
                        result.ValorUnitarioVenda = reader["VALOR_UNITARIO_VENDA"] == DBNull.Value ? 0.0 : Convert.ToDouble(reader["VALOR_UNITARIO_VENDA"]);
                        result.PesoProduto = reader["PESO_KG"] == DBNull.Value ? 0.0 : Convert.ToDouble(reader["PESO_KG"]);
                        result.PesoAplicavel = reader["PESO_KG_APLICAVEL"] == DBNull.Value ? 0.0 : Convert.ToDouble(reader["PESO_KG_APLICAVEL"]);
                        result.QtdeEstoque = reader["QUANTIDADE_PRODUTO"] == DBNull.Value ? 0 : Convert.ToInt32(reader["QUANTIDADE_PRODUTO"]);
                        result.IdCategoria = reader["ID_CATEGORIA"] == DBNull.Value ? 0 : Convert.ToInt32(reader["ID_CATEGORIA"]);
                        result.DescricaoCategoria = reader["DESCRICAO_CATEGORIA"].ToString() ?? null;
                        result.TipoAnimal = reader["TIPO_ANIMAL_APLICAVEL"].ToString() ?? null;
                        result.TipoCategoria = reader["CATEGORIA_PRODUTO"].ToString() ?? null;
                        result.FotoProduto = reader["FOTO_PRODUTO"].ToString() ?? null;
                        result.DhUltimaAtualizacao = reader["DATA_HORA_ULT_ATUALIZACAO"] == DBNull.Value ? new DateTime() : Convert.ToDateTime(reader["DATA_HORA_ULT_ATUALIZACAO"]);
                        result.DataValidade = reader["DATA_VALIDADE"] == DBNull.Value ? new DateTime() : Convert.ToDateTime(reader["DATA_VALIDADE"]);
                        result.Fornecedor.IdFornecedor = reader["ID_FORNECEDOR"] == DBNull.Value ? 0 : Convert.ToInt32(reader["ID_FORNECEDOR"]);
                        result.Fornecedor.NomeFornecedor = reader["NOME_FORNECEDOR"].ToString() ?? null;
                        result.Fornecedor.CnpjFornecedor = reader["CNPJ_FORNECEDOR"].ToString() ?? null;
                    }

                    return result;
                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Produto> GetProdutoByISBN(string isbn)
        {
            var result = new Produto();
            try
            {
                using (var connection = DbConFactory.CreateConnection())
                {
                    using var command = ComWrapperFac.CreateCommand("sp_BUSCAR_INFO_PRODUTO");
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@ISBN_PRODUTO", isbn);
                    command.Connection = connection;
                    string commandString = command.SqlCommandToString();
                    connection.Open();

                    var reader = await command.ExecuteReaderAsync();
                    await reader.ReadAsync();
                    if (reader.HasRows)
                    {
                        result.Fornecedor = new();
                        result.IdProduto = reader["ID_PRODUTO"] == DBNull.Value ? 0 : Convert.ToInt32(reader["ID_PRODUTO"]); ;
                        result.IsbnProduto = reader["ISBN_PRODUTO"].ToString() ?? null;
                        result.NomeProduto = reader["NOME_PRODUTO"].ToString() ?? null;
                        result.MarcaProduto = reader["MARCA_PRODUTO"].ToString() ?? null;
                        result.ValorUnitarioCusto = reader["VALOR_UNITARIO_CUSTO"] == DBNull.Value ? 0.0 : Convert.ToDouble(reader["VALOR_UNITARIO_CUSTO"]);
                        result.ValorUnitarioVenda = reader["VALOR_UNITARIO_VENDA"] == DBNull.Value ? 0.0 : Convert.ToDouble(reader["VALOR_UNITARIO_VENDA"]);
                        result.PesoProduto = reader["PESO_KG"] == DBNull.Value ? 0.0 : Convert.ToDouble(reader["PESO_KG"]);
                        result.PesoAplicavel = reader["PESO_KG_APLICAVEL"] == DBNull.Value ? 0.0 : Convert.ToDouble(reader["PESO_KG_APLICAVEL"]);
                        result.QtdeEstoque = reader["QUANTIDADE_PRODUTO"] == DBNull.Value ? 0 : Convert.ToInt32(reader["QUANTIDADE_PRODUTO"]);
                        result.IdCategoria = reader["ID_CATEGORIA"] == DBNull.Value ? 0 : Convert.ToInt32(reader["ID_CATEGORIA"]);
                        result.DescricaoCategoria = reader["DESCRICAO_CATEGORIA"].ToString() ?? null;
                        result.TipoAnimal = reader["TIPO_ANIMAL_APLICAVEL"].ToString() ?? null;
                        result.TipoCategoria = reader["CATEGORIA_PRODUTO"].ToString() ?? null;
                        result.FotoProduto = reader["FOTO_PRODUTO"].ToString() ?? null;
                        result.DhUltimaAtualizacao = reader["DATA_HORA_ULT_ATUALIZACAO"] == DBNull.Value ? new DateTime() : Convert.ToDateTime(reader["DATA_HORA_ULT_ATUALIZACAO"]);
                        result.DataValidade = reader["DATA_VALIDADE"] == DBNull.Value ? new DateTime() : Convert.ToDateTime(reader["DATA_VALIDADE"]);
                        result.Fornecedor.IdFornecedor = reader["ID_FORNECEDOR"] == DBNull.Value ? 0 : Convert.ToInt32(reader["ID_FORNECEDOR"]);
                        result.Fornecedor.NomeFornecedor = reader["NOME_FORNECEDOR"].ToString() ?? null;
                        result.Fornecedor.CnpjFornecedor = reader["CNPJ_FORNECEDOR"].ToString() ?? null;
                    }

                    return result;
                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<Produto>> GetTop10ProdutosByNome(string nomeProduto)
        {
            var result = new List<Produto>();
            try
            {
                using (var connection = DbConFactory.CreateConnection())
                {
                    using var command = ComWrapperFac.CreateCommand("sp_BUSCAR_INFO_PRODUTOS_BY_NOME");
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@NOME_PRODUTO", nomeProduto);
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
                                CnpjFornecedor = reader["CNPJ_FORNECEDOR"].ToString() ?? null,
                            };
                            var produto = new Produto()
                            {
                                IdProduto = reader["ID_PRODUTO"] == DBNull.Value ? 0 : Convert.ToInt32(reader["ID_PRODUTO"]),
                                IsbnProduto = reader["ISBN_PRODUTO"].ToString() ?? null,
                                NomeProduto = reader["NOME_PRODUTO"].ToString() ?? null,
                                MarcaProduto = reader["MARCA_PRODUTO"].ToString() ?? null,
                                ValorUnitarioCusto = reader["VALOR_UNITARIO_CUSTO"] == DBNull.Value ? 0.0 : Convert.ToDouble(reader["VALOR_UNITARIO_CUSTO"]),
                                ValorUnitarioVenda = reader["VALOR_UNITARIO_VENDA"] == DBNull.Value ? 0.0 : Convert.ToDouble(reader["VALOR_UNITARIO_VENDA"]),
                                PesoProduto = reader["PESO_KG"] == DBNull.Value ? 0.0 : Convert.ToDouble(reader["PESO_KG"]),
                                PesoAplicavel = reader["PESO_KG_APLICAVEL"] == DBNull.Value ? 0.0 : Convert.ToDouble(reader["PESO_KG_APLICAVEL"]),
                                QtdeEstoque = reader["QUANTIDADE_PRODUTO"] == DBNull.Value ? 0 : Convert.ToInt32(reader["QUANTIDADE_PRODUTO"]),
                                IdCategoria = reader["ID_CATEGORIA"] == DBNull.Value ? 0 : Convert.ToInt32(reader["ID_CATEGORIA"]),
                                DescricaoCategoria = reader["DESCRICAO_CATEGORIA"].ToString() ?? null,
                                TipoAnimal = reader["TIPO_ANIMAL_APLICAVEL"].ToString() ?? null,
                                TipoCategoria = reader["CATEGORIA_PRODUTO"].ToString() ?? null,
                                DhUltimaAtualizacao = reader["DATA_HORA_ULT_ATUALIZACAO"] == DBNull.Value ? new DateTime() : Convert.ToDateTime(reader["DATA_HORA_ULT_ATUALIZACAO"]),
                                DataValidade = reader["DATA_VALIDADE"] == DBNull.Value ? new DateTime() : Convert.ToDateTime(reader["DATA_VALIDADE"]),
                                Fornecedor = fornecedor,
                                FotoProduto = reader["FOTO_PRODUTO"].ToString() ?? null
                            };
                            result.Add(produto);
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

        public async Task<List<Produto>> GetProdutosAVencer()
        {
            var result = new List<Produto>();
            try
            {
                using (var connection = DbConFactory.CreateConnection())
                {
                    using var command = ComWrapperFac.CreateCommand("sp_BUSCAR_INFO_PRODUTO_A_VENCER");
                    command.CommandType = CommandType.StoredProcedure;
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
                                CnpjFornecedor = reader["CNPJ_FORNECEDOR"].ToString() ?? null,
                            };
                            var produto = new Produto()
                            {
                                IdProduto = reader["ID_PRODUTO"] == DBNull.Value ? 0 : Convert.ToInt32(reader["ID_PRODUTO"]),
                                IsbnProduto = reader["ISBN_PRODUTO"].ToString() ?? null,
                                NomeProduto = reader["NOME_PRODUTO"].ToString() ?? null,
                                MarcaProduto = reader["MARCA_PRODUTO"].ToString() ?? null,
                                ValorUnitarioCusto = reader["VALOR_UNITARIO_CUSTO"] == DBNull.Value ? 0.0 : Convert.ToDouble(reader["VALOR_UNITARIO_CUSTO"]),
                                ValorUnitarioVenda = reader["VALOR_UNITARIO_VENDA"] == DBNull.Value ? 0.0 : Convert.ToDouble(reader["VALOR_UNITARIO_VENDA"]),
                                PesoProduto = reader["PESO_KG"] == DBNull.Value ? 0.0 : Convert.ToDouble(reader["PESO_KG"]),
                                PesoAplicavel = reader["PESO_KG_APLICAVEL"] == DBNull.Value ? 0.0 : Convert.ToDouble(reader["PESO_KG_APLICAVEL"]),
                                QtdeEstoque = reader["QUANTIDADE_PRODUTO"] == DBNull.Value ? 0 : Convert.ToInt32(reader["QUANTIDADE_PRODUTO"]),
                                IdCategoria = reader["ID_CATEGORIA"] == DBNull.Value ? 0 : Convert.ToInt32(reader["ID_CATEGORIA"]),
                                DescricaoCategoria = reader["DESCRICAO_CATEGORIA"].ToString() ?? null,
                                TipoAnimal = reader["TIPO_ANIMAL_APLICAVEL"].ToString() ?? null,
                                TipoCategoria = reader["CATEGORIA_PRODUTO"].ToString() ?? null,
                                DhUltimaAtualizacao = reader["DATA_HORA_ULT_ATUALIZACAO"] == DBNull.Value ? new DateTime() : Convert.ToDateTime(reader["DATA_HORA_ULT_ATUALIZACAO"]),
                                DataValidade = reader["DATA_VALIDADE"] == DBNull.Value ? new DateTime() : Convert.ToDateTime(reader["DATA_VALIDADE"]),
                                Fornecedor = fornecedor,
                                FotoProduto = reader["FOTO_PRODUTO"].ToString() ?? null
                            };
                            result.Add(produto);
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

        public async Task<List<Produto>> GetTop10ProdutosByCategoria(int idCategoria)
        {
            var result = new List<Produto>();
            try
            {
                using (var connection = DbConFactory.CreateConnection())
                {
                    using var command = ComWrapperFac.CreateCommand("sp_BUSCAR_INFO_PRODUTOS_BY_CATEGORIA");
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@ID_CATEGORIA", idCategoria);
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
                                CnpjFornecedor = reader["CNPJ_FORNECEDOR"].ToString() ?? null,
                            };
                            var produto = new Produto()
                            {
                                IdProduto = reader["ID_PRODUTO"] == DBNull.Value ? 0 : Convert.ToInt32(reader["ID_PRODUTO"]),
                                IsbnProduto = reader["ISBN_PRODUTO"].ToString() ?? null,
                                NomeProduto = reader["NOME_PRODUTO"].ToString() ?? null,
                                MarcaProduto = reader["MARCA_PRODUTO"].ToString() ?? null,
                                ValorUnitarioCusto = reader["VALOR_UNITARIO_CUSTO"] == DBNull.Value ? 0.0 : Convert.ToDouble(reader["VALOR_UNITARIO_CUSTO"]),
                                ValorUnitarioVenda = reader["VALOR_UNITARIO_VENDA"] == DBNull.Value ? 0.0 : Convert.ToDouble(reader["VALOR_UNITARIO_VENDA"]),
                                PesoProduto = reader["PESO_KG"] == DBNull.Value ? 0.0 : Convert.ToDouble(reader["PESO_KG"]),
                                PesoAplicavel = reader["PESO_KG_APLICAVEL"] == DBNull.Value ? 0.0 : Convert.ToDouble(reader["PESO_KG_APLICAVEL"]),
                                QtdeEstoque = reader["QUANTIDADE_PRODUTO"] == DBNull.Value ? 0 : Convert.ToInt32(reader["QUANTIDADE_PRODUTO"]),
                                IdCategoria = reader["ID_CATEGORIA"] == DBNull.Value ? 0 : Convert.ToInt32(reader["ID_CATEGORIA"]),
                                DescricaoCategoria = reader["DESCRICAO_CATEGORIA"].ToString() ?? null,
                                TipoAnimal = reader["TIPO_ANIMAL_APLICAVEL"].ToString() ?? null,
                                TipoCategoria = reader["CATEGORIA_PRODUTO"].ToString() ?? null,
                                DhUltimaAtualizacao = reader["DATA_HORA_ULT_ATUALIZACAO"] == DBNull.Value ? new DateTime() : Convert.ToDateTime(reader["DATA_HORA_ULT_ATUALIZACAO"]),
                                DataValidade = reader["DATA_VALIDADE"] == DBNull.Value ? new DateTime() : Convert.ToDateTime(reader["DATA_VALIDADE"]),
                                Fornecedor = fornecedor,
                                FotoProduto = reader["FOTO_PRODUTO"].ToString() ?? null
                            };
                            result.Add(produto);
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

        public async Task<Produto> CadastrarProduto(Produto produto)
        {
            try
            {
                using (var connection = DbConFactory.CreateConnection())
                {
                    using var command = ComWrapperFac.CreateCommand("sp_CADASTRAR_INFO_PRODUTO");
                    command.CommandType = CommandType.StoredProcedure;
                    command.Connection = connection;

                    command.Parameters.AddWithValue("@ID_CATEGORIA", produto.IdCategoria);
                    command.Parameters.AddWithValue("@ISBN_PRODUTO", produto.IsbnProduto);
                    command.Parameters.AddWithValue("@NOME_PRODUTO", produto.NomeProduto);
                    command.Parameters.AddWithValue("@MARCA_PRODUTO", produto.MarcaProduto);
                    command.Parameters.AddWithValue("@VALOR_UNITARIO_CUSTO", produto.ValorUnitarioCusto);
                    command.Parameters.AddWithValue("@VALOR_UNITARIO_VENDA", produto.ValorUnitarioVenda);
                    command.Parameters.AddWithValue("@QUANTIDADE_PRODUTO", produto.QtdeEstoque);
                    command.Parameters.AddWithValue("@ID_FORNECEDOR", produto.Fornecedor.IdFornecedor);
                    command.Parameters.AddWithValue("@PESO_KG", produto.PesoProduto);
                    command.Parameters.AddWithValue("@DATA_VALIDADE", produto.DataValidade);
                    command.Parameters.AddWithValue("@IDADE_APLICAVEL", produto.IdadeAplicavel);
                    command.Parameters.AddWithValue("@PESO_KG_APLICAVEL", produto.PesoAplicavel);
                    command.Parameters.AddWithValue("@FOTO_PRODUTO", produto.FotoProduto);

                    string commandString = command.SqlCommandToString();
                    connection.Open();

                    var idNovoProduto = Convert.ToInt32(await command.ExecuteScalarAsync());

                    var result = await GetProdutoById(idNovoProduto);

                    return result;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<int> AtualizarInfoProduto(Produto produto)
        {
            try
            {
                using (var connection = DbConFactory.CreateConnection())
                {
                    using var command = ComWrapperFac.CreateCommand("sp_EDITAR_INFO_PRODUTO");
                    command.CommandType = CommandType.StoredProcedure;
                    command.Connection = connection;

                    command.Parameters.AddWithValue("@ID_CATEGORIA", produto.IdCategoria);
                    command.Parameters.AddWithValue("@ISBN_PRODUTO", produto.IsbnProduto);
                    command.Parameters.AddWithValue("@NOME_PRODUTO", produto.NomeProduto);
                    command.Parameters.AddWithValue("@MARCA_PRODUTO", produto.MarcaProduto);
                    command.Parameters.AddWithValue("@VALOR_UNITARIO_CUSTO", produto.ValorUnitarioCusto);
                    command.Parameters.AddWithValue("@VALOR_UNITARIO_VENDA", produto.ValorUnitarioVenda);
                    command.Parameters.AddWithValue("@QUANTIDADE_PRODUTO", produto.QtdeEstoque);
                    command.Parameters.AddWithValue("@ID_FORNECEDOR", produto.Fornecedor.IdFornecedor);
                    command.Parameters.AddWithValue("@PESO_KG", produto.PesoProduto);
                    command.Parameters.AddWithValue("@ID_PRODUTO", produto.IdProduto);
                    command.Parameters.AddWithValue("@DATA_VALIDADE", produto.DataValidade);
                    command.Parameters.AddWithValue("@IDADE_APLICAVEL", produto.IdadeAplicavel);
                    command.Parameters.AddWithValue("@PESO_APLICAVEL", produto.PesoAplicavel);
                    command.Parameters.AddWithValue("@FOTO_PRODUTO", produto.FotoProduto);

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

        public async Task<int> AtualizarQtdeProduto(int quantidade, int idProduto, bool soma)
        {
            try
            {
                using (var connection = DbConFactory.CreateConnection())
                {
                    using var command = ComWrapperFac.CreateCommand("sp_EDITAR_QTDE_PRODUTO");
                    command.CommandType = CommandType.StoredProcedure;
                    command.Connection = connection;

                    command.Parameters.AddWithValue("@QUANTIDADE_PRODUTO", quantidade);
                    command.Parameters.AddWithValue("@ID_PRODUTO", idProduto);
                    command.Parameters.AddWithValue("@SOMAR", soma);
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

        public async Task<int> ExcluirProduto(int idProduto)
        {
            try
            {
                using (var connection = DbConFactory.CreateConnection())
                {
                    using var command = ComWrapperFac.CreateCommand("DELETE FROM PRODUTO WHERE ID_PRODUTO=@ID_PRODUTO");
                    command.CommandType = CommandType.Text;
                    command.Connection = connection;

                    command.Parameters.AddWithValue("@ID_PRODUTO", idProduto);
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

        public async Task<List<Produto>> GetCustoTotalEstoque()
        {
            var result = new List<Produto>();
            try
            {
                using (var connection = DbConFactory.CreateConnection())
                {
                    string query = @"SELECT 
                                            PROD.ID_PRODUTO,
                                            PROD.NOME_PRODUTO,
                                            PROD.VALOR_UNITARIO_CUSTO, 
                                            PROD.QUANTIDADE_PRODUTO
                                    FROM PRODUTO PROD WITH(NOLOCK)
                                    WHERE PROD.QUANTIDADE_PRODUTO > 0";
                    using var command = ComWrapperFac.CreateCommand(query);
                    command.CommandType = CommandType.Text;
                    command.Connection = connection;
                    string commandString = command.SqlCommandToString();
                    connection.Open();

                    var reader = await command.ExecuteReaderAsync();
                    while (await reader.ReadAsync())
                    {
                        if (reader.HasRows)
                        {
                            var produto = new Produto()
                            {
                                ValorUnitarioCusto = reader["VALOR_UNITARIO_CUSTO"] == DBNull.Value ? 0.0 : Convert.ToDouble(reader["VALOR_UNITARIO_CUSTO"]),
                                QtdeEstoque = reader["QUANTIDADE_PRODUTO"] == DBNull.Value ? 0 : Convert.ToInt32(reader["QUANTIDADE_PRODUTO"]),
                                IdProduto = reader["ID_PRODUTO"] == DBNull.Value ? 0 : Convert.ToInt32(reader["ID_PRODUTO"]),
                                NomeProduto = reader["NOME_PRODUTO"].ToString() ?? null
                            };
                            result.Add(produto);
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
    }
}
