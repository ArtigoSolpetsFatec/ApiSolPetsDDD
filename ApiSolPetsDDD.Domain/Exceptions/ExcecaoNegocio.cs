using System;

namespace ApiSolPetsDDD.Domain.Exceptions
{
    [Serializable]
    public class ExcecaoNegocio : Exception
    {
        public ExcecaoNegocio() : base() { }
        public ExcecaoNegocio(string message) : base(message) { }
    }
}
