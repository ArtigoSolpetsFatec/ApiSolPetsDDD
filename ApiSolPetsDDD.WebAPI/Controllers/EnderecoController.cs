using ApiSolPetsDDD.Domain.Exceptions;
using ApiSolPetsDDD.Domain.Interfaces;
using ApiSolPetsDDD.Domain.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace ApiSolPetsDDD.WebAPI.Controllers
{
    [Produces("application/json")]
    public class EnderecoController : Controller
    {
        private readonly IEnderecoService enderecoService;

        public EnderecoController(IEnderecoService enderecoService)
        {
            this.enderecoService = enderecoService;
        }

        [SwaggerOperation(Summary = "Efetua cadastro de endereco de cliente")]
        [SwaggerResponse(200, "Executado com sucesso!", typeof(Endereco))]
        [SwaggerResponse(204, "Endereço não encontrado!")]
        [SwaggerResponse(401, "Requisição requer autenticação")]
        [SwaggerResponse(412, "Exceção de negócio!")]
        [SwaggerResponse(500, "Erro Interno!")]
        [HttpPost]
        [Route("cliente/{idCliente}/endereco")]
        [Authorize]
        public async Task<IActionResult> PostEndereco([FromBody] Endereco endereco, int idCliente)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed, ModelState.Values.Select(
                        x => x.Errors.Select(y => y.Exception != null ? y.Exception.Message : y.ErrorMessage)));
                }
                var result = await enderecoService.PostEndereco(endereco, idCliente);

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

        [SwaggerOperation(Summary = "Atualiza cadastro de endereço de cliente")]
        [SwaggerResponse(200, "Executado com sucesso!", typeof(int))]
        [SwaggerResponse(401, "Requisição requer autenticação")]
        [SwaggerResponse(412, "Exceção de negócio!")]
        [SwaggerResponse(500, "Erro Interno!")]
        [HttpPut]
        [Route("cliente/atualiza-endereco-cliente")]
        [Authorize]
        public async Task<IActionResult> PutEndereco([FromBody] Endereco enderecoCliente)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed, ModelState.Values.Select(
                        x => x.Errors.Select(y => y.Exception != null ? y.Exception.Message : y.ErrorMessage)));
                }
                var result = await enderecoService.PutEndereco(enderecoCliente);
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


        [SwaggerOperation(Summary = "Exclui endereco de cliente")]
        [SwaggerResponse(200, "Excluído com sucesso!", typeof(int))]
        [SwaggerResponse(401, "Requisição requer autenticação")]
        [SwaggerResponse(412, "Exceção de negócio!")]
        [SwaggerResponse(500, "Erro Interno!")]
        [HttpDelete]
        [Route("cliente/{idCliente}/exclui-endereco/{idEndereco}")]
        [Authorize]
        public async Task<IActionResult> DeleteEndereco(int idCliente, int idEndereco)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed, ModelState.Values.Select(
                        x => x.Errors.Select(y => y.Exception != null ? y.Exception.Message : y.ErrorMessage)));
                }
                var result = await enderecoService.DeleteEndereco(idCliente, idEndereco);
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
