using APISolPets.Domain.Extensions;
using ApiSolPetsDDD.Domain.Exceptions;
using ApiSolPetsDDD.Domain.Interfaces;
using ApiSolPetsDDD.Domain.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApiSolPetsDDD.Application.Services
{
    public class ProdutoService : IProdutoService
    {
        private readonly IProdutoRepository produtoRepository;

        public ProdutoService(IProdutoRepository produtoRepository)
        {
            this.produtoRepository = produtoRepository;
        }

        public async Task<Produto> GetProdutoById(int idProduto)
        {
            try
            {
                if (idProduto == 0)
                    throw new ExcecaoNegocio("[Exceção de Negócio] - Obrigatório informar o id do produto!");

                var result = await produtoRepository.GetProdutoById(idProduto);

                return result;
            }
            catch (ExcecaoNegocio ex)
            {
                throw new ExcecaoNegocio(ex.Message);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Produto> GetProdutoByISBN(string isbn)
        {
            try
            {
                if (string.IsNullOrEmpty(isbn))
                    throw new ExcecaoNegocio("[Exceção de Negócio] - Obrigatório informar o isbn do produto!");

                var result = await produtoRepository.GetProdutoByISBN(isbn);

                return result;
            }
            catch (ExcecaoNegocio ex)
            {
                throw new ExcecaoNegocio(ex.Message);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<Produto>> GetTop10ProdutosByNome(string nomeProduto)
        {
            try
            {
                if (string.IsNullOrEmpty(nomeProduto))
                    throw new ExcecaoNegocio("[Exceção de Negócio] - Obrigatório informar o nome do produto!");

                var result = await produtoRepository.GetTop10ProdutosByNome(nomeProduto);

                return result;
            }
            catch (ExcecaoNegocio ex)
            {
                throw new ExcecaoNegocio(ex.Message);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<Produto>> GetProdutosAVencer()
        {
            try
            {
                var result = await produtoRepository.GetProdutosAVencer();
                return result;
            }
            catch (ExcecaoNegocio ex)
            {
                throw new ExcecaoNegocio(ex.Message);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<Produto>> GetTop10ProdutosByCategoria(int idCategoria)
        {
            try
            {
                if (idCategoria == 0)
                    throw new ExcecaoNegocio("[Exceção de Negócio] - Obrigatório informar o id da categoria!");

                var result = await produtoRepository.GetTop10ProdutosByCategoria(idCategoria);

                return result;
            }
            catch (ExcecaoNegocio ex)
            {
                throw new ExcecaoNegocio(ex.Message);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Produto> PostProduto(Produto produto)
        {
            var dataMaxima = DateTime.MaxValue;
            var dataAtual = DateTime.Now;
            try
            {
                produto.IdCategoria = produto.IdCategoria > 0 ? produto.IdCategoria :
                    throw new ExcecaoNegocio("[Exceção de Negócio] - Obrigatório informar o código da categoria!");
                produto.Fornecedor.IdFornecedor = produto.Fornecedor.IdFornecedor > 0 ? produto.Fornecedor.IdFornecedor :
                    throw new ExcecaoNegocio("[Exceção de Negócio] - Obrigatório informar o código do fornecedor!");
                produto.IsbnProduto = !string.IsNullOrEmpty(produto.IsbnProduto) ? produto.IsbnProduto :
                    throw new ExcecaoNegocio("[Exceção de Negócio] - Obrigatório informar o isbn do produto!");
                produto.NomeProduto = !string.IsNullOrEmpty(produto.NomeProduto) ? produto.NomeProduto.ToUpper().Trim() :
                    throw new ExcecaoNegocio("[Exceção de Negócio] - Obrigatório informar o nome do produto!");
                produto.MarcaProduto = !string.IsNullOrEmpty(produto.MarcaProduto) ? produto.MarcaProduto.ToUpper().Trim() :
                    throw new ExcecaoNegocio("[Exceção de Negócio] - Obrigatório informar a marca do produto!");
                produto.ValorUnitarioCusto = produto.ValorUnitarioCusto > 0.0 ? produto.ValorUnitarioCusto :
                    throw new ExcecaoNegocio("[Exceção de Negócio] - Obrigatório informar o valor unitário de custo do produto!");
                produto.ValorUnitarioVenda = produto.ValorUnitarioVenda > 0.0 && !produto.ValorVendaEditavel ? produto.ValorUnitarioVenda :
                    produto.ValorUnitarioVenda >= 0.0 && produto.ValorVendaEditavel ? produto.ValorUnitarioVenda :
                    throw new ExcecaoNegocio("[Exceção de Negócio] - Obrigatório informar o valor unitário de venda do produto!");
                produto.QtdeEstoque = produto.QtdeEstoque > 0 ? produto.QtdeEstoque : 0;
                if (produto.DataValidade == dataMaxima)
                    throw new ExcecaoData("[Exceção de Data] - Data de validade informada é inválida!");
                if (produto.DataValidade == dataAtual || produto.DataValidade < dataAtual)
                    throw new ExcecaoData("[Exceção de Data] - Data de validade informada não pode ser atual, nem passada!");
                if (produto.QtdeEstoque < 0)
                    throw new ExcecaoNegocio("[Exceção de Negócio] - Obrigatório informar uma quantidade maior ou igual a 0!");

                var result = await produtoRepository.CadastrarProduto(produto);

                return result;
            }
            catch (ExcecaoNegocio ex)
            {
                throw new ExcecaoNegocio(ex.Message);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<int> PutProduto(Produto produto)
        {
            var dataMaxima = DateTime.MaxValue;
            var dataAtual = DateTime.Now;
            try
            {
                produto.IdProduto = produto.IdProduto > 0 ? produto.IdProduto :
                    throw new ExcecaoNegocio("[Exceção de Negócio] - Obrigatório informar o id do produto!");
                produto.IdCategoria = produto.IdCategoria > 0 ? produto.IdCategoria :
                    throw new ExcecaoNegocio("[Exceção de Negócio] - Obrigatório informar o id da categoria!");
                produto.Fornecedor.IdFornecedor = produto.Fornecedor.IdFornecedor > 0 ? produto.Fornecedor.IdFornecedor :
                    throw new ExcecaoNegocio("[Exceção de Negócio] - Obrigatório informar o id do fornecedor!");
                produto.IsbnProduto = !string.IsNullOrEmpty(produto.IsbnProduto) ? produto.IsbnProduto :
                    throw new ExcecaoNegocio("[Exceção de Negócio] - Obrigatório informar o isbn do produto!");
                produto.NomeProduto = !string.IsNullOrEmpty(produto.NomeProduto) ? produto.NomeProduto.ToUpper() :
                    throw new ExcecaoNegocio("[Exceção de Negócio] - Obrigatório informar o isbn do produto!");
                produto.NomeProduto = produto.NomeProduto.RemoveAcentos();
                produto.MarcaProduto = !string.IsNullOrEmpty(produto.MarcaProduto) ? produto.MarcaProduto.ToUpper() :
                    throw new ExcecaoNegocio("[Exceção de Negócio] - Obrigatório informar a marca do produto!");
                produto.MarcaProduto = produto.MarcaProduto.RemoveAcentos();
                produto.ValorUnitarioCusto = produto.ValorUnitarioCusto > 0.0 ? produto.ValorUnitarioCusto :
                    throw new ExcecaoNegocio("[Exceção de Negócio] - Obrigatório informar o valor unitário de custo do produto!");
                produto.ValorUnitarioVenda = produto.ValorUnitarioVenda > 0.0 ? produto.ValorUnitarioVenda :
                    throw new ExcecaoNegocio("[Exceção de Negócio] - Obrigatório informar o valor unitário de venda do produto!");
                produto.QtdeEstoque = produto.QtdeEstoque > 0 ? produto.QtdeEstoque : 0;
                if (produto.DataValidade == dataMaxima)
                    throw new ExcecaoData("[Exceção de Data] - Data de validade informada é inválida!");
                if (produto.DataValidade == dataAtual || produto.DataValidade < dataAtual)
                    throw new ExcecaoData("[Exceção de Data] - Data de validade informada não pode ser atual, nem passada!");
                if (produto.QtdeEstoque < 0)
                    throw new ExcecaoNegocio("[Exceção de Negócio] - Obrigatório informar uma quantidade maior ou igual a 0!");

                var result = await produtoRepository.AtualizarInfoProduto(produto);

                return result;
            }
            catch (ExcecaoNegocio ex)
            {
                throw new ExcecaoNegocio(ex.Message);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<int> PatchQtdeProduto(int quantidade, int idProduto, bool soma)
        {
            try
            {
                if (idProduto == 0)
                    throw new ExcecaoNegocio("[Exceção de Negócio] - Obrigatório informar o id do produto!");
                if (quantidade < 0)
                    throw new ExcecaoNegocio("[Exceção de Negócio] - Obrigatório informar uma quantidade maior ou igual a 0!");

                var result = await produtoRepository.AtualizarQtdeProduto(quantidade, idProduto, soma);

                return result;
            }
            catch (ExcecaoNegocio ex)
            {
                throw new ExcecaoNegocio(ex.Message);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<int> DeleteProduto(int idProduto)
        {
            try
            {
                if (idProduto == 0)
                    throw new ExcecaoNegocio("[Exceção de Negócio] - Obrigatório informar o id do produto!");

                var result = await produtoRepository.ExcluirProduto(idProduto);

                return result;
            }
            catch (ExcecaoNegocio ex)
            {
                throw new ExcecaoNegocio(ex.Message);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
