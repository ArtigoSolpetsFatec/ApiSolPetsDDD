using ApiSolPetsDDD.Domain.Model;
using System.Threading.Tasks;

namespace ApiSolPetsDDD.Domain.Interfaces
{
    public interface ILoginRepository
    {
        Task<Login> GetLogin(string email);
        Task<Login> GetInfoLogin(int idLogin);
        Task<Login> CriarLogin(Login login);
        Task<int> AtualizarLogin(Login login);
        Task<int> ExcluirLogin(int idLogin);
    }
}
