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
    public class ContasController : Controller
    {
        private readonly IContasService contasService;

        public ContasController(IContasService contasService)
        {
            this.contasService = contasService;
        }

        [SwaggerOperation(Summary = "Realiza a busca de uma conta através de seu id")]
        [SwaggerResponse(200, "Executado com sucesso!", typeof(PagamentoContasFixas))]
        [SwaggerResponse(204, "Conta não encontrada!")]
        [SwaggerResponse(401, "Requisição requer autenticação")]
        [SwaggerResponse(412, "Exceção de negócio!")]
        [SwaggerResponse(500, "Erro Interno!")]
        [HttpGet]
        [Route("/contas/busca-conta/{idConta}")]
        [Authorize]
        public async Task<IActionResult> GetContaById(int idConta)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed, ModelState.Values.Select(
                        x => x.Errors.Select(y => y.Exception != null ? y.Exception.Message : y.ErrorMessage)));
                }

                var result = await contasService.GetContaById(idConta);
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

        [SwaggerOperation(Summary = "Realiza a busca de contas a vencer")]
        [SwaggerResponse(200, "Executado com sucesso!", typeof(List<PagamentoContasFixas>))]
        [SwaggerResponse(204, "Contas não encontradas!")]
        [SwaggerResponse(401, "Requisição requer autenticação")]
        [SwaggerResponse(412, "Exceção de negócio!")]
        [SwaggerResponse(500, "Erro Interno!")]
        [HttpGet]
        [Route("/contas/busca-contas-a-vencer")]
        [Authorize]
        public async Task<IActionResult> GetContasAVencer()
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed, ModelState.Values.Select(
                        x => x.Errors.Select(y => y.Exception != null ? y.Exception.Message : y.ErrorMessage)));
                }

                var result = await contasService.GetContasAVencer();
                if (result == null || result.Count == 0)
                    return NoContent();

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, $"Erro Interno : {ex.Message}");
            }
        }

        [SwaggerOperation(Summary = "Realiza a busca de contas a vencidas")]
        [SwaggerResponse(200, "Executado com sucesso!", typeof(List<PagamentoContasFixas>))]
        [SwaggerResponse(204, "Contas não encontradas!")]
        [SwaggerResponse(401, "Requisição requer autenticação")]
        [SwaggerResponse(412, "Exceção de negócio!")]
        [SwaggerResponse(500, "Erro Interno!")]
        [HttpGet]
        [Route("/contas/busca-contas-vencidas")]
        [Authorize]
        public async Task<IActionResult> GetContasVencidas()
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed, ModelState.Values.Select(
                        x => x.Errors.Select(y => y.Exception != null ? y.Exception.Message : y.ErrorMessage)));
                }

                var result = await contasService.GetContasVencidas();
                if (result == null || result.Count == 0)
                    return NoContent();

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, $"Erro Interno : {ex.Message}");
            }
        }

        [SwaggerOperation(Summary = "Realiza a busca de contas a vencer do mês corrente")]
        [SwaggerResponse(200, "Executado com sucesso!", typeof(List<PagamentoContasFixas>))]
        [SwaggerResponse(204, "Contas não encontradas!")]
        [SwaggerResponse(401, "Requisição requer autenticação")]
        [SwaggerResponse(412, "Exceção de negócio!")]
        [SwaggerResponse(500, "Erro Interno!")]
        [HttpGet]
        [Route("/contas/busca-contas-vencer-mes")]
        [Authorize]
        public async Task<IActionResult> GetContasAVencerMesCorrente()
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed, ModelState.Values.Select(
                        x => x.Errors.Select(y => y.Exception != null ? y.Exception.Message : y.ErrorMessage)));
                }

                var result = await contasService.GetContasAVencerMesCorrente();
                if (result == null || result.Count == 0)
                    return NoContent();

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, $"Erro Interno : {ex.Message}");
            }
        }

        [SwaggerOperation(Summary = "Realiza a busca de uma conta paga através de seu id")]
        [SwaggerResponse(200, "Executado com sucesso!", typeof(PagamentoContasFixas))]
        [SwaggerResponse(204, "Conta não encontrada!")]
        [SwaggerResponse(401, "Requisição requer autenticação")]
        [SwaggerResponse(412, "Exceção de negócio!")]
        [SwaggerResponse(500, "Erro Interno!")]
        [HttpGet]
        [Route("/contas/busca-conta-paga/{idPagamentoConta}")]
        [Authorize]
        public async Task<IActionResult> GetContaPagaById(int idPagamentoConta)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed, ModelState.Values.Select(
                        x => x.Errors.Select(y => y.Exception != null ? y.Exception.Message : y.ErrorMessage)));
                }

                var result = await contasService.GetContaPagaById(idPagamentoConta);
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

        [SwaggerOperation(Summary = "Cadastra uma conta")]
        [SwaggerResponse(200, "Executado com sucesso!", typeof(ContasFixas))]
        [SwaggerResponse(204, "Conta não encontrada!")]
        [SwaggerResponse(401, "Requisição requer autenticação")]
        [SwaggerResponse(412, "Exceção de negócio!")]
        [SwaggerResponse(500, "Erro Interno!")]
        [HttpPost]
        [Route("/contas")]
        [Authorize]
        public async Task<IActionResult> PostConta([FromBody] ContasFixas contasFixas)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed, ModelState.Values.Select(
                        x => x.Errors.Select(y => y.Exception != null ? y.Exception.Message : y.ErrorMessage)));
                }

                var result = await contasService.PostConta(contasFixas);
                if (result == null)
                    return NoContent();

                return Ok(result);
            }
            catch (ExcecaoData ex)
            {
                return StatusCode((int)HttpStatusCode.PreconditionFailed, $"{ex.Message}");
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

        [SwaggerOperation(Summary = "Cadastra um pagamento de uma conta")]
        [SwaggerResponse(200, "Executado com sucesso!", typeof(PagamentoContasFixas))]
        [SwaggerResponse(204, "Conta não encontrada!")]
        [SwaggerResponse(401, "Requisição requer autenticação")]
        [SwaggerResponse(412, "Exceção de negócio!")]
        [SwaggerResponse(500, "Erro Interno!")]
        [HttpPost]
        [Route("/pagamento-conta")]
        [Authorize]
        public async Task<IActionResult> PostPagamentoConta([FromBody] PagamentoContasFixas pagamentoContasFixas)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed, ModelState.Values.Select(
                        x => x.Errors.Select(y => y.Exception != null ? y.Exception.Message : y.ErrorMessage)));
                }

                var result = await contasService.PostPagamentoConta(pagamentoContasFixas);
                if (result == null)
                    return NoContent();

                return Ok(result);
            }
            catch (ExcecaoData ex)
            {
                return StatusCode((int)HttpStatusCode.PreconditionFailed, $"{ex.Message}");
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

        [SwaggerOperation(Summary = "Atualiza os dados de uma conta")]
        [SwaggerResponse(200, "Executado com sucesso!", typeof(string))]
        [SwaggerResponse(204, "Conta não encontrada!")]
        [SwaggerResponse(401, "Requisição requer autenticação")]
        [SwaggerResponse(412, "Exceção de negócio!")]
        [SwaggerResponse(500, "Erro Interno!")]
        [HttpPut]
        [Route("/contas/atualiza-conta")]
        [Authorize]
        public async Task<IActionResult> PutConta([FromBody] ContasFixas contasFixas)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed, ModelState.Values.Select(
                        x => x.Errors.Select(y => y.Exception != null ? y.Exception.Message : y.ErrorMessage)));
                }

                var result = await contasService.PutConta(contasFixas);
                if (result == 0)
                    return NoContent();

                return Ok("Dados atualizados com sucesso!");
            }
            catch (ExcecaoData ex)
            {
                return StatusCode((int)HttpStatusCode.PreconditionFailed, $"{ex.Message}");
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

        [SwaggerOperation(Summary = "Atualiza os dados de pagamento de uma conta")]
        [SwaggerResponse(200, "Executado com sucesso!", typeof(string))]
        [SwaggerResponse(204, "Conta não encontrada!")]
        [SwaggerResponse(401, "Requisição requer autenticação")]
        [SwaggerResponse(412, "Exceção de negócio!")]
        [SwaggerResponse(500, "Erro Interno!")]
        [HttpPut]
        [Route("/contas/atualiza-pagamento-conta")]
        [Authorize]
        public async Task<IActionResult> PutPagamentoConta([FromBody] PagamentoContasFixas pagamentoContasFixas)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed, ModelState.Values.Select(
                        x => x.Errors.Select(y => y.Exception != null ? y.Exception.Message : y.ErrorMessage)));
                }

                var result = await contasService.PutPagamentoConta(pagamentoContasFixas);
                if (result == 0)
                    return NoContent();

                return Ok("Dados atualizados com sucesso!");
            }
            catch (ExcecaoData ex)
            {
                return StatusCode((int)HttpStatusCode.PreconditionFailed, $"{ex.Message}");
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

        [SwaggerOperation(Summary = "Exclui uma conta paga através de seu id")]
        [SwaggerResponse(200, "Executado com sucesso!", typeof(string))]
        [SwaggerResponse(204, "Conta não encontrada!")]
        [SwaggerResponse(401, "Requisição requer autenticação")]
        [SwaggerResponse(412, "Exceção de negócio!")]
        [SwaggerResponse(500, "Erro Interno!")]
        [HttpGet]
        [Route("/contas/exclui-conta/{idConta}")]
        [Authorize]
        public async Task<IActionResult> DeleteConta(int idConta)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed, ModelState.Values.Select(
                        x => x.Errors.Select(y => y.Exception != null ? y.Exception.Message : y.ErrorMessage)));
                }

                var result = await contasService.DeleteConta(idConta);
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

        [SwaggerOperation(Summary = "Exclui pagamento de uma conta paga através de seu id")]
        [SwaggerResponse(200, "Executado com sucesso!", typeof(string))]
        [SwaggerResponse(204, "Conta não encontrada!")]
        [SwaggerResponse(401, "Requisição requer autenticação")]
        [SwaggerResponse(412, "Exceção de negócio!")]
        [SwaggerResponse(500, "Erro Interno!")]
        [HttpDelete]
        [Route("/contas/exclui-pagamento-conta/{idPagConta}")]
        [Authorize]
        public async Task<IActionResult> DeletePagamentoConta(int idPagConta)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed, ModelState.Values.Select(
                        x => x.Errors.Select(y => y.Exception != null ? y.Exception.Message : y.ErrorMessage)));
                }

                var result = await contasService.DeletePagamentoConta(idPagConta);
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
