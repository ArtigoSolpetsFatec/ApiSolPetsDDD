using APISolPets.Domain.Extensions;
using ApiSolPetsDDD.Domain.Exceptions;
using ApiSolPetsDDD.Domain.Interfaces;
using ApiSolPetsDDD.Domain.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApiSolPetsDDD.Application.Services
{
    public class CategoriaService : ICategoriaService
    {
        private readonly ICategoriaRepository categoriaRepository;

        public CategoriaService(ICategoriaRepository categoriaRepository)
        {
            this.categoriaRepository = categoriaRepository;
        }

        public async Task<Categoria> GetCategoriaById(int idCategoria)
        {
            try
            {
                if (idCategoria == 0)
                    throw new ExcecaoNegocio("[Exceção de Negócio] - Obrigatório informar o id da categoria");

                var result = await categoriaRepository.GetCategoriaById(idCategoria);
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

        public async Task<Categoria> GetCategoriaByName(string nomeCategoria)
        {
            try
            {
                if (string.IsNullOrEmpty(nomeCategoria))
                    throw new ExcecaoNegocio("[Exceção de Negócio] - Obrigatório informar o nome da categoria");

                var result = await categoriaRepository.GetCategoriaByName(nomeCategoria);
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

        public async Task<List<Categoria>> GetAllCategorias()
        {
            try
            {
                var result = await categoriaRepository.GetAllCategorias();
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Categoria> PostCategoria(Categoria categoria)
        {
            try
            {
                categoria.DescricaoCategoria = !string.IsNullOrEmpty(categoria.DescricaoCategoria) ? categoria.DescricaoCategoria.ToUpper() :
                    throw new ExcecaoNegocio("[Exceção de Negócio] - Obrigatório informar a descrição da categoria");
                categoria.DescricaoCategoria = categoria.DescricaoCategoria.Trim();

                categoria.TipoAnimal = !string.IsNullOrEmpty(categoria.TipoAnimal) ? categoria.TipoAnimal.ToUpper() :
                    throw new ExcecaoNegocio("[Exceção de Negócio] - Obrigatório informar o tipo de animal da categoria");
                categoria.TipoAnimal = categoria.TipoAnimal.Trim();
                categoria.TipoAnimal = categoria.TipoAnimal.RemoveAcentos();

                categoria.TipoCategoria = !string.IsNullOrEmpty(categoria.TipoCategoria) ? categoria.TipoCategoria.ToUpper() :
                    throw new ExcecaoNegocio("[Exceção de Negócio] - Obrigatório informar o tipo da categoria");
                categoria.TipoCategoria = categoria.TipoCategoria.Trim();
                categoria.TipoCategoria = categoria.TipoCategoria.RemoveAcentos();

                var result = await categoriaRepository.CadastrarCategoria(categoria);
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

        public async Task<int> PutCategoria(Categoria categoria)
        {
            try
            {
                categoria.IdCategoria = categoria.IdCategoria > 0 ? categoria.IdCategoria :
                   throw new ExcecaoNegocio("[Exceção de Negócio] - Obrigatório informar o id da categoria");
                categoria.DescricaoCategoria = !string.IsNullOrEmpty(categoria.DescricaoCategoria) ? categoria.DescricaoCategoria.ToUpper() :
                   throw new ExcecaoNegocio("[Exceção de Negócio] - Obrigatório informar a descrição da categoria");
                categoria.DescricaoCategoria = categoria.DescricaoCategoria.Trim();
                categoria.DescricaoCategoria = categoria.DescricaoCategoria.RemoveAcentos();

                categoria.TipoAnimal = !string.IsNullOrEmpty(categoria.TipoAnimal) ? categoria.TipoAnimal.ToUpper() :
                    throw new ExcecaoNegocio("[Exceção de Negócio] - Obrigatório informar o tipo de animal da categoria");
                categoria.TipoAnimal = categoria.TipoAnimal.Trim();
                categoria.TipoAnimal = categoria.TipoAnimal.RemoveAcentos();

                categoria.TipoCategoria = !string.IsNullOrEmpty(categoria.TipoCategoria) ? categoria.TipoCategoria.ToUpper() :
                    throw new ExcecaoNegocio("[Exceção de Negócio] - Obrigatório informar o tipo da categoria");
                categoria.TipoCategoria = categoria.TipoCategoria.Trim();
                categoria.TipoCategoria = categoria.TipoCategoria.RemoveAcentos();

                var result = await categoriaRepository.AtualizarInfoCategoria(categoria);
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

        public async Task<int> DeleteCategoria(int idCategoria)
        {
            try
            {
                if (idCategoria == 0)
                    throw new ExcecaoNegocio("[Exceção de Negócio] - Obrigatório informar o id da categoria");

                var result = await categoriaRepository.ExcluirCategoria(idCategoria);
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
