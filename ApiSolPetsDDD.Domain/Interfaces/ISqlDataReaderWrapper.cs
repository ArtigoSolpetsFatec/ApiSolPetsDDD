using System.Threading.Tasks;

namespace APISolPets.Domain.Interfaces
{
    public interface ISqlDataReaderWrapper
    {
        Task<bool> ReadAsync();
        bool HasRows { get; }
        object this[string name] { get; }
    }
}
