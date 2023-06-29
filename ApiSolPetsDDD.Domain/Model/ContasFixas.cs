using System;

namespace ApiSolPetsDDD.Domain.Model
{
    public class ContasFixas
    {
        public int IdContas { get; set; }
        public string TipoConta { get; set; }
        public string Empresa { get; set; }
        public double ValorConta { get; set; }
        public DateTime DataVencimento { get; set; }
    }
}
