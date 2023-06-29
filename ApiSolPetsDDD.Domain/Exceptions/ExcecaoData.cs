using System;

namespace ApiSolPetsDDD.Domain.Exceptions
{
    [Serializable]
    public class ExcecaoData : Exception
    {
        public ExcecaoData() : base() { }
        public ExcecaoData(string message) : base(message) { }
    }
}
