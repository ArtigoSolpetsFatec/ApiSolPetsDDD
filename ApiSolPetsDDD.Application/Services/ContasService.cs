using APISolPets.Domain.Extensions;
using ApiSolPetsDDD.Domain.Exceptions;
using ApiSolPetsDDD.Domain.Interfaces;
using ApiSolPetsDDD.Domain.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApiSolPetsDDD.Application.Services
{
    public class ContasService : IContasService
    {
        private readonly IContasRepository contasRepository;

        public ContasService(IContasRepository contasRepository)
        {
            this.contasRepository = contasRepository;
        }

        public async Task<PagamentoContasFixas> GetContaById(int idConta)
        {
            try
            {
                if (idConta == 0)
                    throw new ExcecaoNegocio("[Exceção de Negócio] - Obrigatório informar o id da conta!");

                var result = await contasRepository.GetContaById(idConta);
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

        public async Task<List<PagamentoContasFixas>> GetContasAVencer()
        {
            try
            {
                var result = await contasRepository.GetContasAVencer();
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<PagamentoContasFixas>> GetContasVencidas()
        {
            try
            {
                var result = await contasRepository.GetContasVencidas();
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<PagamentoContasFixas>> GetContasAVencerMesCorrente()
        {
            try
            {
                var result = await contasRepository.GetContasAVencerMesCorrente();
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<PagamentoContasFixas> GetContaPagaById(int idPagamentoConta)
        {
            try
            {
                if (idPagamentoConta == 0)
                    throw new ExcecaoNegocio("[Exceção de Negócio] - Obrigatório informar o id pagamento!");

                var result = await contasRepository.GetContaPagaById(idPagamentoConta);
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

        public async Task<ContasFixas> PostConta(ContasFixas contas)
        {
            var dataMaxima = DateTime.MaxValue;
            var dataMinima = DateTime.MinValue;
            try
            {
                contas.TipoConta = !string.IsNullOrEmpty(contas.TipoConta) ? contas.TipoConta.ToUpper() :
                    throw new ExcecaoNegocio("[Exceção de Negócio] - Obrigatório informar o tipo da conta!");
                contas.TipoConta = contas.TipoConta.RemoveAcentos();
                contas.Empresa = !string.IsNullOrEmpty(contas.Empresa) ? contas.Empresa.ToUpper() :
                    throw new ExcecaoNegocio("[Exceção de Negócio] - Obrigatório informar a empresa da conta!");
                contas.Empresa = contas.Empresa.RemoveAcentos();
                contas.ValorConta = contas.ValorConta > 0.0 ? contas.ValorConta :
                    throw new ExcecaoNegocio("[Exceção de Negócio] - Obrigatório informar o valor da conta!");
                if (contas.DataVencimento == dataMaxima || contas.DataVencimento == dataMinima)
                    throw new ExcecaoData("[Exceção de Data] - Data de vencimento informada é inválida!");

                var result = await contasRepository.CadastrarConta(contas);
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

        public async Task<PagamentoContasFixas> PostPagamentoConta(PagamentoContasFixas pagConta)
        {
            var dataMaxima = DateTime.MaxValue;
            var dataMinima = DateTime.MinValue;
            try
            {
                pagConta.IdContas = pagConta.IdContas > 0 ? pagConta.IdContas :
                    throw new ExcecaoNegocio("[Exceção de Negócio] - Obrigatório informar o id da conta!");
                if (pagConta.DataPagamento == dataMaxima || pagConta.DataPagamento == dataMinima)
                    throw new ExcecaoData("[Exceção de Data] - Data de pagamento informada é inválida!");
                pagConta.Funcionario.IdFuncionario = pagConta.Funcionario.IdFuncionario > 0 ? pagConta.Funcionario.IdFuncionario :
                    throw new ExcecaoNegocio("[Exceção de Negócio] - Obrigatório informar o id do funcionário!");

                var result = await contasRepository.CadastrarPagamentoConta(pagConta);
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

        public async Task<int> PutConta(ContasFixas contas)
        {
            var dataMaxima = DateTime.MaxValue;
            var dataMinima = DateTime.MinValue;
            try
            {
                contas.IdContas = contas.IdContas > 0 ? contas.IdContas :
                    throw new ExcecaoNegocio("[Exceção de Negócio] - Obrigatório informar o id da conta!");
                contas.TipoConta = !string.IsNullOrEmpty(contas.TipoConta) ? contas.TipoConta.ToUpper() :
                    throw new ExcecaoNegocio("[Exceção de Negócio] - Obrigatório informar o tipo da conta!");
                contas.TipoConta = contas.TipoConta.RemoveAcentos();
                contas.Empresa = !string.IsNullOrEmpty(contas.Empresa) ? contas.Empresa.ToUpper() :
                    throw new ExcecaoNegocio("[Exceção de Negócio] - Obrigatório informar a empresa da conta!");
                contas.Empresa = contas.Empresa.RemoveAcentos();
                contas.ValorConta = contas.ValorConta > 0.0 ? contas.ValorConta :
                    throw new ExcecaoNegocio("[Exceção de Negócio] - Obrigatório informar o valor da conta!");
                if (contas.DataVencimento == dataMaxima || contas.DataVencimento == dataMinima)
                    throw new ExcecaoData("[Exceção de Data] - Data de vencimento informada é inválida!");

                var result = await contasRepository.AtualizarConta(contas);
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

        public async Task<int> PutPagamentoConta(PagamentoContasFixas pagConta)
        {
            var dataMaxima = DateTime.MaxValue;
            var dataMinima = DateTime.MinValue;
            try
            {
                pagConta.IdPagamento = pagConta.IdPagamento > 0 ? pagConta.IdPagamento :
                    throw new ExcecaoNegocio("[Exceção de Negócio] - Obrigatório informar o id do pagamento!");
                pagConta.IdContas = pagConta.IdContas > 0 ? pagConta.IdContas :
                    throw new ExcecaoNegocio("[Exceção de Negócio] - Obrigatório informar o id da conta!");
                if (pagConta.DataPagamento == dataMaxima || pagConta.DataPagamento == dataMinima)
                    throw new ExcecaoData("[Exceção de Data] - Data de pagamento informada é inválida!");
                pagConta.Funcionario.IdFuncionario = pagConta.Funcionario.IdFuncionario > 0 ? pagConta.Funcionario.IdFuncionario :
                    throw new ExcecaoNegocio("[Exceção de Negócio] - Obrigatório informar o id do funcionário!");

                var result = await contasRepository.AtualizarPagamentoConta(pagConta);
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

        public async Task<int> DeleteConta(int idConta)
        {
            try
            {
                if (idConta == 0)
                    throw new ExcecaoNegocio("[Exceção de Negócio] - Obrigatório informar o id da conta!");

                var result = await contasRepository.ExcluirConta(idConta);
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

        public async Task<int> DeletePagamentoConta(int idPagConta)
        {
            try
            {
                if (idPagConta == 0)
                    throw new ExcecaoNegocio("[Exceção de Negócio] - Obrigatório informar o id do pagamento!");

                var result = await contasRepository.ExcluirPagamentoConta(idPagConta);
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
