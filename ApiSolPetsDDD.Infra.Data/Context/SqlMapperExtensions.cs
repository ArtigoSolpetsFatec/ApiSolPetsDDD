using Dapper;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace ApiSolPetsDDD.Infra.Data.Context
{
    public static class SqlMapperExtensions<T> where T : class
    {
        public static void MapperByAttributtes()
        {
            SqlMapper.SetTypeMap(typeof(T), new CustomPropertyTypeMap(
                typeof(T), (type, columnName) => type.GetProperties().FirstOrDefault(
                    p => p.GetCustomAttributes(false).OfType<ColumnAttribute>().Any(
                        att => att.Name == columnName))));
        }
    }
}
