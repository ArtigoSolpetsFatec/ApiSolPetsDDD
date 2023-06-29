using APISolPets.Domain.Models;
using ApiSolPetsDDD.Domain.Exceptions;
using ApiSolPetsDDD.Domain.Extensions;
using ApiSolPetsDDD.Domain.Interfaces;
using ApiSolPetsDDD.Domain.Model;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace APISolPets.Domain.Extensions
{
    public static class GeneralExtensions
    {
        private static readonly RNGCryptoServiceProvider cryptoService = new();
        public static readonly string Secret = "fedaf7d8863b48e197b9287d492b708e";

        public static string SqlCommandToString(this ISqlComWrapper sqlCommand)
        {
            try
            {
                var command = sqlCommand.CommandText;
                foreach (SqlParameter sqlParameter in sqlCommand.Parameters)
                {
                    if (sqlCommand.CommandType == CommandType.StoredProcedure)
                        command = $"{command} @{sqlParameter.ParameterName}";
                    var valueParam = sqlParameter.Value;
                    var parameter = valueParam ?? "NULL";
                    var isString = (valueParam is string) || (valueParam is DateTime);
                    var isDate = valueParam is DateTime;
                    var parameterString = isDate ? ((DateTime)parameter).ToString("yyyy-MM-dd hh:mm:ss") : parameter.ToString();
                    if (isString || isDate)
                        parameter = string.Concat("'", parameterString, "'");
                    command = command.Replace($"@{sqlParameter.ParameterName}", string.IsNullOrEmpty(parameterString) ? "'" : parameterString);
                }
                return command;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public static string RemoveAcentos(this string text)
        {
            return new string(text
                .Normalize(NormalizationForm.FormD)
                .Where(ch => char.GetUnicodeCategory(ch) != UnicodeCategory.NonSpacingMark)
                .ToArray());
        }

        public static bool ValidarCPF(this string cpf)
        {
            int[] multiplicador1 = new int[9] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplicador2 = new int[10] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            string tempCpf;
            string digito;
            int soma;
            int resto;
            cpf = cpf.Trim();
            cpf = cpf.Replace(".", "").Replace("-", "");
            if (cpf.Length != 11)
                return false;

            int num1 = int.Parse(cpf.Substring(0, 1));
            int num2 = int.Parse(cpf.Substring(1, 1));
            int num3 = int.Parse(cpf.Substring(2, 1));
            int num4 = int.Parse(cpf.Substring(3, 1));
            int num5 = int.Parse(cpf.Substring(4, 1));
            int num6 = int.Parse(cpf.Substring(5, 1));
            int num7 = int.Parse(cpf.Substring(6, 1));
            int num8 = int.Parse(cpf.Substring(7, 1));
            int num9 = int.Parse(cpf.Substring(8, 1));
            int num10 = int.Parse(cpf.Substring(9, 1));
            int num11 = int.Parse(cpf.Substring(10, 1));

            if (num1.Equals(num2) && num2.Equals(num3) && num3.Equals(num4) && num4.Equals(num5) &&
                num5.Equals(num6) && num7.Equals(num8) && num8.Equals(num9) && num9.Equals(num10) &&
                num10.Equals(num11))
                return false;

            tempCpf = cpf.Substring(0, 9);
            soma = 0;

            for (int i = 0; i < 9; i++)
                soma += int.Parse(tempCpf[i].ToString()) * multiplicador1[i];

            resto = soma % 11;
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;
            digito = resto.ToString();
            tempCpf += digito;
            soma = 0;
            for (int i = 0; i < 10; i++)
                soma += int.Parse(tempCpf[i].ToString()) * multiplicador2[i];
            resto = soma % 11;
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;
            digito += resto.ToString();
            return cpf.EndsWith(digito);
        }

        public static bool ValidarCNPJ(this string cnpj)
        {
            int[] multiplicador1 = new int[12] { 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplicador2 = new int[13] { 6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
            int soma;
            int resto;
            string digito;
            string tempCnpj;
            cnpj = cnpj.Trim();
            cnpj = cnpj.Replace(".", "").Replace("-", "").Replace("/", "");
            if (cnpj.Length != 14)
                return false;

            int num1 = int.Parse(cnpj.Substring(0, 1));
            int num2 = int.Parse(cnpj.Substring(1, 1));
            int num3 = int.Parse(cnpj.Substring(2, 1));
            int num4 = int.Parse(cnpj.Substring(3, 1));
            int num5 = int.Parse(cnpj.Substring(4, 1));
            int num6 = int.Parse(cnpj.Substring(5, 1));
            int num7 = int.Parse(cnpj.Substring(6, 1));
            int num8 = int.Parse(cnpj.Substring(7, 1));
            int num9 = int.Parse(cnpj.Substring(8, 1));
            int num10 = int.Parse(cnpj.Substring(9, 1));
            int num11 = int.Parse(cnpj.Substring(10, 1));
            int num12 = int.Parse(cnpj.Substring(11, 1));
            int num13 = int.Parse(cnpj.Substring(12, 1));
            int num14 = int.Parse(cnpj.Substring(13, 1));

            if (num1.Equals(num2) && num2.Equals(num3) && num3.Equals(num4) && num4.Equals(num5) &&
                num5.Equals(num6) && num7.Equals(num8) && num8.Equals(num9) && num9.Equals(num10) &&
                num10.Equals(num11) && num11.Equals(num12) && num12.Equals(num13) && num13.Equals(num14))
                return false;

            tempCnpj = cnpj.Substring(0, 12);
            soma = 0;
            for (int i = 0; i < 12; i++)
                soma += int.Parse(tempCnpj[i].ToString()) * multiplicador1[i];
            resto = (soma % 11);
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;
            digito = resto.ToString();
            tempCnpj += digito;
            soma = 0;
            for (int i = 0; i < 13; i++)
                soma += int.Parse(tempCnpj[i].ToString()) * multiplicador2[i];
            resto = (soma % 11);
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;
            digito += resto.ToString();

            return cnpj.EndsWith(digito);
        }

        public static string CriarEmail(this string nomeFuncionario)
        {
            var nomeMinusculo = nomeFuncionario.ToLower();
            var nomeSeparado = nomeMinusculo.Split(" ");
            var inicioEmail = string.Join(".", nomeSeparado);
            return new string(inicioEmail + "@solpets.com");
        }

        public static string GerarSenhaSegura()
        {
            var tamanhoSenha = 12;
            string codigoSenha = DateTime.Now.Ticks.ToString();
            try
            {
                string senha = BitConverter.ToString(new SHA512CryptoServiceProvider().
                    ComputeHash(Encoding.Default.GetBytes(codigoSenha))).Replace("-", string.Empty);
                var result = senha.Substring(0, tamanhoSenha);
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static bool VerificarLogin(string senhaInformada, string senhaBaseDados, string email, string emailBase)
        {
            try
            {
                var hash = new Hash(SHA512.Create());
                var senha = hash.CriptografarSenha(senhaInformada);

                return senha.Equals(senhaBaseDados);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static bool ValidarEmail(this string email)
        {
            if (!string.IsNullOrEmpty(email) && email.Contains("@")) return true;

            return false;
        }

        public static Cliente ValidarInfoCliente(Cliente cliente)
        {
            var dataMaxima = DateTime.MaxValue;
            var dataMinima = DateTime.MinValue;
            var dataAtual = DateTime.Now;
            var cpfIsValid = false;
            var cnpjIsValid = false;
            try
            {
                if (string.IsNullOrEmpty(cliente.CnpjCliente) && string.IsNullOrEmpty(cliente.CpfCliente))
                    throw new ExcecaoNegocio("[Exceção de negócio] - Obrigatório informar o CPF ou CNPJ do cliente!");

                if (!string.IsNullOrEmpty(cliente.CpfCliente))
                    cpfIsValid = cliente.CpfCliente.ValidarCPF();

                if (!cpfIsValid && !string.IsNullOrEmpty(cliente.CpfCliente))
                    throw new ExcecaoNegocio("[Exceção de negócio] - CPF informado é inválido!");

                if (!string.IsNullOrEmpty(cliente.CnpjCliente))
                    cnpjIsValid = cliente.CnpjCliente.ValidarCNPJ();

                if (!cnpjIsValid && !string.IsNullOrEmpty(cliente.CnpjCliente))
                    throw new ExcecaoNegocio("[Exceção de negócio] - CNPJ informado é inválido!");

                if (!string.IsNullOrEmpty(cliente.NomeCliente))
                {
                    cliente.NomeCliente = cliente.NomeCliente.ToUpper().Trim();
                }
                else if (string.IsNullOrEmpty(cliente.NomeCliente) && string.IsNullOrEmpty(cliente.NomeEmpresaCliente))
                {
                    throw new ExcecaoNegocio("[Exceção de Negócio] - Obrigatório informar o nome da empresa ou o nome completo do cliente!");
                }

                cliente.NomeCliente = cliente.NomeCliente.RemoveAcentos();
                var nomeSeparado = cliente.NomeCliente.Split(" ");

                if (nomeSeparado.Length < 2)
                    throw new ExcecaoNegocio("[Exceção de Negócio] - Obrigatório informar o sobrenome do cliente!");

                if (!string.IsNullOrEmpty(cliente.NomeEmpresaCliente))
                {
                    cliente.NomeEmpresaCliente = cliente.NomeEmpresaCliente.ToUpper().Trim();
                    cliente.NomeEmpresaCliente = cliente.NomeEmpresaCliente.RemoveAcentos();
                }

                if (!string.IsNullOrEmpty(cliente.RgCliente) && !string.IsNullOrEmpty(cliente.UfRg))
                {
                    cliente.RgCliente = cliente.RgCliente.Trim();
                    cliente.UfRg = cliente.UfRg.Trim().ToUpper();
                }

                if ((string.IsNullOrEmpty(cliente.RgCliente) && !string.IsNullOrEmpty(cliente.UfRg)) ||
                    (!string.IsNullOrEmpty(cliente.RgCliente) && string.IsNullOrEmpty(cliente.UfRg)))
                    throw new ExcecaoNegocio("[Exceção de Negócio] - Ao informar o RG, obrigatório informar a UF do Estado de emissão e vice-versa!");

                if (cliente.DataNascimentoCliente == dataMaxima || cliente.DataNascimentoCliente == dataMinima ||
                    cliente.DataNascimentoCliente == dataAtual)
                    throw new ExcecaoData("[Exceção de Data] - Data de nascimento informada é inválida!");

                var dataFormatIso = cliente.DataNascimentoCliente.ToString("yyyy-MM-dd HH:mm:ss");
                cliente.DataNascimentoCliente = DateTime.Parse(dataFormatIso);

                if (cliente.ContatosCliente?.Count > 0)
                {
                    foreach (var contato in cliente.ContatosCliente)
                    {
                        if (!string.IsNullOrEmpty(contato.TelefoneCelular))
                            contato.TelefoneCelular = contato.TelefoneCelular.Trim();
                        if (!string.IsNullOrEmpty(contato.OutroCelular))
                            contato.OutroCelular = contato.OutroCelular.Trim();
                        if (!string.IsNullOrEmpty(contato.TelefoneFixo))
                            contato.TelefoneFixo = contato.TelefoneFixo.Trim();
                        if (!string.IsNullOrEmpty(contato.EmailPrincipal))
                        {
                            contato.EmailPrincipal = contato.EmailPrincipal.ValidarEmail() ?
                                contato.EmailPrincipal.Trim() : throw new ExcecaoNegocio("[Exceção de Negócio] - Email informado é inválido!");
                        }
                        if (!string.IsNullOrEmpty(contato.EmailSecundario))
                        {
                            contato.EmailSecundario = contato.EmailSecundario.ValidarEmail() ?
                                contato.EmailSecundario.Trim() : throw new ExcecaoNegocio("[Exceção de Negócio] - Email informado é inválido!");
                        }
                    }
                }
                if (cliente.PetsCliente?.Count > 0)
                {
                    foreach (var petCli in cliente.PetsCliente)
                    {
                        if (!string.IsNullOrEmpty(petCli.NomePet))
                        {
                            petCli.NomePet = petCli.NomePet.RemoveAcentos().ToUpper().Trim();
                        }
                        if (!string.IsNullOrEmpty(petCli.RacaPet))
                        {
                            petCli.RacaPet = petCli.RacaPet.RemoveAcentos().ToUpper().Trim();
                        }
                        if (!string.IsNullOrEmpty(petCli.TipoAnimalPet))
                        {
                            petCli.TipoAnimalPet = petCli.TipoAnimalPet.RemoveAcentos().ToUpper().Trim();
                        }
                    }
                }
                if (cliente.EnderecosCliente?.Count > 0)
                {
                    foreach (var endeCli in cliente.EnderecosCliente)
                    {
                        endeCli.Logradouro = endeCli.Logradouro.RemoveAcentos().ToUpper().Trim();
                        if (!string.IsNullOrEmpty(endeCli.Bairro))
                        {
                            endeCli.Bairro = endeCli.Bairro.RemoveAcentos().ToUpper().Trim();
                        }
                        if (!string.IsNullOrEmpty(endeCli.Cidade))
                        {
                            endeCli.Cidade = endeCli.Cidade.RemoveAcentos().ToUpper().Trim();
                        }
                        if (!string.IsNullOrEmpty(endeCli.Complemento))
                        {
                            endeCli.Complemento = endeCli.Complemento.RemoveAcentos().ToUpper().Trim();
                        }
                        if (!string.IsNullOrEmpty(endeCli.UfEstado))
                        {
                            endeCli.UfEstado = endeCli.UfEstado.RemoveAcentos().ToUpper().Trim();
                        }
                    }
                }

                return cliente;
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

        public static string GenerateToken(UserAuthenticator user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.Username.ToString()),
                    new Claim(ClaimTypes.Role, user.Role.ToString())
                }),
                Expires = DateTime.UtcNow.AddHours(2),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
