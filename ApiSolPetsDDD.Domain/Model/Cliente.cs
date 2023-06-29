using ApiSolPetsDDD.Domain.Model;
using System;
using System.Collections.Generic;

namespace APISolPets.Domain.Models
{
    public class Cliente
    {
        public int IdCliente { get; set; }
        public string NomeCliente { get; set; }
        public string CpfCliente { get; set; }
        public string CnpjCliente { get; set; }
        public char SexoCliente { get; set; }
        public string NomeEmpresaCliente { get; set; }
        public string RgCliente { get; set; }
        public string UfRg { get; set; }
        public DateTime DataNascimentoCliente { get; set; }
        public DateTime DHUltimaAtualizacao { get; set; }
        public List<Pet> PetsCliente { get; set; }
        public List<Contato> ContatosCliente { get; set; }
        public List<Endereco> EnderecosCliente { get; set; }
    }
}
