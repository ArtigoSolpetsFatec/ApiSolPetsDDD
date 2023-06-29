using System;

namespace ApiSolPetsDDD.Domain.Interfaces
{
    public class Cargo
    {
        public int IdCargo { get; set; }
        public string NomeCargo { get; set; }
        public double Salario { get; set; }
        public DateTime DhUltimaAtualizacao { get; set; }
    }
}
