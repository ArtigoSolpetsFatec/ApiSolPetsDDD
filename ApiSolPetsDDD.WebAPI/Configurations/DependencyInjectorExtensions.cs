using ApiSolPetsDDD.Infra.CrossCutting;
using Microsoft.Extensions.DependencyInjection;

namespace ApiSolPetsDDD.WebAPI.Configurations
{
    public static class DependencyInjectorExtensions
    {
        public static void AddDependencyInjection(this IServiceCollection services)
        {
            NativeInjectorBootStrapper.RegisterServices(services);
        }
    }
}
