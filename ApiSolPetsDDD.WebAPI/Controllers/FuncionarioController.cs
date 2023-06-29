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
    public class FuncionarioController : Controller
    {
        private readonly IFuncionarioService funcionarioService;

        public FuncionarioController(IFuncionarioService funcionarioService)
        {
            this.funcionarioService = funcionarioService;
        }

        [SwaggerOperation(Summary = "Executa busca de dados do funcionário")]
        [SwaggerResponse(200, "Executado com sucesso!", typeof(Funcionario))]
        [SwaggerResponse(204, "Funcionário não encontrado!")]
        [SwaggerResponse(401, "Requisição requer autenticação")]
        [SwaggerResponse(412, "Exceção de negócio!")]
        [SwaggerResponse(500, "Erro Interno!")]
        [HttpGet]
        [Route("/funcionario/{idFuncionario}")]
        [Authorize]
        public async Task<IActionResult> GetFuncionarioById(int idFuncionario)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed, ModelState.Values.Select(
                        x => x.Errors.Select(y => y.Exception != null ? y.Exception.Message : y.ErrorMessage)));
                }

                var result = await funcionarioService.GetFuncionarioById(idFuncionario);
                if (result == null || result.IdFuncionario == 0)
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

        [SwaggerOperation(Summary = "Executa busca de dados do funcionário por nome")]
        [SwaggerResponse(200, "Executado com sucesso!", typeof(Funcionario))]
        [SwaggerResponse(204, "Funcionário não encontrado!")]
        [SwaggerResponse(401, "Requisição requer autenticação")]
        [SwaggerResponse(412, "Exceção de negócio!")]
        [SwaggerResponse(500, "Erro Interno!")]
        [HttpGet]
        [Route("/funcionario/nome-funcionario/{nomeFuncionario}")]
        [Authorize]
        public async Task<IActionResult> GetFuncionarioByNome(string nomeFuncionario)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed, ModelState.Values.Select(
                        x => x.Errors.Select(y => y.Exception != null ? y.Exception.Message : y.ErrorMessage)));
                }

                var result = await funcionarioService.GetFuncionarioByNome(nomeFuncionario);
                if (result == null || result.IdFuncionario == 0)
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

        [SwaggerOperation(Summary = "Executa busca de dados do funcionário")]
        [SwaggerResponse(200, "Executado com sucesso!", typeof(Funcionario))]
        [SwaggerResponse(204, "Funcionário não encontrado!")]
        [SwaggerResponse(401, "Requisição requer autenticação")]
        [SwaggerResponse(412, "Exceção de negócio!")]
        [SwaggerResponse(500, "Erro Interno!")]
        [HttpGet]
        [Route("/funcionario/idLogin/{idLogin}")]
        [Authorize]
        public async Task<IActionResult> GetFuncionarioByIdLogin(int idLogin)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed, ModelState.Values.Select(
                        x => x.Errors.Select(y => y.Exception != null ? y.Exception.Message : y.ErrorMessage)));
                }

                var result = await funcionarioService.GetFuncionarioByIdLogin(idLogin);
                if (result == null || result.IdFuncionario == 0)
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

        [SwaggerOperation(Summary = "Efetua cadastro de funcionário")]
        [SwaggerResponse(200, "Executado com sucesso!", typeof(Funcionario))]
        [SwaggerResponse(204, "Funcionário não encontrado!")]
        [SwaggerResponse(401, "Requisição requer autenticação")]
        [SwaggerResponse(412, "Exceção de negócio!")]
        [SwaggerResponse(500, "Erro Interno!")]
        [HttpPost]
        [Route("/funcionario")]
        [Authorize]
        public async Task<IActionResult> PostFuncionario([FromBody] Funcionario funcionario)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed, ModelState.Values.Select(
                        x => x.Errors.Select(y => y.Exception != null ? y.Exception.Message : y.ErrorMessage)));
                }

                var result = await funcionarioService.PostFuncionario(funcionario);
                if (result == null || result.IdFuncionario == 0)
                    return NoContent();

                return Ok(result);
            }
            catch (ExcecaoNegocio ex)
            {
                return StatusCode((int)HttpStatusCode.PreconditionFailed, $"{ex.Message}");
            }
            catch (ExcecaoData ex)
            {
                return StatusCode((int)HttpStatusCode.PreconditionFailed, $"{ex.Message}");
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, $"Erro : {ex.Message}");
            }
        }

        [SwaggerOperation(Summary = "Atualiza o cadastro de um funcionário")]
        [SwaggerResponse(200, "Executado com sucesso!", typeof(Funcionario))]
        [SwaggerResponse(204, "funcionário não encontrado!")]
        [SwaggerResponse(401, "Requisição requer autenticação")]
        [SwaggerResponse(412, "Exceção de negócio!")]
        [SwaggerResponse(500, "Erro Interno!")]
        [HttpPut]
        [Route("/funcionário/atualiza-funcionário")]
        [Authorize]
        public async Task<IActionResult> PutFuncionario([FromBody] Funcionario funcionario)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed, ModelState.Values.Select(
                        x => x.Errors.Select(y => y.Exception != null ? y.Exception.Message : y.ErrorMessage)));
                }

                var result = await funcionarioService.PutFuncionario(funcionario);
                if (result == 0)
                    return NoContent();
                return Ok("Dados atualizados com sucesso!");
            }
            catch (ExcecaoNegocio ex)
            {
                return StatusCode((int)HttpStatusCode.PreconditionFailed, $"{ex.Message}");
            }
            catch (ExcecaoData ex)
            {
                return StatusCode((int)HttpStatusCode.PreconditionFailed, $"{ex.Message}");
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, $"Erro Interno : {ex.Message}");
            }
        }

        [SwaggerOperation(Summary = "Exclui o cadastro de um funcionário")]
        [SwaggerResponse(200, "Executado com sucesso!", typeof(Funcionario))]
        [SwaggerResponse(204, "Funcionário não encontrado!")]
        [SwaggerResponse(401, "Requisição requer autenticação")]
        [SwaggerResponse(412, "Exceção de negócio!")]
        [SwaggerResponse(500, "Erro Interno!")]
        [HttpDelete]
        [Route("/funcionario/excluir-funcionario/{idFuncionario}")]
        [Authorize]
        public async Task<IActionResult> DeleteFuncioanrio(int idFuncionario)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed, ModelState.Values.Select(
                        x => x.Errors.Select(y => y.Exception != null ? y.Exception.Message : y.ErrorMessage)));
                }

                var result = await funcionarioService.DeleteFuncionario(idFuncionario);
                if (result == 0)
                    return NoContent();

                return Ok("Dados excluídos com sucesso");
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
