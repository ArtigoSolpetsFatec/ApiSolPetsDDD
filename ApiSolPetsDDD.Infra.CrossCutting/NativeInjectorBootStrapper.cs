using APISolPets.Domain.Interfaces;
using APISolPets.Infra.Data.DataContext;
using ApiSolPetsDDD.Application.Services;
using ApiSolPetsDDD.Domain.Factory;
using ApiSolPetsDDD.Domain.Interfaces;
using ApiSolPetsDDD.Infra.Data.Repository;
using Microsoft.Extensions.DependencyInjection;

namespace ApiSolPetsDDD.Infra.CrossCutting
{
    public static class NativeInjectorBootStrapper
    {
        public static void RegisterServices(IServiceCollection services)
        {
            // Service
            services.AddScoped<IClienteService, ClienteService>();
            services.AddScoped<IPetService, PetService>();
            services.AddScoped<IContatoService, ContatoService>();
            services.AddScoped<IEnderecoService, EnderecoService>();
            services.AddScoped<IFornecedorService, FornecedorService>();
            services.AddScoped<IComprasService, ComprasService>();
            services.AddScoped<IFuncionarioService, FuncionarioService>();
            services.AddScoped<ILoginService, LoginService>();
            services.AddScoped<ICargoService, CargoService>();
            services.AddScoped<IContasService, ContasService>();
            services.AddScoped<IProdutoService, ProdutoService>();
            services.AddScoped<ICategoriaService, CategoriaService>();
            services.AddScoped<IPedidoService, PedidoService>();
            services.AddScoped<IUserAuthenticatorService, UserAuthenticatorService>();
            services.AddScoped<IGeradorSenhaService, GeradorSenhaService>();
            services.AddScoped<IIndicadorService, IndicadorService>();

            // Context
            services.AddScoped<IDbConFactory, DbConnectionFactory>();
            services.AddScoped<ISqlComWrapperFac, SqlComWrapperFac>();
            services.AddScoped<IClienteRepository, ClienteRepository>();
            services.AddScoped<IContatoRepository, ContatoRepository>();
            services.AddScoped<IFornecedorRepository, FornecedorRepository>();
            services.AddScoped<IComprasRepository, ComprasRepository>();
            services.AddScoped<IFuncionarioRepository, FuncionarioRepository>();
            services.AddScoped<ILoginRepository, LoginRepository>();
            services.AddScoped<ICargoRepository, CargoRepository>();
            services.AddScoped<IContasRepository, ContasRepository>();
            services.AddScoped<IProdutoRepository, ProdutoRepository>();
            services.AddScoped<ICategoriaRepository, CategoriaRepository>();
            services.AddScoped<IPedidoRepository, PedidoRepository>();
            services.AddScoped<IUserAuthenticatorRepository, UserAuthenticatorRepository>();
            services.AddScoped<IIndicadorRepository, IndicadorRepository>();
        }
    }
}
