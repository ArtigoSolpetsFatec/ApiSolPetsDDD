using ApiSolPetsDDD.Domain.Exceptions;
using ApiSolPetsDDD.Domain.Interfaces;
using ApiSolPetsDDD.Domain.Model;
using System;
using System.Threading.Tasks;

namespace ApiSolPetsDDD.Application.Services
{
    public class UserAuthenticatorService : IUserAuthenticatorService
    {
        private readonly IUserAuthenticatorRepository userAuthenticatorRepository;

        public UserAuthenticatorService(IUserAuthenticatorRepository userAuthenticatorRepository)
        {
            this.userAuthenticatorRepository = userAuthenticatorRepository;
        }

        public async Task<UserAuthenticator> GetToken(string username, string password)
        {
            try
            {
                if (string.IsNullOrEmpty(username))
                    throw new ExcecaoNegocio("[Exceção de Negócio] - Obrigatório informar o nome do usuário!");
                if (string.IsNullOrEmpty(password))
                    throw new ExcecaoNegocio("[Exceção de Negócio] - Obrigatório informar a senha!");

                var result = await userAuthenticatorRepository.GetUser(username, password);
                return result;
            }
            catch (ExcecaoNegocio ex)
            {
                throw new ExcecaoNegocio(ex.Message);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
