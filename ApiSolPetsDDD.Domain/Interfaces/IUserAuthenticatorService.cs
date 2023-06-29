using ApiSolPetsDDD.Domain.Model;
using System.Threading.Tasks;

namespace ApiSolPetsDDD.Domain.Interfaces
{
    public interface IUserAuthenticatorService
    {
        Task<UserAuthenticator> GetToken(string username, string password);
    }
}
