using ApiSolPetsDDD.Domain.Exceptions;
using ApiSolPetsDDD.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApiSolPetsDDD.Application.Services
{
    public class CargoService : ICargoService
    {
        private readonly ICargoRepository cargoRepository;

        public CargoService(ICargoRepository cargoRepository)
        {
            this.cargoRepository = cargoRepository;
        }

        public async Task<Cargo> PostCargo(Cargo cargo)
        {
            try
            {
                cargo.NomeCargo = !string.IsNullOrEmpty(cargo.NomeCargo) ? cargo.NomeCargo.ToUpper().Trim() :
                    throw new ExcecaoNegocio("[Exceção de Negócio] - Obrigatório informar o nome do cargo!");
                cargo.Salario = cargo.Salario >= 0.0 ? cargo.Salario :
                    throw new ExcecaoNegocio("Exceção de Negócio] - Salário informado é inválido!");

                var result = await cargoRepository.CadastrarCargo(cargo);

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

        public async Task<int> PutCargo(Cargo cargo)
        {
            try
            {
                cargo.NomeCargo = !string.IsNullOrEmpty(cargo.NomeCargo) ? cargo.NomeCargo.ToUpper().Trim() :
                    throw new ExcecaoNegocio("[Exceção de Negócio] - Obrigatório informar o nome do cargo!");
                cargo.Salario = cargo.Salario >= 0.0 ? cargo.Salario :
                    throw new ExcecaoNegocio("Exceção de Negócio] - Salário informado é inválido!");

                var result = await cargoRepository.AtualizarCargo(cargo);

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

        public async Task<List<Cargo>> GetCargos()
        {
            try
            {
                var result = await cargoRepository.GetCargos();
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

        public async Task<Cargo> GetCargo(int idCargo)
        {
            try
            {
                if (idCargo == 0)
                    throw new ExcecaoNegocio("[Exceção de Negócio] - Obrigatório informar o id do cargo!");

                var result = await cargoRepository.GetCargoById(idCargo);

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

        public async Task<Cargo> GetCargoByName(string descricaoCargo)
        {
            try
            {
                if (string.IsNullOrEmpty(descricaoCargo))
                    throw new ExcecaoNegocio("[Exceção de Negócio] - Obrigatório informar a descrição do cargo!");

                var result = await cargoRepository.GetCargoByName(descricaoCargo);

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

        public async Task<int> DeleteCargo(int idCargo)
        {
            try
            {
                if (idCargo == 0)
                    throw new ExcecaoNegocio("[Exceção de Negócio] - Obrigatório informar o id do cargo!");

                var result = await cargoRepository.ExcluirCargo(idCargo);

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
