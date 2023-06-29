using ApiSolPetsDDD.Domain.Model;
using System.Threading.Tasks;

namespace ApiSolPetsDDD.Domain.Interfaces
{
    public interface IFuncionarioRepository
    {
        Task<Funcionario> GetInfoFuncionario(int idFuncionario);
        Task<Funcionario> GetFuncionarioByName(string nomeFuncionario);
        Task<Funcionario> GetFuncionarioByIdLogin(int idLogin);
        Task<Funcionario> CadastrarFuncionario(Funcionario funcionario);
        Task<int> AtualizarFuncionario(Funcionario funcionario);
        Task<int> ExcluirFuncionario(int idFuncionario);
    }
}
