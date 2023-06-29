using ApiSolPetsDDD.Domain.Model;
using System.Threading.Tasks;

namespace ApiSolPetsDDD.Domain.Interfaces
{
    public interface IFuncionarioService
    {
        Task<Funcionario> GetFuncionarioById(int idFuncionario);
        Task<Funcionario> GetFuncionarioByNome(string nomeFuncionario);
        Task<Funcionario> GetFuncionarioByIdLogin(int idLogin);
        Task<Funcionario> PostFuncionario(Funcionario funcionario);
        Task<int> PutFuncionario(Funcionario funcionario);
        Task<int> DeleteFuncionario(int idFuncionario);
    }
}
