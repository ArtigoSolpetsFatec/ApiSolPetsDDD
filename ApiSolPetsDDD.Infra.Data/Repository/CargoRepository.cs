using APISolPets.Domain.Extensions;
using APISolPets.Domain.Interfaces;
using ApiSolPetsDDD.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace ApiSolPetsDDD.Infra.Data.Repository
{
    public class CargoRepository : ICargoRepository
    {
        private readonly IDbConFactory DbConFactory;
        private readonly ISqlComWrapperFac ComWrapperFac;

        public CargoRepository(IDbConFactory dbConnectionFactory, ISqlComWrapperFac commandWrapperFactory)
        {
            DbConFactory = dbConnectionFactory;
            ComWrapperFac = commandWrapperFactory;
        }

        public async Task<Cargo> GetCargoById(int idCargo)
        {
            var result = new Cargo();
            try
            {
                using (var connection = DbConFactory.CreateConnection())
                {
                    using var command = ComWrapperFac.CreateCommand("sp_BUSCAR_INFO_CARGO_BY_ID");
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@ID_CARGO", idCargo);
                    command.Connection = connection;
                    string commandString = command.SqlCommandToString();
                    connection.Open();

                    var reader = await command.ExecuteReaderAsync();
                    await reader.ReadAsync();
                    if (reader.HasRows)
                    {
                        result.IdCargo = idCargo;
                        result.NomeCargo = reader["NOME_CARGO"].ToString() ?? null;
                        result.Salario = reader["SALARIO_CARGO"] == DBNull.Value ? 0.0 : Convert.ToDouble(reader["SALARIO_CARGO"]);
                        result.DhUltimaAtualizacao = reader["DATA_HORA_ULT_ATUALIZACAO"] == DBNull.Value ? new DateTime() : Convert.ToDateTime(reader["DATA_HORA_ULT_ATUALIZACAO"]);
                    }

                    return result;
                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Cargo> GetCargoByName(string descricaoCargo)
        {
            var result = new Cargo();
            try
            {
                using (var connection = DbConFactory.CreateConnection())
                {
                    var query = @"SELECT * FROM CARGO WITH(NOLOCK)
                                  WHERE 1=1 AND NOME_CARGO LIKE @DESCRICAO_CARGO";
                    using var command = ComWrapperFac.CreateCommand(query);
                    command.CommandType = CommandType.Text;
                    command.Parameters.AddWithValue("@DESCRICAO_CARGO", descricaoCargo);
                    command.Connection = connection;
                    string commandString = command.SqlCommandToString();
                    connection.Open();

                    var reader = await command.ExecuteReaderAsync();
                    await reader.ReadAsync();
                    if (reader.HasRows)
                    {
                        result.IdCargo = reader["ID_CARGO"] == DBNull.Value ? 0 : Convert.ToInt32(reader["ID_CARGO"]);
                        result.NomeCargo = reader["NOME_CARGO"].ToString() ?? null;
                        result.Salario = reader["SALARIO_CARGO"] == DBNull.Value ? 0.0 : Convert.ToDouble(reader["SALARIO_CARGO"]);
                        result.DhUltimaAtualizacao = reader["DATA_HORA_ULT_ATUALIZACAO"] == DBNull.Value ? new DateTime() : Convert.ToDateTime(reader["DATA_HORA_ULT_ATUALIZACAO"]);
                    }

                    return result;
                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<Cargo>> GetCargos()
        {
            var result = new List<Cargo>();
            try
            {
                using (var connection = DbConFactory.CreateConnection())
                {
                    using var command = ComWrapperFac.CreateCommand("SELECT * FROM CARGO WITH(NOLOCK)");
                    command.CommandType = CommandType.Text;
                    command.Connection = connection;
                    string commandString = command.SqlCommandToString();
                    connection.Open();

                    var reader = await command.ExecuteReaderAsync();
                    while (await reader.ReadAsync())
                    {
                        if (reader.HasRows)
                        {
                            var cargo = new Cargo()
                            {
                                IdCargo = reader["ID_CARGO"] == DBNull.Value ? 0 : Convert.ToInt32(reader["ID_CARGO"]),
                                NomeCargo = reader["NOME_CARGO"].ToString() ?? null,
                                Salario = reader["SALARIO_CARGO"] == DBNull.Value ? 0.0 : Convert.ToDouble(reader["SALARIO_CARGO"]),
                                DhUltimaAtualizacao = reader["DATA_HORA_ULT_ATUALIZACAO"] == DBNull.Value ? new DateTime() :
                                Convert.ToDateTime(reader["DATA_HORA_ULT_ATUALIZACAO"])
                            };
                            result.Add(cargo);
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

        public async Task<Cargo> CadastrarCargo(Cargo cargo)
        {
            try
            {
                using (var connection = DbConFactory.CreateConnection())
                {
                    using var command = ComWrapperFac.CreateCommand("sp_CADASTRAR_INFO_CARGO");
                    command.CommandType = CommandType.StoredProcedure;
                    command.Connection = connection;

                    command.Parameters.AddWithValue("@NOME_CARGO", cargo.NomeCargo);
                    command.Parameters.AddWithValue("@SALARIO_CARGO", cargo.Salario);
                    string commandString = command.SqlCommandToString();
                    connection.Open();

                    var idNovoCargo = Convert.ToInt32(await command.ExecuteScalarAsync());

                    var result = await GetCargoById(idNovoCargo);

                    return result;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<int> AtualizarCargo(Cargo cargo)
        {
            try
            {
                using (var connection = DbConFactory.CreateConnection())
                {
                    using var command = ComWrapperFac.CreateCommand("sp_EDITAR_INFO_CARGO");
                    command.CommandType = CommandType.StoredProcedure;
                    command.Connection = connection;

                    command.Parameters.AddWithValue("@NOME_CARGO", cargo.NomeCargo);
                    command.Parameters.AddWithValue("@SALARIO_CARGO", cargo.Salario);
                    command.Parameters.AddWithValue("@ID_CARGO", cargo.IdCargo);
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

        public async Task<int> ExcluirCargo(int idCargo)
        {
            try
            {
                using (var connection = DbConFactory.CreateConnection())
                {
                    using var command = ComWrapperFac.CreateCommand("DELETE FROM CARGO WHERE ID_CARGO=@ID_CARGO");
                    command.CommandType = CommandType.Text;
                    command.Parameters.AddWithValue("@ID_CARGO", idCargo);
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
