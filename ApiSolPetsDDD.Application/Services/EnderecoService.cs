using ApiSolPetsDDD.Domain.Exceptions;
using ApiSolPetsDDD.Domain.Interfaces;
using ApiSolPetsDDD.Domain.Model;
using System;
using System.Threading.Tasks;

namespace ApiSolPetsDDD.Application.Services
{
    public class EnderecoService : IEnderecoService
    {
        private readonly IClienteRepository clienteRepository;

        public EnderecoService(IClienteRepository clienteRepository)
        {
            this.clienteRepository = clienteRepository;
        }

        public async Task<Endereco> PostEndereco(Endereco endereco, int idCliente)
        {
            try
            {
                var result = await clienteRepository.CadastrarEnderecoCliente(endereco, idCliente);
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

        public async Task<int> PutEndereco(Endereco enderecoCliente)
        {
            try
            {
                if (enderecoCliente.IdEndreco == 0 || enderecoCliente?.IdEndreco == null)
                    throw new ExcecaoNegocio("[Exceção de negócio] - Informe um id do endereço válido!");
                var result = await clienteRepository.AtualizaEnderecoCliente(enderecoCliente);
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

        public async Task<int> DeleteEndereco(int idCliente, int idEndereco)
        {
            try
            {
                if (idCliente == 0 && idEndereco == 0)
                    throw new ExcecaoNegocio("[Exceção de negócio] - id do cliente e/ou id do endereço inválido");
                var result = await clienteRepository.ExcluirEndeCliente(idCliente, idEndereco);
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
