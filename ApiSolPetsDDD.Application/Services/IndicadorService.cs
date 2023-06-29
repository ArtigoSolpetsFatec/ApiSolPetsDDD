using ApiSolPetsDDD.Domain.Exceptions;
using ApiSolPetsDDD.Domain.Interfaces;
using ApiSolPetsDDD.Domain.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApiSolPetsDDD.Application.Services
{
    public class IndicadorService : IIndicadorService
    {
        private readonly IIndicadorRepository indicadorRepository;

        public IndicadorService(IIndicadorRepository indicadorRepository)
        {
            this.indicadorRepository = indicadorRepository;
        }

        public async Task<List<Indicador>> GetIndicadoresByMeses(int meses)
        {
            var result = new List<Indicador>();
            try
            {
                if (meses == 0)
                    throw new ExcecaoNegocio("Obrigatório informar o mês da consulta!");
                else if (meses == 12)
                {
                    result = await indicadorRepository.GetIndicadoresAno();
                }
                else if (meses == 1)
                {
                    result = await indicadorRepository.GetIndicadoresMes();
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

        public async Task<List<Indicador>> GetIndicadoresDia()
        {
            try
            {
                var result = await indicadorRepository.GetIndicadoresDia();
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
