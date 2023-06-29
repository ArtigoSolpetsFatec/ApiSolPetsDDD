using APISolPets.Domain.Extensions;
using ApiSolPetsDDD.Domain.Exceptions;
using ApiSolPetsDDD.Domain.Interfaces;
using ApiSolPetsDDD.Domain.Model;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace ApiSolPetsDDD.WebAPI.Controllers
{
    [Produces("application/json")]
    public class UserAuthenticatorController : Controller
    {
        private readonly IUserAuthenticatorService userAuthenticatorService;

        public UserAuthenticatorController(IUserAuthenticatorService userAuthenticatorService)
        {
            this.userAuthenticatorService = userAuthenticatorService;
        }

        [SwaggerOperation(Summary = "Busca o token de autorização para chamar todos os endPoints!")]
        [SwaggerResponse(200, "Executado com sucesso!")]
        [SwaggerResponse(204, "Token não encontrado!")]
        [SwaggerResponse(401, "Requisição requer autenticação")]
        [SwaggerResponse(412, "Exceção de negócio!")]
        [SwaggerResponse(500, "Erro Interno!")]
        [HttpPost]
        [Route("/token")]
        public async Task<IActionResult> GetToken([FromBody] UserAuthenticator userAuthenticator)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed, ModelState.Values.Select(
                        x => x.Errors.Select(y => y.Exception != null ? y.Exception.Message : y.ErrorMessage)));
                }

                var result = await userAuthenticatorService.GetToken(userAuthenticator.Username, userAuthenticator.Password);
                if (result == null)
                    return NoContent();

                var token = GeneralExtensions.GenerateToken(result);
                result.Password = "";
                var newObjct = new { User = result, Token = token };
                return Ok(newObjct);
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
