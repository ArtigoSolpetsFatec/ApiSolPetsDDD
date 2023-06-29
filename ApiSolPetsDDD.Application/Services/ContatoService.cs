using ApiSolPetsDDD.Domain.Exceptions;
using ApiSolPetsDDD.Domain.Interfaces;
using ApiSolPetsDDD.Domain.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApiSolPetsDDD.Application.Services
{
    public class ContatoService : IContatoService
    {
        private readonly IContatoRepository contatoRepository;
        private readonly IClienteService clienteService;
        private readonly IFornecedorService fornecedorService;

        public ContatoService(IContatoRepository contatoRepository, IClienteService clienteService,
            IFornecedorService fornecedorService)
        {
            this.contatoRepository = contatoRepository;
            this.clienteService = clienteService;
            this.fornecedorService = fornecedorService;
        }

        public async Task<List<Contato>> GetContatosByIdClienteOrFonecedor(int idCliente, int idFornecedor)
        {
            try
            {
                var result = await contatoRepository.GetContatos(idCliente, idFornecedor);
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

        public async Task<Contato> PostContato(Contato contato)
        {
            try
            {
                var cliente = await clienteService.GetClienteById(contato.IdCliente);
                var fornecedor = await fornecedorService.GetInfoFornecedor(contato.IdFornecedor);
                if (cliente.IdCliente == 0 && fornecedor.IdFornecedor == 0)
                    throw new ExcecaoNegocio("[Exceção de negócio] - Obrigatório informar o id do cliente ou do fornecedor");
                var result = await contatoRepository.CadastrarContato(contato);
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

        public async Task<int> PutContato(Contato contatoCliente)
        {
            try
            {
                var result = await contatoRepository.AtualizaContato(contatoCliente);
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

        public async Task<int> DeleteContato(int idContato, int idCliente, int idFornecedor)
        {
            try
            {
                var result = await contatoRepository.ExcluirContato(idCliente, idContato, idFornecedor);
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
