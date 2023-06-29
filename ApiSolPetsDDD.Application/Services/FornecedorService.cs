using APISolPets.Domain.Extensions;
using ApiSolPetsDDD.Domain.Exceptions;
using ApiSolPetsDDD.Domain.Interfaces;
using ApiSolPetsDDD.Domain.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApiSolPetsDDD.Application.Services
{
    public class FornecedorService : IFornecedorService
    {
        private readonly IFornecedorRepository fornecedorRepository;

        public FornecedorService(IFornecedorRepository fornecedorRepository)
        {
            this.fornecedorRepository = fornecedorRepository;
        }

        public async Task<List<Fornecedor>> GetFornecedores()
        {
            try
            {
                var result = await fornecedorRepository.GetAllFornecedores();
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Fornecedor> PostFornecedor(Fornecedor fornecedor)
        {
            try
            {
                if (string.IsNullOrEmpty(fornecedor.NomeFornecedor))
                    throw new ExcecaoNegocio("[Exceção de negócio] - Obrigatório informar nome do fornecedor!");
                else
                {
                    fornecedor.NomeFornecedor = fornecedor.NomeFornecedor.Trim();
                    fornecedor.NomeFornecedor = fornecedor.NomeFornecedor.ToUpper();
                }
                if (!fornecedor.CnpjFornecedor.ValidarCNPJ())
                    throw new ExcecaoNegocio("[Exceção de negócio] - O CNPJ informado é inválido!");

                var result = await fornecedorRepository.CadastrarFornecedor(fornecedor);
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

        public async Task<Fornecedor> GetInfoFornecedor(int idFornecedor)
        {
            var result = new Fornecedor();
            try
            {
                if (idFornecedor > 0)
                    result = await fornecedorRepository.GetFornecedorById(idFornecedor);
                else
                    throw new ExcecaoNegocio("[Exceção de negócio] - Obrigatório informar o código do fornecedor!");
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

        public async Task<List<Fornecedor>> GetInfoFornecedorByCnpj(string cnpjFornecedor)
        {
            var result = new List<Fornecedor>();
            try
            {
                if (!string.IsNullOrEmpty(cnpjFornecedor) && cnpjFornecedor.ValidarCNPJ())
                    result = await fornecedorRepository.GetFornecedorByCnpj(cnpjFornecedor);
                else if (!cnpjFornecedor.ValidarCNPJ())
                    throw new ExcecaoNegocio("[Exceção de negócio] - O CNPJ informado é inválido!");
                else
                    throw new ExcecaoNegocio("[Exceção de negócio] - Obrigatório informar o CNPJ!");

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

        public async Task<int> PutFornecedor(Fornecedor fornecedor)
        {
            int result = 0;
            try
            {
                if (fornecedor.IdFornecedor > 0)
                    result = await fornecedorRepository.AtualizaFornecedor(fornecedor);
                else
                    throw new ExcecaoNegocio("[Exceção de Negócio] - Obrigatório informar o código do fornecedor!");
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

        public async Task<int> DeleteFornecedor(int idFornecedor)
        {
            int result = 0;
            try
            {
                if (idFornecedor > 0)
                    result = await fornecedorRepository.ExcluirFornecedor(idFornecedor);
                else
                    throw new ExcecaoNegocio("[Exceção de negócio] - Obrigatório informar o código do fornecedor!");
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
