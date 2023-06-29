using ApiSolPetsDDD.Domain.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApiSolPetsDDD.Domain.Interfaces
{
    public interface IProdutoRepository
    {
        Task<Produto> GetProdutoById(int idProduto);
        Task<Produto> GetProdutoByISBN(string isbn);
        Task<List<Produto>> GetTop10ProdutosByNome(string nomeProduto);
        Task<List<Produto>> GetProdutosAVencer();
        Task<List<Produto>> GetTop10ProdutosByCategoria(int idCategoria);
        Task<Produto> CadastrarProduto(Produto produto);
        Task<int> AtualizarInfoProduto(Produto produto);
        Task<int> AtualizarQtdeProduto(int quantidade, int idProduto, bool soma);
        Task<int> ExcluirProduto(int idProduto);
        Task<List<Produto>> GetCustoTotalEstoque();
    }
}
