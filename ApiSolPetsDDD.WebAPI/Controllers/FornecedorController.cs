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
    public class FornecedorController : Controller
    {
        private readonly IFornecedorService fornecedorService;

        public FornecedorController(IFornecedorService fornecedorService)
        {
            this.fornecedorService = fornecedorService;
        }

        [SwaggerOperation(Summary = "Efetua cadastro de fornecedor")]
        [SwaggerResponse(200, "Executado com sucesso!", typeof(Fornecedor))]
        [SwaggerResponse(204, "Fornecedor não encontrado!")]
        [SwaggerResponse(401, "Requisição requer autenticação")]
        [SwaggerResponse(412, "Exceção de negócio!")]
        [SwaggerResponse(500, "Erro Interno!")]
        [HttpPost]
        [Route("/fornecedor")]
        [Authorize]
        public async Task<IActionResult> PostFornecedor([FromBody] Fornecedor fornecedor)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed, ModelState.Values.Select(
                        x => x.Errors.Select(y => y.Exception != null ? y.Exception.Message : y.ErrorMessage)));
                }

                var result = await fornecedorService.PostFornecedor(fornecedor);
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
                return StatusCode((int)HttpStatusCode.InternalServerError, $"Erro : {ex.Message}");
            }
        }

        [SwaggerOperation(Summary = "Efetua busca de fornecedor por Id")]
        [SwaggerResponse(200, "Executado com sucesso!", typeof(Fornecedor))]
        [SwaggerResponse(204, "Fornecedor não encontrado!")]
        [SwaggerResponse(401, "Requisição requer autenticação")]
        [SwaggerResponse(412, "Exceção de negócio!")]
        [SwaggerResponse(500, "Erro Interno!")]
        [HttpGet]
        [Route("/fornecedor/{idFornecedor}")]
        [Authorize]
        public async Task<IActionResult> GetFornecedorById(int idFornecedor)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed, ModelState.Values.Select(
                        x => x.Errors.Select(y => y.Exception != null ? y.Exception.Message : y.ErrorMessage)));
                }

                var result = await fornecedorService.GetInfoFornecedor(idFornecedor);
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
                return StatusCode((int)HttpStatusCode.InternalServerError, $"Erro : {ex.Message}");
            }
        }

        [SwaggerOperation(Summary = "Efetua busca de fornecedor por Id")]
        [SwaggerResponse(200, "Executado com sucesso!", typeof(List<Fornecedor>))]
        [SwaggerResponse(204, "Fornecedor não encontrado!")]
        [SwaggerResponse(401, "Requisição requer autenticação")]
        [SwaggerResponse(412, "Exceção de negócio!")]
        [SwaggerResponse(500, "Erro Interno!")]
        [HttpGet]
        [Route("/fornecedores")]
        [Authorize]
        public async Task<IActionResult> GetFornecedores()
        {
            try
            {

                var result = await fornecedorService.GetFornecedores();
                if (result.Count == 0)
                    return NoContent();

                return Ok(result);
            }
            catch (ExcecaoNegocio ex)
            {
                return StatusCode((int)HttpStatusCode.PreconditionFailed, $"{ex.Message}");
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, $"Erro : {ex.Message}");
            }
        }

        [SwaggerOperation(Summary = "Efetua busca de fornecedor por CNPJ")]
        [SwaggerResponse(200, "Executado com sucesso!", typeof(List<Fornecedor>))]
        [SwaggerResponse(204, "Fornecedor não encontrado!")]
        [SwaggerResponse(401, "Requisição requer autenticação")]
        [SwaggerResponse(412, "Exceção de negócio!")]
        [SwaggerResponse(500, "Erro Interno!")]
        [HttpGet]
        [Route("/fornecedor/{cnpjFornecedor}/cnpj")]
        [Authorize]
        public async Task<IActionResult> GetFornecedorByCnpj(string cnpjFornecedor)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed, ModelState.Values.Select(
                        x => x.Errors.Select(y => y.Exception != null ? y.Exception.Message : y.ErrorMessage)));
                }

                var result = await fornecedorService.GetInfoFornecedorByCnpj(cnpjFornecedor);
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
                return StatusCode((int)HttpStatusCode.InternalServerError, $"Erro : {ex.Message}");
            }
        }

        [SwaggerOperation(Summary = "Atualiza cadastro de fornecedor")]
        [SwaggerResponse(200, "Executado com sucesso!", typeof(int))]
        [SwaggerResponse(204, "Fornecedor não encontrado!")]
        [SwaggerResponse(401, "Requisição requer autenticação")]
        [SwaggerResponse(412, "Exceção de negócio!")]
        [SwaggerResponse(500, "Erro Interno!")]
        [HttpPut]
        [Route("/fornecedor/atualiza-fornecedor")]
        [Authorize]
        public async Task<IActionResult> PutFornecedor([FromBody] Fornecedor fornecedor)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed, ModelState.Values.Select(
                        x => x.Errors.Select(y => y.Exception != null ? y.Exception.Message : y.ErrorMessage)));
                }

                var result = await fornecedorService.PutFornecedor(fornecedor);
                if (result == 0)
                    return NoContent();

                return Ok(result);
            }
            catch (ExcecaoNegocio ex)
            {
                return StatusCode((int)HttpStatusCode.PreconditionFailed, $"{ex.Message}");
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, $"Erro : {ex.Message}");
            }
        }

        [SwaggerOperation(Summary = "Exclui cadastro de fornecedor")]
        [SwaggerResponse(200, "Excluído com sucesso!", typeof(int))]
        [SwaggerResponse(401, "Requisição requer autenticação")]
        [SwaggerResponse(412, "Exceção de negócio!")]
        [SwaggerResponse(500, "Erro Interno!")]
        [HttpDelete]
        [Route("/fornecedor/excluir-fornecedor/{idfornecedor}")]
        [Authorize]
        public async Task<IActionResult> DeleteFornecedor(int idfornecedor)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed, ModelState.Values.Select(
                        x => x.Errors.Select(y => y.Exception != null ? y.Exception.Message : y.ErrorMessage)));
                }
                var result = await fornecedorService.DeleteFornecedor(idfornecedor);
                if (result == 0)
                    return NoContent();

                return Ok(result);
            }
            catch (ExcecaoNegocio ex)
            {
                return StatusCode((int)HttpStatusCode.PreconditionFailed, $" {ex.Message}");
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, $"Erro : {ex.Message}");
            }
        }
    }
}
