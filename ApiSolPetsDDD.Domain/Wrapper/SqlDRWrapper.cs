using APISolPets.Domain.Interfaces;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace ApiSolPetsDDD.Domain.Wrapper
{
    public class SqlDRWrapper : ISqlDataReaderWrapper
    {
        private readonly SqlDataReader sqlDataReader;

        public SqlDRWrapper(SqlDataReader sqlDataReader)
        {
            this.sqlDataReader = sqlDataReader;
        }

        public object this[string name] => sqlDataReader[name];
        public bool HasRows => sqlDataReader.HasRows;
        public async Task<bool> ReadAsync() => await sqlDataReader.ReadAsync();
    }
}
