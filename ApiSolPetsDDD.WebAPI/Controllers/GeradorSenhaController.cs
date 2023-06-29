using ApiSolPetsDDD.Domain.Interfaces;
using ApiSolPetsDDD.Domain.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace ApiSolPetsDDD.WebAPI.Controllers
{
    [Produces("application/json")]
    public class GeradorSenhaController : Controller
    {
        private readonly IGeradorSenhaService geradorSenhaService;

        public GeradorSenhaController(IGeradorSenhaService geradorSenhaService)
        {
            this.geradorSenhaService = geradorSenhaService;
        }

        [SwaggerOperation(Summary = "Gera uma senha segura para o usuário")]
        [SwaggerResponse(200, "Executado com sucesso!", typeof(SenhaModel))]
        [SwaggerResponse(401, "Requisição requer autenticação")]
        [SwaggerResponse(412, "Exceção de negócio!")]
        [SwaggerResponse(500, "Erro Interno!")]
        [HttpGet]
        [Route("/senha/gera-senha")]
        [Authorize]
        public SenhaModel GetSenha()
        {
            var result = geradorSenhaService.GetSenha();
            return result;
        }
    }
}
