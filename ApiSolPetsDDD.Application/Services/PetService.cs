using ApiSolPetsDDD.Domain.Exceptions;
using ApiSolPetsDDD.Domain.Interfaces;
using ApiSolPetsDDD.Domain.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApiSolPetsDDD.Application.Services
{
    public class PetService : IPetService
    {
        private readonly IClienteRepository clienteRepository;

        public PetService(IClienteRepository clienteRepository)
        {
            this.clienteRepository = clienteRepository;
        }

        public async Task<List<Pet>> GetPetByNome(string nomePet)
        {
            try
            {
                var result = await clienteRepository.GetPetNome(nomePet);
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

        public async Task<Pet> PostPet(Pet pet, int idCliente)
        {
            try
            {
                var result = await clienteRepository.CadastrarPetCliente(pet, idCliente);
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

        public async Task<int> PutPet(Pet petCliente)
        {
            try
            {
                var result = await clienteRepository.AtualizaPetCliente(petCliente);
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

        public async Task<int> DeletePet(int idCliente, int idPet)
        {
            try
            {
                var result = await clienteRepository.ExcluirPetCliente(idCliente, idPet);
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
