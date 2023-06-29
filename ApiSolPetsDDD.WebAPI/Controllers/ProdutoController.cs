using ApiSolPetsDDD.Domain.Exceptions;
using ApiSolPetsDDD.Domain.Interfaces;
using ApiSolPetsDDD.Domain.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace ApiSolPetsDDD.WebAPI.Controllers
{
    [Produces("application/json")]
    public class ProdutoController : Controller
    {
        private readonly IProdutoService produtoService;

        public ProdutoController(IProdutoService produtoService)
        {
            this.produtoService = produtoService;
        }

        [SwaggerOperation(Summary = "Realiza a busca de produto por id")]
        [SwaggerResponse(200, "Executado com sucesso!", typeof(Produto))]
        [SwaggerResponse(204, "Produto não encontrado!")]
        [SwaggerResponse(401, "Requisição requer autenticação")]
        [SwaggerResponse(412, "Exceção de negócio!")]
        [SwaggerResponse(500, "Erro Interno!")]
        [HttpGet]
        [Route("/produto/codigoProduto/{idProduto}")]
        [Authorize]
        public async Task<IActionResult> GetProdutoById(int idProduto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed, ModelState.Values.Select(
                        x => x.Errors.Select(y => y.Exception != null ? y.Exception.Message : y.ErrorMessage)));
                }

                var result = await produtoService.GetProdutoById(idProduto);
                if (result == null)
                    return NoContent();
                return Ok(result);
            }
            catch (ExcecaoNegocio ex)
            {
                return StatusCode((int)HttpStatusCode.PreconditionFailed, $"{ex.Message}");
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, $"Erro Interno : {ex.Message}");
            }
        }

        [SwaggerOperation(Summary = "Realiza a busca de produto por ISBN")]
        [SwaggerResponse(200, "Executado com sucesso!", typeof(Produto))]
        [SwaggerResponse(204, "Produto não encontrado!")]
        [SwaggerResponse(401, "Requisição requer autenticação")]
        [SwaggerResponse(412, "Exceção de negócio!")]
        [SwaggerResponse(500, "Erro Interno!")]
        [HttpGet]
        [Route("/produto/isbn/{isbn}")]
        [Authorize]
        public async Task<IActionResult> GetProdutoByISBN(string isbn)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed, ModelState.Values.Select(
                        x => x.Errors.Select(y => y.Exception != null ? y.Exception.Message : y.ErrorMessage)));
                }

                var result = await produtoService.GetProdutoByISBN(isbn);
                if (result == null || result.IdProduto == 0)
                    return NoContent();
                return Ok(result);
            }
            catch (ExcecaoNegocio ex)
            {
                return StatusCode((int)HttpStatusCode.PreconditionFailed, $"{ex.Message}");
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, $"Erro Interno : {ex.Message}");
            }
        }

        [SwaggerOperation(Summary = "Realiza a busca de produtos por nome")]
        [SwaggerResponse(200, "Executado com sucesso!", typeof(List<Produto>))]
        [SwaggerResponse(204, "Produtos não encontrados!")]
        [SwaggerResponse(401, "Requisição requer autenticação")]
        [SwaggerResponse(412, "Exceção de negócio!")]
        [SwaggerResponse(500, "Erro Interno!")]
        [HttpGet]
        [Route("/produtos/nomeProduto/{nomeProduto}")]
        [Authorize]
        public async Task<IActionResult> GetTop10ProdutosByNome(string nomeProduto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed, ModelState.Values.Select(
                        x => x.Errors.Select(y => y.Exception != null ? y.Exception.Message : y.ErrorMessage)));
                }

                var result = await produtoService.GetTop10ProdutosByNome(nomeProduto);
                if (result == null || result.Count == 0)
                    return NoContent();
                return Ok(result);
            }
            catch (ExcecaoNegocio ex)
            {
                return StatusCode((int)HttpStatusCode.PreconditionFailed, $"{ex.Message}");
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, $"Erro Interno : {ex.Message}");
            }
        }

        [SwaggerOperation(Summary = "Realiza a busca de produtos próximos ao vencimento")]
        [SwaggerResponse(200, "Executado com sucesso!", typeof(List<Produto>))]
        [SwaggerResponse(204, "Produtos não encontrados!")]
        [SwaggerResponse(401, "Requisição requer autenticação")]
        [SwaggerResponse(412, "Exceção de negócio!")]
        [SwaggerResponse(500, "Erro Interno!")]
        [HttpGet]
        [Route("/produtos/produtos-vencimento")]
        [Authorize]
        public async Task<IActionResult> GetProdutosAVencer()
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed, ModelState.Values.Select(
                        x => x.Errors.Select(y => y.Exception != null ? y.Exception.Message : y.ErrorMessage)));
                }

                var result = await produtoService.GetProdutosAVencer();
                if (result == null || result.Count == 0)
                    return NoContent();
                return Ok(result);
            }
            catch (ExcecaoNegocio ex)
            {
                return StatusCode((int)HttpStatusCode.PreconditionFailed, $"{ex.Message}");
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, $"Erro Interno : {ex.Message}");
            }
        }

        [SwaggerOperation(Summary = "Realiza a busca de produtos por categoria")]
        [SwaggerResponse(200, "Executado com sucesso!", typeof(List<Produto>))]
        [SwaggerResponse(204, "Produtos não encontrados!")]
        [SwaggerResponse(401, "Requisição requer autenticação")]
        [SwaggerResponse(412, "Exceção de negócio!")]
        [SwaggerResponse(500, "Erro Interno!")]
        [HttpGet]
        [Route("/produtos/codigoCategoria/{idCategoria}")]
        [Authorize]
        public async Task<IActionResult> GetTop10ProdutosByCategoria(int idCategoria)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed, ModelState.Values.Select(
                        x => x.Errors.Select(y => y.Exception != null ? y.Exception.Message : y.ErrorMessage)));
                }

                var result = await produtoService.GetTop10ProdutosByCategoria(idCategoria);
                if (result == null || result.Count == 0)
                    return NoContent();
                return Ok(result);
            }
            catch (ExcecaoNegocio ex)
            {
                return StatusCode((int)HttpStatusCode.PreconditionFailed, $"{ex.Message}");
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, $"Erro Interno : {ex.Message}");
            }
        }

        [SwaggerOperation(Summary = "Cadastra um produto")]
        [SwaggerResponse(200, "Executado com sucesso!", typeof(Produto))]
        [SwaggerResponse(204, "Produto não encontrado!")]
        [SwaggerResponse(401, "Requisição requer autenticação")]
        [SwaggerResponse(412, "Exceção de negócio!")]
        [SwaggerResponse(500, "Erro Interno!")]
        [HttpPost]
        [Route("/produto")]
        [Authorize]
        public async Task<IActionResult> PostProduto([FromBody] Produto produto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed, ModelState.Values.Select(
                        x => x.Errors.Select(y => y.Exception != null ? y.Exception.Message : y.ErrorMessage)));
                }

                var result = await produtoService.PostProduto(produto);
                if (result == null)
                    return NoContent();
                return Ok(result);
            }
            catch (ExcecaoNegocio ex)
            {
                return StatusCode((int)HttpStatusCode.PreconditionFailed, $"{ex.Message}");
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, $"Erro Interno : {ex.Message}");
            }
        }

        [SwaggerOperation(Summary = "Atualiza os dados de um produto")]
        [SwaggerResponse(200, "Executado com sucesso!", typeof(string))]
        [SwaggerResponse(204, "Produto não encontrado!")]
        [SwaggerResponse(401, "Requisição requer autenticação")]
        [SwaggerResponse(412, "Exceção de negócio!")]
        [SwaggerResponse(500, "Erro Interno!")]
        [HttpPut]
        [Route("/produto/atualiza-produto")]
        [Authorize]
        public async Task<IActionResult> PutProduto([FromBody] Produto produto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed, ModelState.Values.Select(
                        x => x.Errors.Select(y => y.Exception != null ? y.Exception.Message : y.ErrorMessage)));
                }

                var result = await produtoService.PutProduto(produto);
                if (result == 0)
                    return NoContent();
                return Ok("Dados atualizados com sucesso!");
            }
            catch (ExcecaoNegocio ex)
            {
                return StatusCode((int)HttpStatusCode.PreconditionFailed, $"{ex.Message}");
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, $"Erro Interno : {ex.Message}");
            }
        }

        [SwaggerOperation(Summary = "Atualiza a quantidae de um produto")]
        [SwaggerResponse(200, "Executado com sucesso!", typeof(string))]
        [SwaggerResponse(204, "Produto não encontrado!")]
        [SwaggerResponse(401, "Requisição requer autenticação")]
        [SwaggerResponse(412, "Exceção de negócio!")]
        [SwaggerResponse(500, "Erro Interno!")]
        [HttpPatch]
        [Route("/produto/atualiza-qtde-produto/{quantidade}/produto/{idProduto}/soma-qtde/{soma}")]
        [Authorize]
        public async Task<IActionResult> PatchQtdeProduto(int quantidade, int idProduto, bool soma)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed, ModelState.Values.Select(
                        x => x.Errors.Select(y => y.Exception != null ? y.Exception.Message : y.ErrorMessage)));
                }

                var result = await produtoService.PatchQtdeProduto(quantidade, idProduto, soma);
                if (result == 0)
                    return NoContent();
                return Ok("Quantidade atualizada com sucesso!");
            }
            catch (ExcecaoNegocio ex)
            {
                return StatusCode((int)HttpStatusCode.PreconditionFailed, $"{ex.Message}");
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, $"Erro Interno : {ex.Message}");
            }
        }

        [SwaggerOperation(Summary = "Exclui os dados de um produto")]
        [SwaggerResponse(200, "Executado com sucesso!", typeof(string))]
        [SwaggerResponse(204, "Produto não encontrado!")]
        [SwaggerResponse(401, "Requisição requer autenticação")]
        [SwaggerResponse(412, "Exceção de negócio!")]
        [SwaggerResponse(500, "Erro Interno!")]
        [HttpDelete]
        [Route("/produto/exclui-produto/{idProduto}")]
        [Authorize]
        public async Task<IActionResult> DeleteProduto(int idProduto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed, ModelState.Values.Select(
                        x => x.Errors.Select(y => y.Exception != null ? y.Exception.Message : y.ErrorMessage)));
                }

                var result = await produtoService.DeleteProduto(idProduto);
                if (result == 0)
                    return NoContent();
                return Ok("Dados excluídos com sucesso!");
            }
            catch (ExcecaoNegocio ex)
            {
                return StatusCode((int)HttpStatusCode.PreconditionFailed, $"{ex.Message}");
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, $"Erro Interno : {ex.Message}");
            }
        }
    }
}
