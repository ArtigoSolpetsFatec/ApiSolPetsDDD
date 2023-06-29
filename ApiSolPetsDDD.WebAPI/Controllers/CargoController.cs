using ApiSolPetsDDD.Domain.Exceptions;
using ApiSolPetsDDD.Domain.Interfaces;
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
    public class CargoController : Controller
    {
        private readonly ICargoService cargoService;

        public CargoController(ICargoService cargoService)
        {
            this.cargoService = cargoService;
        }

        [SwaggerOperation(Summary = "Realiza a busca de todos os cargos cadastrados")]
        [SwaggerResponse(200, "Executado com sucesso!", typeof(List<Cargo>))]
        [SwaggerResponse(204, "Cargos não encontrados!")]
        [SwaggerResponse(401, "Requisição requer autenticação")]
        [SwaggerResponse(412, "Exceção de negócio!")]
        [SwaggerResponse(500, "Erro Interno!")]
        [HttpGet]
        [Route("/cargos/busca-cargos")]
        [Authorize]
        public async Task<IActionResult> GetCargos()
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed, ModelState.Values.Select(
                        x => x.Errors.Select(y => y.Exception != null ? y.Exception.Message : y.ErrorMessage)));
                }

                var result = await cargoService.GetCargos();
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

        [SwaggerOperation(Summary = "Realiza a busca de um cargo por id")]
        [SwaggerResponse(200, "Executado com sucesso!", typeof(Cargo))]
        [SwaggerResponse(204, "Cargo não encontrado!")]
        [SwaggerResponse(401, "Requisição requer autenticação")]
        [SwaggerResponse(412, "Exceção de negócio!")]
        [SwaggerResponse(500, "Erro Interno!")]
        [HttpGet]
        [Route("/cargo/busca-cargo/{idCargo}")]
        [Authorize]
        public async Task<IActionResult> GetCargo(int idCargo)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed, ModelState.Values.Select(
                        x => x.Errors.Select(y => y.Exception != null ? y.Exception.Message : y.ErrorMessage)));
                }

                var result = await cargoService.GetCargo(idCargo);
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

        [SwaggerOperation(Summary = "Realiza a busca de um cargo por sua descrição")]
        [SwaggerResponse(200, "Executado com sucesso!", typeof(Cargo))]
        [SwaggerResponse(204, "Cargo não encontrado!")]
        [SwaggerResponse(401, "Requisição requer autenticação")]
        [SwaggerResponse(412, "Exceção de negócio!")]
        [SwaggerResponse(500, "Erro Interno!")]
        [HttpGet]
        [Route("/cargo/descricao-cargo/{descricaoCargo}")]
        [Authorize]
        public async Task<IActionResult> GetCargoByName(string descricaoCargo)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed, ModelState.Values.Select(
                        x => x.Errors.Select(y => y.Exception != null ? y.Exception.Message : y.ErrorMessage)));
                }

                var result = await cargoService.GetCargoByName(descricaoCargo);
                if (result == null || result.IdCargo == 0)
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

        [SwaggerOperation(Summary = "Efetua o cadastro de um cargo")]
        [SwaggerResponse(200, "Executado com sucesso!", typeof(Cargo))]
        [SwaggerResponse(204, "Cargo não encontrado!")]
        [SwaggerResponse(401, "Requisição requer autenticação")]
        [SwaggerResponse(412, "Exceção de negócio!")]
        [SwaggerResponse(500, "Erro Interno!")]
        [HttpPost]
        [Route("/cargo")]
        [Authorize]
        public async Task<IActionResult> PostCargo([FromBody] Cargo cargo)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed, ModelState.Values.Select(
                        x => x.Errors.Select(y => y.Exception != null ? y.Exception.Message : y.ErrorMessage)));
                }

                var result = await cargoService.PostCargo(cargo);
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

        [SwaggerOperation(Summary = "Atualiza o cadastro de um cargo")]
        [SwaggerResponse(200, "Executado com sucesso!", typeof(int))]
        [SwaggerResponse(204, "Cargo não encontrado!")]
        [SwaggerResponse(401, "Requisição requer autenticação")]
        [SwaggerResponse(412, "Exceção de negócio!")]
        [SwaggerResponse(500, "Erro Interno!")]
        [HttpPut]
        [Route("/cargo/atualiza-cargo")]
        [Authorize]
        public async Task<IActionResult> PutCargo([FromBody] Cargo cargo)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed, ModelState.Values.Select(
                        x => x.Errors.Select(y => y.Exception != null ? y.Exception.Message : y.ErrorMessage)));
                }

                var result = await cargoService.PutCargo(cargo);
                if (result == 0)
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
                return StatusCode((int)HttpStatusCode.InternalServerError, $"Erro Interno : {ex.Message}");
            }
        }

        [SwaggerOperation(Summary = "Exclui o cadastro de um cargo")]
        [SwaggerResponse(200, "Executado com sucesso!", typeof(int))]
        [SwaggerResponse(204, "Cargo não encontrado!")]
        [SwaggerResponse(401, "Requisição requer autenticação")]
        [SwaggerResponse(412, "Exceção de negócio!")]
        [SwaggerResponse(500, "Erro Interno!")]
        [HttpDelete]
        [Route("/cargo/excluir-cargo/{idCargo}")]
        [Authorize]
        public async Task<IActionResult> DeleteCargo(int idCargo)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed, ModelState.Values.Select(
                        x => x.Errors.Select(y => y.Exception != null ? y.Exception.Message : y.ErrorMessage)));
                }

                var result = await cargoService.DeleteCargo(idCargo);
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
                return StatusCode((int)HttpStatusCode.InternalServerError, $"Erro Interno : {ex.Message}");
            }
        }
    }
}
