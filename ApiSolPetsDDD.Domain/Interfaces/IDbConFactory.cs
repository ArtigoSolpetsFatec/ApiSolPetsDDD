using System.Data;

namespace APISolPets.Domain.Interfaces
{
    public interface IDbConFactory
    {
        IDbConnection CreateConnection();
    }
}
