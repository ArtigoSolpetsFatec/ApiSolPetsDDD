using System;

namespace ApiSolPetsDDD.Domain.Model
{
    public class Pet
    {
        public int IdCliente { get; set; }
        public int IdPet { get; set; }
        public string NomePet { get; set; }
        public string RacaPet { get; set; }
        public string TipoAnimalPet { get; set; }
        public DateTime DataNascimentoPet { get; set; }
        public DateTime DHUltimaAtualizacao { get; set; }
    }
}
