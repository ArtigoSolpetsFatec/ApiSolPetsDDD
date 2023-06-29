using APISolPets.Domain.Extensions;
using APISolPets.Domain.Interfaces;
using ApiSolPetsDDD.Domain.Interfaces;
using ApiSolPetsDDD.Domain.Model;
using System;
using System.Data;
using System.Threading.Tasks;

namespace ApiSolPetsDDD.Infra.Data.Repository
{
    public class LoginRepository : ILoginRepository
    {
        private readonly IDbConFactory DbConFactory;
        private readonly ISqlComWrapperFac ComWrapperFac;

        public LoginRepository(IDbConFactory dbConnectionFactory, ISqlComWrapperFac commandWrapperFactory)
        {
            DbConFactory = dbConnectionFactory;
            ComWrapperFac = commandWrapperFactory;
        }

        public async Task<Login> GetLogin(string email)
        {
            var result = new Login();
            try
            {
                using (var connection = DbConFactory.CreateConnection())
                {
                    using var command = ComWrapperFac.CreateCommand("sp_BUSCAR_LOGIN_BY_EMAIL");
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@EMAIL_LOGIN", email);
                    command.Connection = connection;
                    string commandString = command.SqlCommandToString();
                    connection.Open();

                    var reader = await command.ExecuteReaderAsync();
                    await reader.ReadAsync();
                    if (reader.HasRows)
                    {
                        result.IdLogin = Convert.ToInt32(reader["ID_LOGIN"]);
                        result.Email = reader["EMAIL_LOGIN"].ToString() ?? null;
                        result.IsAdmin = reader["IS_ADMIN"] == DBNull.Value ? false : Convert.ToBoolean(reader["IS_ADMIN"]);
                        result.Senha = reader["SENHA_LOGIN"].ToString() ?? null;
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

        public async Task<Login> GetInfoLogin(int idLogin)
        {
            var result = new Login();
            try
            {
                using (var connection = DbConFactory.CreateConnection())
                {
                    using var command = ComWrapperFac.CreateCommand("sp_BUSCAR_INFO_LOGIN_BY_ID");
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@ID_LOGIN", idLogin);
                    command.Connection = connection;
                    string commandString = command.SqlCommandToString();
                    connection.Open();

                    var reader = await command.ExecuteReaderAsync();
                    await reader.ReadAsync();
                    if (reader.HasRows)
                    {
                        result.IdLogin = Convert.ToInt32(reader["ID_LOGIN"]);
                        result.Email = reader["EMAIL_LOGIN"].ToString() ?? null;
                        result.IsAdmin = reader["IS_ADMIN"] == DBNull.Value ? false : Convert.ToBoolean(reader["IS_ADMIN"]);
                        result.Senha = reader["SENHA_LOGIN"].ToString() ?? null;
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

        public async Task<Login> CriarLogin(Login login)
        {
            try
            {
                using (var connection = DbConFactory.CreateConnection())
                {
                    using var command = ComWrapperFac.CreateCommand("sp_CADASTRAR_INFO_LOGIN");
                    command.CommandType = CommandType.StoredProcedure;
                    command.Connection = connection;

                    command.Parameters.AddWithValue("@EMAIL_LOGIN", login.Email);
                    if (string.IsNullOrEmpty(login.SenhaCriptografada))
                        command.Parameters.AddWithValue("@SENHA_LOGIN", login.Senha);
                    else
                        command.Parameters.AddWithValue("@SENHA_LOGIN", login.SenhaCriptografada);
                    command.Parameters.AddWithValue("@IS_ADMIN", login.IsAdmin);
                    string commandString = command.SqlCommandToString();
                    connection.Open();

                    var idNovoLogin = Convert.ToInt32(await command.ExecuteScalarAsync());

                    var result = await GetInfoLogin(idNovoLogin);

                    return result;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<int> AtualizarLogin(Login login)
        {
            try
            {
                using (var connection = DbConFactory.CreateConnection())
                {
                    using var command = ComWrapperFac.CreateCommand("sp_EDITAR_INFO_LOGIN");
                    command.CommandType = CommandType.StoredProcedure;
                    command.Connection = connection;

                    command.Parameters.AddWithValue("@SENHA_LOGIN", login.Senha);
                    command.Parameters.AddWithValue("@IS_ADMIN", login.IsAdmin);
                    command.Parameters.AddWithValue("@ID_LOGIN", login.IdLogin);
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

        public async Task<int> ExcluirLogin(int idLogin)
        {
            try
            {
                using (var connection = DbConFactory.CreateConnection())
                {
                    using var command = ComWrapperFac.CreateCommand("DELETE FROM LOGIN_USER WHERE ID_LOGIN=@ID_LOGIN");
                    command.CommandType = CommandType.Text;
                    command.Parameters.AddWithValue("@ID_LOGIN", idLogin);
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
