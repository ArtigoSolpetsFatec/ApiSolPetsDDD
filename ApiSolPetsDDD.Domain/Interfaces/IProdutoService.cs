using ApiSolPetsDDD.Domain.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApiSolPetsDDD.Domain.Interfaces
{
    public interface IProdutoService
    {
        Task<Produto> GetProdutoById(int idProduto);
        Task<Produto> GetProdutoByISBN(string isbn);
        Task<List<Produto>> GetTop10ProdutosByNome(string nomeProduto);
        Task<List<Produto>> GetProdutosAVencer();
        Task<List<Produto>> GetTop10ProdutosByCategoria(int idCategoria);
        Task<Produto> PostProduto(Produto produto);
        Task<int> PutProduto(Produto produto);
        Task<int> PatchQtdeProduto(int quantidade, int idProduto, bool soma);
        Task<int> DeleteProduto(int idProduto);
    }
}
