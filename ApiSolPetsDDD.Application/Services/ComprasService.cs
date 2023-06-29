using ApiSolPetsDDD.Domain.Exceptions;
using ApiSolPetsDDD.Domain.Interfaces;
using ApiSolPetsDDD.Domain.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApiSolPetsDDD.Application.Services
{
    public class ComprasService : IComprasService
    {
        private readonly IComprasRepository comprasRepository;

        public ComprasService(IComprasRepository comprasRepository)
        {
            this.comprasRepository = comprasRepository;
        }

        public async Task<List<Compra>> GetCompras(string dataInicio, string dataFim)
        {
            var dataMaxima = DateTime.MaxValue;
            var dataMinima = DateTime.MinValue;
            try
            {
                if ((DateTime.TryParse(dataInicio, out DateTime dataInicioValida) &&
                    (dataInicioValida == dataMaxima || dataInicioValida == dataMinima)) ||
                    !DateTime.TryParse(dataInicio, out DateTime dataIniValida))
                {
                    throw new ExcecaoData("[Exceção de Data] - Data inicial informada é inválida!");
                }
                if ((DateTime.TryParse(dataFim, out DateTime dataFimValida) &&
                    (dataFimValida == dataMaxima || dataFimValida == dataMinima)) ||
                    !DateTime.TryParse(dataFim, out DateTime dataFinalValida))
                {
                    throw new ExcecaoData("[Exceção de Data] - Data final informada é inválida!");
                }

                var result = await comprasRepository.GetInfoCompras(dataInicioValida, dataFimValida);
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

        public async Task<Compra> PostCompra(Compra compra)
        {
            var dataMaxima = DateTime.MaxValue;
            var dataMinima = DateTime.MinValue;
            var dataAtual = DateTime.Now;
            try
            {
                if (compra.Fornecedor.IdFornecedor == 0)
                    throw new ExcecaoNegocio("[Exceção de negócio] - Obrigatório informar o id do fornecedor!");
                if (compra.IdProduto == 0)
                    throw new ExcecaoNegocio("[Exceção de negócio] - Obrigatório informar o id do produto!");
                if (compra.Funcionario.IdFuncionario == 0)
                    throw new ExcecaoNegocio("[Exceção de negócio] - Obrigatório informar o id do funcionário!");
                if (compra.DataCompra == dataMaxima || compra.DataCompra == dataMinima)
                    throw new ExcecaoData("[Exceção de Data] - Data da compra informada é inválida!");
                if (compra.DataCompra > dataAtual)
                    throw new ExcecaoData("[Exceção de Data] - Data da compra não pode ser data futura!");
                if (compra.DataValidade == dataMaxima || compra.DataValidade == dataMinima)
                    throw new ExcecaoData("[Exceção de Data] - Data da validade do produto informada é inválida!");
                if (compra.ValorCompra == 0.0)
                    throw new ExcecaoNegocio("[Exceção de negócio] - Obrigatório informar o valor da compra!");
                if (compra.QuantidadeCompra == 0)
                    throw new ExcecaoNegocio("[Exceção de negócio] - Obrigatório informar a quantidade comprada!");

                var result = await comprasRepository.CadastrarCompra(compra);
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

        public async Task<int> PutCompra(Compra compra)
        {
            var dataMaxima = DateTime.MaxValue;
            var dataMinima = DateTime.MinValue;
            var dataAtual = DateTime.Now;
            try
            {
                if (compra.Fornecedor.IdFornecedor == 0)
                    throw new ExcecaoNegocio("[Exceção de negócio] - Obrigatório informar o id do fornecedor!");
                if (compra.IdProduto == 0)
                    throw new ExcecaoNegocio("[Exceção de negócio] - Obrigatório informar o id do produto!");
                if (compra.Funcionario.IdFuncionario == 0)
                    throw new ExcecaoNegocio("[Exceção de negócio] - Obrigatório informar o id do funcionário!");
                if (compra.DataCompra == dataMaxima || compra.DataCompra == dataMinima)
                    throw new ExcecaoData("[Exceção de Data] - Data da compra informada é inválida!");
                if (compra.DataCompra > dataAtual)
                    throw new ExcecaoData("[Exceção de Data] - Data da compra não pode ser data futura!");
                if (compra.DataValidade == dataMaxima || compra.DataValidade == dataMinima)
                    throw new ExcecaoData("[Exceção de Data] - Data da validade do produto informada é inválida!");
                if (compra.ValorCompra == 0.0)
                    throw new ExcecaoNegocio("[Exceção de negócio] - Obrigatório informar o valor da compra!");
                if (compra.QuantidadeCompra == 0)
                    throw new ExcecaoNegocio("[Exceção de negócio] - Obrigatório informar a quantidade comprada!");

                var result = await comprasRepository.AtualizarCompra(compra);
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

        public async Task<int> DeleteCompra(int idCompra)
        {
            try
            {
                if (idCompra == 0)
                    throw new ExcecaoNegocio("[Exceção de negócio] - Obrigatório informar o id da compra!");

                var result = await comprasRepository.ExcluirCompra(idCompra);
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
