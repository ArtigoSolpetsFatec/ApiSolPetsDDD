using ApiSolPetsDDD.Domain.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApiSolPetsDDD.Domain.Interfaces
{
    public interface ICategoriaService
    {
        Task<Categoria> GetCategoriaByName(string nomeCategoria);
        Task<Categoria> GetCategoriaById(int idCategoria);
        Task<List<Categoria>> GetAllCategorias();
        Task<Categoria> PostCategoria(Categoria categoria);
        Task<int> PutCategoria(Categoria categoria);
        Task<int> DeleteCategoria(int idCategoria);
    }
}
