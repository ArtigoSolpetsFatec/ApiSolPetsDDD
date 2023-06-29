using APISolPets.Domain.Models;
using ApiSolPetsDDD.Infra.Data.Context;

namespace ApiSolPetsDDD.Infra.CrossCutting
{
    public static class SqlMapperBootStrapper
    {
        public static void MapperByAtributtes()
        {
            SqlMapperExtensions<Cliente>.MapperByAttributtes();
        }
    }
}
