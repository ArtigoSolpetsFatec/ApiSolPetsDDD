using ApiSolPetsDDD.Domain.Model;

namespace ApiSolPetsDDD.Domain.Interfaces
{
    public interface IGeradorSenhaService
    {
        SenhaModel GetSenha();
    }
}
