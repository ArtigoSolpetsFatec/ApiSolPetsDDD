using System;

namespace ApiSolPetsDDD.Domain.Model
{
    public class PagamentoContasFixas : ContasFixas
    {
        public int IdPagamento { get; set; }
        public DateTime DataPagamento { get; set; }
        public Funcionario Funcionario { get; set; }
    }
}
