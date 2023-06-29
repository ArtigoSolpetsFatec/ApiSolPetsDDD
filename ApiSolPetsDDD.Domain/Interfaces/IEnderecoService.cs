using ApiSolPetsDDD.Domain.Model;
using System.Threading.Tasks;

namespace ApiSolPetsDDD.Domain.Interfaces
{
    public interface IEnderecoService
    {
        Task<Endereco> PostEndereco(Endereco endereco, int idCliente);
        Task<int> PutEndereco(Endereco enderecoCliente);
        Task<int> DeleteEndereco(int idCliente, int idEndereco);
    }
}
