using System;

namespace ApiSolPetsDDD.Domain.Model
{
    public class Compra : Produto
    {
        public int IdCompra { get; set; }
        public DateTime DataCompra { get; set; }
        public double ValorCompra { get; set; }
        public int QuantidadeCompra { get; set; }
        public Funcionario Funcionario { get; set; }
    }
}
