using APISolPets.Domain.Extensions;
using ApiSolPetsDDD.Domain.Exceptions;
using ApiSolPetsDDD.Domain.Extensions;
using ApiSolPetsDDD.Domain.Interfaces;
using ApiSolPetsDDD.Domain.Model;
using System;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace ApiSolPetsDDD.Application.Services
{
    public class LoginService : ILoginService
    {
        private readonly ILoginRepository loginRepository;
        private readonly IFuncionarioRepository funcionarioRepository;

        public LoginService(ILoginRepository loginRepository, IFuncionarioRepository funcionarioRepository)
        {
            this.loginRepository = loginRepository;
            this.funcionarioRepository = funcionarioRepository;
        }

        public async Task<Login> GetLogin(string email, string senha)
        {
            var result = new Login();
            try
            {
                if (!email.Contains("@"))
                    throw new ExcecaoNegocio("[Exceção de negócio] - Email informado é inválido!");
                email = !string.IsNullOrEmpty(email) ? email.RemoveAcentos() :
                    throw new ExcecaoNegocio("[Exceção de negócio] - Obrigatório informar o email!");
                email = email.ToLower();
                senha = !string.IsNullOrEmpty(senha) ? senha :
                    throw new ExcecaoNegocio("[Exceção de negócio] - Obrigatório informar a senha!");
                var resultBase = await loginRepository.GetLogin(email);
                if (resultBase.IdLogin > 0)
                {
                    var loginIsValid = GeneralExtensions.VerificarLogin(senha, resultBase.Senha, email, resultBase.Email);
                    result.IdLogin = resultBase.IdLogin;
                    result.Email = email;
                    result.Senha = senha;
                    result.LoginIsValid = loginIsValid;
                    result.IsAdmin = resultBase.IsAdmin;

                    return result;
                }
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

        public async Task<Login> GetLoginById(int idLogin)
        {
            try
            {
                if (idLogin == 0) throw new ExcecaoNegocio("[Exceção de negócio] - Código do Login é inválido!");
                var result = await loginRepository.GetInfoLogin(idLogin);
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

        public async Task<Login> PostLogin(string nomeFuncionario, bool isAdmin, string senha)
        {
            var login = new Login();
            var hash = new Hash(SHA512.Create());
            try
            {
                login.Email = !string.IsNullOrEmpty(nomeFuncionario) ? nomeFuncionario.ToLower() :
                    throw new ExcecaoNegocio("[Exceção de Negócio] - Obrigatório informar o nome completo do funcionário!");
                var nomeSeparado = nomeFuncionario.Split(" ");
                if (nomeSeparado.Length < 2)
                    throw new ExcecaoNegocio("[Exceção de Negócio] - Obrigatório informar o sobrenome do funcionário!");
                login.Email = login.Email.RemoveAcentos();
                login.Email = login.Email.CriarEmail();
                login.Senha = hash.CriptografarSenha(senha);
                login.IsAdmin = isAdmin;

                var result = await loginRepository.CriarLogin(login);

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

        public async Task<int> PutLogin(int idLogin, bool isAdmin)
        {
            var login = new Login();
            try
            {
                if (idLogin == 0)
                    throw new ExcecaoNegocio("[Exceção de Negócio] - Obrigatório informar o id do login!");
                login.IdLogin = idLogin;
                if (string.IsNullOrEmpty(login.Senha))
                    throw new ExcecaoNegocio("[Exceção de Negócio] - Obrigatório informar a senha que irá atualizar!");
                login.IsAdmin = isAdmin;

                var result = await loginRepository.AtualizarLogin(login);

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

        public async Task<int> DeleteLogin(int idLogin)
        {
            try
            {
                if (idLogin == 0)
                    throw new ExcecaoNegocio("[Exceção de Negócio] - Obrigatório informar o id do login!");

                var funcionarioLogin = await funcionarioRepository.GetFuncionarioByIdLogin(idLogin);

                if (funcionarioLogin != null && funcionarioLogin.IdFuncionario > 0)
                    throw new ExcecaoNegocio($"[Exceção de Negócio] - Este login está associado ao funcionário com id {funcionarioLogin.IdFuncionario}.Para excluir o login, exclua primeiro o funcionário!");

                var result = await loginRepository.ExcluirLogin(idLogin);

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
