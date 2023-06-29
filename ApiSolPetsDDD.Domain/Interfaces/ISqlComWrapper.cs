using APISolPets.Domain.Interfaces;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace ApiSolPetsDDD.Domain.Interfaces
{
    public interface ISqlComWrapper : IDisposable
    {
        CommandType CommandType { get; set; }
        IDbConnection Connection { get; set; }
        Task<ISqlDataReaderWrapper> ExecuteReaderAsync();
        Task<dynamic> ExecuteScalarAsync();
        Task<dynamic> ExecuteNonQueryAsync();
        SqlParameterCollection Parameters { get; }
        string CommandText { get; set; }
    }
}
