using ApiSolPetsDDD.Domain.Interfaces;
using System;

namespace ApiSolPetsDDD.Domain.Model
{
    public class Funcionario : Cargo
    {
        public int? IdFuncionario { get; set; }
        public string NomeCompleto { get; set; }
        public string Cpf { get; set; }
        public string Rg { get; set; }
        public DateTime DhInicio { get; set; }
        public DateTime DhNascimento { get; set; }
        public int QtdeDependentes { get; set; }
        public Login Login { get; set; }
        public DateTime? DhUltimaAtualizacaoFunc { get; set; }
        public DateTime? DhInativo { get; set; }
    }
}
