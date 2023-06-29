using ApiSolPetsDDD.Infra.CrossCutting;

namespace ApiSolPetsDDD.WebAPI.Configurations
{
    public static class SqlMapperExtensions
    {

        public static void MapperByAttributtes()
        {
            SqlMapperBootStrapper.MapperByAtributtes();
        }
    }
}
