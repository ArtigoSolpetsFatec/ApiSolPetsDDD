using APISolPets.Domain.Extensions;
using APISolPets.Domain.Models;
using ApiSolPetsDDD.Domain.Exceptions;
using ApiSolPetsDDD.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApiSolPetsDDD.Application.Services
{
    public class ClienteService : IClienteService
    {
        private readonly IClienteRepository clienteRepository;

        public ClienteService(IClienteRepository clienteRepository)
        {
            this.clienteRepository = clienteRepository;
        }

        public async Task<Cliente> GetClienteById(int idCliente)
        {
            try
            {
                if (idCliente == 0) throw new ExcecaoNegocio("[Exceção de negócio] - Obrigatório informar o id do cliente!");
                var result = await clienteRepository.GetInfoCliente(idCliente);
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

        public async Task<List<Cliente>> GetClienteByNome(string nomeCliente)
        {
            try
            {
                if (string.IsNullOrEmpty(nomeCliente))
                    throw new ExcecaoNegocio("[Exceção de negócio] - Obrigatório informar o nome do cliente");
                var nomeSemAcentos = nomeCliente.RemoveAcentos();
                nomeSemAcentos = nomeSemAcentos.ToUpper();
                nomeSemAcentos = nomeSemAcentos.Trim();
                var result = await clienteRepository.GetClienteNome(nomeSemAcentos);
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

        public async Task<List<Cliente>> GetClienteByCpf(string cpf)
        {
            try
            {
                if (string.IsNullOrEmpty(cpf))
                    throw new ExcecaoNegocio("[Exceção de negócio] - Obrigatório informar o CPF do cliente!");
                cpf = cpf.Trim();
                var cpfIsValid = cpf.ValidarCPF();
                if (!cpfIsValid)
                    throw new ExcecaoNegocio("[Exceção de negócio] - CPF inválido!");
                cpf = cpf.Replace(".", "").Replace("-", "").Replace("/", "");
                var result = await clienteRepository.GetClienteCpf(cpf);
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

        public async Task<List<Cliente>> GetClientes(int limit)
        {
            try
            {
                var result = await clienteRepository.GetInfoClientes(limit);
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

        public async Task<Cliente> PostCliente(Cliente cliente)
        {

            try
            {
                var clienteValidado = GeneralExtensions.ValidarInfoCliente(cliente);

                var result = await clienteRepository.CadastrarCliente(clienteValidado);
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

        public async Task<int> PutCliente(Cliente cliente)
        {
            try
            {
                if (cliente.IdCliente == 0)
                    throw new ExcecaoNegocio("[Exceção de negócio] - Obrigatório informar o id do cliente!");
                var clienteValidado = GeneralExtensions.ValidarInfoCliente(cliente);

                var result = await clienteRepository.AtualizaCliente(cliente);
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

        public async Task<int> DeleteCliente(int idCliente)
        {
            try
            {
                if (idCliente == 0)
                    throw new ExcecaoNegocio("[Exceção de Negócio] - Obrigatório informar o id do cliente!");
                var result = await clienteRepository.ExcluirCliente(idCliente);
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
