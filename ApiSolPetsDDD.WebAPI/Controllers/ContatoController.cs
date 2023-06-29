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
    public class ContatoController : Controller
    {
        private readonly IContatoService contatoService;

        public ContatoController(IContatoService contatoService)
        {
            this.contatoService = contatoService;
        }

        [SwaggerOperation(Summary = "Efetua cadastro de contato")]
        [SwaggerResponse(200, "Executado com sucesso!", typeof(List<Contato>))]
        [SwaggerResponse(204, "Contato não encontrado!")]
        [SwaggerResponse(401, "Requisição requer autenticação")]
        [SwaggerResponse(412, "Exceção de negócio!")]
        [SwaggerResponse(500, "Erro Interno!")]
        [HttpPost]
        [Route("contato/cadastrar-contato")]
        [Authorize]
        public async Task<IActionResult> PostContato([FromBody] Contato contato)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed, ModelState.Values.Select(
                        x => x.Errors.Select(y => y.Exception != null ? y.Exception.Message : y.ErrorMessage)));
                }
                var result = await contatoService.PostContato(contato);

                if (result == null)
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

        [SwaggerOperation(Summary = "Atualiza cadastro de contato")]
        [SwaggerResponse(200, "Executado com sucesso!", typeof(int))]
        [SwaggerResponse(401, "Requisição requer autenticação")]
        [SwaggerResponse(412, "Exceção de negócio!")]
        [SwaggerResponse(500, "Erro Interno!")]
        [HttpPut]
        [Route("contato/atualiza-contato")]
        [Authorize]
        public async Task<IActionResult> PutContato([FromBody] Contato contato)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed, ModelState.Values.Select(
                        x => x.Errors.Select(y => y.Exception != null ? y.Exception.Message : y.ErrorMessage)));
                }
                var result = await contatoService.PutContato(contato);
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

        [SwaggerOperation(Summary = "Exclui contato")]
        [SwaggerResponse(200, "Excluído com sucesso!", typeof(int))]
        [SwaggerResponse(401, "Requisição requer autenticação")]
        [SwaggerResponse(412, "Exceção de negócio!")]
        [SwaggerResponse(500, "Erro Interno!")]
        [HttpDelete]
        [Route("contato/exclui-contato/{idContato}/cliente/{idCliente}/fornecedor/{idFornecedor}")]
        [Authorize]
        public async Task<IActionResult> DeleteContato(int idContato, int idCliente, int idFornecedor)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed, ModelState.Values.Select(
                        x => x.Errors.Select(y => y.Exception != null ? y.Exception.Message : y.ErrorMessage)));
                }
                var result = await contatoService.DeleteContato(idContato, idCliente, idFornecedor);
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
