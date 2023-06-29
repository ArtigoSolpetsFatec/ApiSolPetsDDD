using ApiSolPetsDDD.Domain.Model;
using System.Threading.Tasks;

namespace ApiSolPetsDDD.Domain.Interfaces
{
    public interface ILoginService
    {
        Task<Login> GetLogin(string email, string senha);
        Task<Login> GetLoginById(int idLogin);
        Task<Login> PostLogin(string nomeFuncionario, bool isAdmin, string senha);
        Task<int> PutLogin(int idLogin, bool isAdmin);
        Task<int> DeleteLogin(int idLogin);
    }
}
