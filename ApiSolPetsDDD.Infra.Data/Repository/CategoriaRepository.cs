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
    public class CategoriaRepository : ICategoriaRepository
    {

        private readonly IDbConFactory DbConFactory;
        private readonly ISqlComWrapperFac ComWrapperFac;

        public CategoriaRepository(IDbConFactory dbConnectionFactory, ISqlComWrapperFac commandWrapperFactory)
        {
            DbConFactory = dbConnectionFactory;
            ComWrapperFac = commandWrapperFactory;
        }

        public async Task<Categoria> GetCategoriaById(int idCategoria)
        {
            var result = new Categoria();
            try
            {
                using (var connection = DbConFactory.CreateConnection())
                {
                    using var command = ComWrapperFac.CreateCommand("sp_BUSCAR_INFO_CATEGORIA_BY_ID");
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@ID_CATEGORIA", idCategoria);
                    command.Connection = connection;
                    string commandString = command.SqlCommandToString();
                    connection.Open();

                    var reader = await command.ExecuteReaderAsync();
                    await reader.ReadAsync();
                    if (reader.HasRows)
                    {
                        result.IdCategoria = reader["ID_CATEGORIA"] == DBNull.Value ? 0 : Convert.ToInt32(reader["ID_CATEGORIA"]);
                        result.DescricaoCategoria = reader["DESCRICAO_CATEGORIA"].ToString() ?? null;
                        result.TipoAnimal = reader["TIPO_ANIMAL_APLICAVEL"].ToString() ?? null;
                        result.TipoCategoria = reader["CATEGORIA_PRODUTO"].ToString() ?? null;
                    }

                    return result;
                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Categoria> GetCategoriaByName(string nomeCategoria)
        {
            var result = new Categoria();
            try
            {
                using (var connection = DbConFactory.CreateConnection())
                {
                    string query = @"SELECT * FROM CATEGORIA WITH(NOLOCK)
                                     WHERE 1=1 AND CATEGORIA_PRODUTO LIKE @CATEGORIA_PRODUTO";
                    using var command = ComWrapperFac.CreateCommand(query);
                    command.CommandType = CommandType.Text;
                    command.Parameters.AddWithValue("@CATEGORIA_PRODUTO", nomeCategoria);
                    command.Connection = connection;
                    string commandString = command.SqlCommandToString();
                    connection.Open();

                    var reader = await command.ExecuteReaderAsync();
                    await reader.ReadAsync();
                    if (reader.HasRows)
                    {
                        result.IdCategoria = reader["ID_CATEGORIA"] == DBNull.Value ? 0 : Convert.ToInt32(reader["ID_CATEGORIA"]);
                        result.DescricaoCategoria = reader["DESCRICAO_CATEGORIA"].ToString() ?? null;
                        result.TipoAnimal = reader["TIPO_ANIMAL_APLICAVEL"].ToString() ?? null;
                        result.TipoCategoria = reader["CATEGORIA_PRODUTO"].ToString() ?? null;
                    }

                    return result;
                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<Categoria>> GetAllCategorias()
        {
            var result = new List<Categoria>();
            try
            {
                using (var connection = DbConFactory.CreateConnection())
                {
                    using var command = ComWrapperFac.CreateCommand("SELECT TOP(1000) * FROM CATEGORIA WITH(NOLOCK)");
                    command.CommandType = CommandType.Text;
                    command.Connection = connection;
                    string commandString = command.SqlCommandToString();
                    connection.Open();

                    var reader = await command.ExecuteReaderAsync();
                    while (await reader.ReadAsync())
                    {
                        if (reader.HasRows)
                        {
                            var categoria = new Categoria()
                            {
                                IdCategoria = reader["ID_CATEGORIA"] == DBNull.Value ? 0 : Convert.ToInt32(reader["ID_CATEGORIA"]),
                                DescricaoCategoria = reader["DESCRICAO_CATEGORIA"].ToString() ?? null,
                                TipoAnimal = reader["TIPO_ANIMAL_APLICAVEL"].ToString() ?? null,
                                TipoCategoria = reader["CATEGORIA_PRODUTO"].ToString() ?? null,
                            };
                            result.Add(categoria);
                        }
                    }
                }

                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Categoria> CadastrarCategoria(Categoria categoria)
        {
            try
            {
                using (var connection = DbConFactory.CreateConnection())
                {
                    using var command = ComWrapperFac.CreateCommand("sp_CADASTRAR_INFO_CATEGORIA");
                    command.CommandType = CommandType.StoredProcedure;
                    command.Connection = connection;

                    command.Parameters.AddWithValue("@DESCRICAO_CATEGORIA", categoria.DescricaoCategoria);
                    command.Parameters.AddWithValue("@TIPO_ANIMAL_APLICAVEL", categoria.TipoAnimal);
                    command.Parameters.AddWithValue("@CATEGORIA_PRODUTO", categoria.TipoCategoria);
                    string commandString = command.SqlCommandToString();
                    connection.Open();

                    var idNovaCategoria = Convert.ToInt32(await command.ExecuteScalarAsync());

                    var result = await GetCategoriaById(idNovaCategoria);

                    return result;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<int> AtualizarInfoCategoria(Categoria categoria)
        {
            try
            {
                using (var connection = DbConFactory.CreateConnection())
                {
                    using var command = ComWrapperFac.CreateCommand("sp_EDITAR_INFO_CATEGORIA");
                    command.CommandType = CommandType.StoredProcedure;
                    command.Connection = connection;

                    command.Parameters.AddWithValue("@DESCRICAO_CATEGORIA", categoria.DescricaoCategoria);
                    command.Parameters.AddWithValue("@TIPO_ANIMAL_APLICAVEL", categoria.TipoAnimal);
                    command.Parameters.AddWithValue("@CATEGORIA_PRODUTO", categoria.TipoCategoria);
                    command.Parameters.AddWithValue("@ID_CATEGORIA", categoria.IdCategoria);
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

        public async Task<int> ExcluirCategoria(int idCategoria)
        {
            try
            {
                using (var connection = DbConFactory.CreateConnection())
                {
                    using var command = ComWrapperFac.CreateCommand("DELETE FROM CATEGORIA WHERE ID_CATEGORIA=@ID_CATEGORIA");
                    command.CommandType = CommandType.Text;
                    command.Parameters.AddWithValue("@ID_CATEGORIA", idCategoria);
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
