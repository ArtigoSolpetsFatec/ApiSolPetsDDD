using APISolPets.Domain.Extensions;
using APISolPets.Domain.Interfaces;
using System.Data;
using System.Data.SqlClient;

namespace APISolPets.Infra.Data.DataContext
{
    public class DbConnectionFactory : IDbConFactory
    {
        public IDbConnection CreateConnection()
        {
            return new SqlConnection(EnviromentsConfigurations.BdSolPets);
        }
    }
}
