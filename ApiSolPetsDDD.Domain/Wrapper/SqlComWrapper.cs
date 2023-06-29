using APISolPets.Domain.Interfaces;
using ApiSolPetsDDD.Domain.Interfaces;
using ApiSolPetsDDD.Domain.Wrapper;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace ApiSolPetsDDD.Domain.Factory
{
    public class SqlComWrapper : ISqlComWrapper
    {
        private readonly SqlCommand SqlCom;
        private bool disposedValueConnection;

        public SqlComWrapper(string textCommand)
        {
            SqlCom = new SqlCommand(textCommand);
        }

        public CommandType CommandType
        {
            get { return SqlCom.CommandType; }
            set { SqlCom.CommandType = value; }
        }

        public IDbConnection Connection
        {
            get { return SqlCom.Connection; }
            set { SqlCom.Connection = (SqlConnection)value; }
        }

        public async Task<ISqlDataReaderWrapper> ExecuteReaderAsync()
        {
            var reader = await SqlCom.ExecuteReaderAsync();
            return new SqlDRWrapper(reader);
        }

        public async Task<dynamic> ExecuteNonQueryAsync()
        {
            return await SqlCom.ExecuteNonQueryAsync();
        }

        public async Task<dynamic> ExecuteScalarAsync()
        {
            return await SqlCom.ExecuteScalarAsync();
        }

        public SqlParameterCollection Parameters { get { return SqlCom.Parameters; } }

        public string CommandText
        {
            get { return SqlCom.CommandText; }
            set { SqlCom.CommandText = value; }
        }

        protected virtual void DisposeConnection(bool disposingConnection)
        {
            if (!disposedValueConnection)
            {
                if (disposingConnection)
                {
                    SqlCom.Connection.Dispose();
                    SqlCom.Dispose();
                }
                disposedValueConnection = true;
            }
        }

        public void Dispose()
        {
            DisposeConnection(disposingConnection: true);
            GC.SuppressFinalize(this);
        }
    }
}
