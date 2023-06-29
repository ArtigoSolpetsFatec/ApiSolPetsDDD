using APISolPets.Domain.Extensions;
using ApiSolPetsDDD.Domain.Interfaces;
using ApiSolPetsDDD.Domain.Model;
using System;

namespace ApiSolPetsDDD.Application.Services
{
    public class GeradorSenhaService : IGeradorSenhaService
    {
        public SenhaModel GetSenha()
        {
            var result = new SenhaModel();
            try
            {
                result.Senha = GeneralExtensions.GerarSenhaSegura();
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
