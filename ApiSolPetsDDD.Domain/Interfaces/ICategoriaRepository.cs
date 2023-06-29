using ApiSolPetsDDD.Domain.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApiSolPetsDDD.Domain.Interfaces
{
    public interface ICategoriaRepository
    {
        Task<Categoria> GetCategoriaById(int idCategoria);
        Task<Categoria> GetCategoriaByName(string nomeCategoria);
        Task<List<Categoria>> GetAllCategorias();
        Task<Categoria> CadastrarCategoria(Categoria categoria);
        Task<int> AtualizarInfoCategoria(Categoria categoria);
        Task<int> ExcluirCategoria(int idCategoria);
    }
}
