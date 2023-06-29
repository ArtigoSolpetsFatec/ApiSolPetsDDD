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
    public class FuncionarioService : IFuncionarioService
    {
        private readonly IFuncionarioRepository funcionarioRepository;

        public FuncionarioService(IFuncionarioRepository funcionarioRepository)
        {
            this.funcionarioRepository = funcionarioRepository;
        }

        public async Task<Funcionario> GetFuncionarioById(int idFuncionario)
        {
            try
            {
                if (idFuncionario == 0) throw new ExcecaoNegocio("[Exceção de negócio] - Código de funcionário é inválido!");
                var result = await funcionarioRepository.GetInfoFuncionario(idFuncionario);
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

        public async Task<Funcionario> GetFuncionarioByNome(string nomeFuncionario)
        {
            try
            {
                if (string.IsNullOrEmpty(nomeFuncionario))
                    throw new ExcecaoNegocio("[Exceção de negócio] - Obrigatório informar o nome do funcionário");
                var result = await funcionarioRepository.GetFuncionarioByName(nomeFuncionario);
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

        public async Task<Funcionario> GetFuncionarioByIdLogin(int idLogin)
        {
            try
            {
                if (idLogin == 0) throw new ExcecaoNegocio("[Exceção de negócio] - Código do login é inválido!");
                var result = await funcionarioRepository.GetFuncionarioByIdLogin(idLogin);
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

        public async Task<Funcionario> PostFuncionario(Funcionario funcionario)
        {
            var dataMaxima = DateTime.MaxValue;
            var dataMinima = DateTime.MinValue;
            var dataAtual = DateTime.Now;
            var hash = new Hash(SHA512.Create());
            try
            {
                var nomeFuncionario = funcionario.NomeCompleto.Split(" ");
                funcionario.NomeCompleto = nomeFuncionario.Length > 1 ? funcionario.NomeCompleto.ToUpper() :
                    throw new ExcecaoNegocio("[Exceção de Negócio] - Obrigatório informar o nome e ao menos 1 sobrenome!");
                funcionario.NomeCompleto = !string.IsNullOrEmpty(funcionario.NomeCompleto) ? funcionario.NomeCompleto.RemoveAcentos() :
                    throw new ExcecaoNegocio("[Exceção de Negócio] - Obrigatório informar o nome completo!");
                var isValidCpf = !string.IsNullOrEmpty(funcionario.Cpf) ? funcionario.Cpf.ValidarCPF() :
                    throw new ExcecaoNegocio("[Exceção de Negócio] - Obrigatório informar o CPF do funcionário!");
                funcionario.Cpf = isValidCpf ? funcionario.Cpf :
                    throw new ExcecaoNegocio("[Exceção de Negócio] - CPF informado é inválido!");
                funcionario.Rg = !string.IsNullOrEmpty(funcionario.Rg) ? funcionario.Rg :
                    throw new ExcecaoNegocio("[Exceção de Negócio] - Obrigatório informar o RG do funcionário!");
                funcionario.IdCargo = funcionario.IdCargo != 0 ? funcionario.IdCargo :
                    throw new ExcecaoNegocio("[Exceção de Negócio] - Obrigatório informar o id do cargo!");
                if (funcionario.DhInativo == dataMaxima)
                    throw new ExcecaoData("[Exceção de Data] - Data de inatividade informada é inválida!");
                if (funcionario.DhNascimento == dataMaxima || funcionario.DhNascimento == dataMinima
                    || funcionario.DhNascimento == dataAtual || funcionario.DhNascimento > dataAtual)
                    throw new ExcecaoData("[Exceção de Data] - Data de nascimento informada é inválida!");
                if (funcionario.DhInicio == dataMaxima || funcionario.DhInicio == dataMinima)
                    throw new ExcecaoData("[Exceção de Data] - Data de início informada é inválida!");

                var senhaInicial = GeneralExtensions.GerarSenhaSegura();

                funcionario.Login = new Login()
                {
                    Email = funcionario.NomeCompleto.CriarEmail(),
                    Senha = senhaInicial,
                    SenhaCriptografada = hash.CriptografarSenha(senhaInicial),
                    IsAdmin = funcionario.NomeCargo.ToUpper().RemoveAcentos().Contains("GERENTE")
                };

                var result = await funcionarioRepository.CadastrarFuncionario(funcionario);

                result.Login.Senha = funcionario.Login.Senha;
                result.Login.SenhaCriptografada = funcionario.Login.SenhaCriptografada;

                return result;
            }
            catch (ExcecaoData ex)
            {
                throw new ExcecaoData(ex.Message);
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

        public async Task<int> PutFuncionario(Funcionario funcionario)
        {
            var dataMaxima = DateTime.MaxValue;
            var dataMinima = DateTime.MinValue;
            var dataAtual = DateTime.Now;
            try
            {
                if (funcionario.IdFuncionario == 0)
                    throw new ExcecaoNegocio("[Exceção de Negócio] - Obrigatório informar o id do funcionário!");
                var nomeFuncionario = funcionario.NomeCompleto.Split(" ");
                funcionario.NomeCompleto = nomeFuncionario.Length > 1 ? funcionario.NomeCompleto.ToUpper() :
                    throw new ExcecaoNegocio("[Exceção de Negócio] - Obrigatório informar o nome e ao menos 1 sobrenome!");
                funcionario.NomeCompleto = !string.IsNullOrEmpty(funcionario.NomeCompleto) ? funcionario.NomeCompleto.RemoveAcentos() :
                    throw new ExcecaoNegocio("[Exceção de Negócio] - Obrigatório informar o nome completo!");
                var isValidCpf = !string.IsNullOrEmpty(funcionario.Cpf) ? funcionario.Cpf.ValidarCPF() :
                    throw new ExcecaoNegocio("[Exceção de Negócio] - Obrigatório informar o CPF do funcionário!");
                funcionario.Cpf = isValidCpf ? funcionario.Cpf :
                    throw new ExcecaoNegocio("[Exceção de Negócio] - CPF informado é inválido!");
                funcionario.Rg = !string.IsNullOrEmpty(funcionario.Rg) ? funcionario.Rg :
                    throw new ExcecaoNegocio("[Exceção de Negócio] - Obrigatório informar o RG do funcionário!");
                funcionario.IdCargo = funcionario.IdCargo != 0 ? funcionario.IdCargo :
                    throw new ExcecaoNegocio("[Exceção de Negócio] - Obrigatório informar o id do cargo!");
                funcionario.NomeCargo = !string.IsNullOrEmpty(funcionario.NomeCargo) ? funcionario.NomeCargo.ToUpper() :
                  throw new ExcecaoNegocio("[Exceção de Negócio] - Obrigatório informar o nome do cargo!");
                funcionario.NomeCargo = funcionario.NomeCargo.RemoveAcentos();
                if (funcionario.DhInativo == dataMaxima)
                    throw new ExcecaoData("[Exceção de Data] - Data de inatividade informada é inválida!");
                if (funcionario.DhInativo == dataAtual || funcionario.DhInativo > dataAtual)
                    throw new ExcecaoData("[Exceção de Data] - Data de inatividade informada não pode ser atual, nem futura!");
                if (funcionario.DhNascimento == dataMaxima || funcionario.DhNascimento == dataMinima)
                    throw new ExcecaoData("[Exceção de Data] - Data de nascimento informada é inválida!");
                if (funcionario.DhInicio == dataMaxima || funcionario.DhInicio == dataMinima)
                    throw new ExcecaoData("[Exceção de Data] - Data de início informada é inválida!");
                if (funcionario.Login != null && string.IsNullOrEmpty(funcionario.Login.Senha))
                    throw new ExcecaoNegocio("[Exceção de Negócio] - Ao informar o login do funcionário é obrigatório informar a senha!");

                var result = await funcionarioRepository.AtualizarFuncionario(funcionario);

                return result;
            }
            catch (ExcecaoData ex)
            {
                throw new ExcecaoData(ex.Message);
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

        public async Task<int> DeleteFuncionario(int idFuncionario)
        {
            try
            {
                if (idFuncionario == 0)
                    throw new ExcecaoNegocio("[Exceção de Negócio] - Obrigatório informar o id do funcionário!");

                var result = await funcionarioRepository.ExcluirFuncionario(idFuncionario);

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
