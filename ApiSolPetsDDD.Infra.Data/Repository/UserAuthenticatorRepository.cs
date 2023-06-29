using APISolPets.Domain.Extensions;
using APISolPets.Domain.Interfaces;
using ApiSolPetsDDD.Domain.Interfaces;
using ApiSolPetsDDD.Domain.Model;
using System;
using System.Data;
using System.Threading.Tasks;

namespace ApiSolPetsDDD.Infra.Data.Repository
{
    public class UserAuthenticatorRepository : IUserAuthenticatorRepository
    {
        private readonly IDbConFactory DbConFactory;
        private readonly ISqlComWrapperFac ComWrapperFac;

        public UserAuthenticatorRepository(IDbConFactory dbConnectionFactory, ISqlComWrapperFac commandWrapperFactory)
        {
            DbConFactory = dbConnectionFactory;
            ComWrapperFac = commandWrapperFactory;
        }

        public async Task<UserAuthenticator> GetUser(string username, string password)
        {
            var result = new UserAuthenticator();
            try
            {
                using (var connection = DbConFactory.CreateConnection())
                {
                    using var command = ComWrapperFac.CreateCommand("sp_BUSCAR_INFO_USER_AUTHENTICATOR");
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@USERNAME", username);
                    command.Parameters.AddWithValue("@PASSWORD_USER", password);
                    command.Connection = connection;
                    string commandString = command.SqlCommandToString();
                    connection.Open();

                    var reader = await command.ExecuteReaderAsync();
                    await reader.ReadAsync();
                    if (reader.HasRows)
                    {
                        result.Id = Convert.ToInt32(reader["ID_USER"]);
                        result.Username = reader["USERNAME"].ToString() ?? null;
                        result.Password = reader["PASSWORD_USER"].ToString() ?? null;
                        result.Role = reader["ROLE_USE"].ToString() ?? null;
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
