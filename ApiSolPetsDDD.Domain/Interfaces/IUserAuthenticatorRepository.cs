using ApiSolPetsDDD.Domain.Model;
using System.Threading.Tasks;

namespace ApiSolPetsDDD.Domain.Interfaces
{
    public interface IUserAuthenticatorRepository
    {
        Task<UserAuthenticator> GetUser(string username, string password);
    }
}
