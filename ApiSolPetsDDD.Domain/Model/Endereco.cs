using System;

namespace ApiSolPetsDDD.Domain.Model
{
    public class Endereco
    {
        public int IdCliente { get; set; }
        public int IdEndreco { get; set; }
        public string Bairro { get; set; }
        public string Cep { get; set; }
        public string Complemento { get; set; }
        public string Logradouro { get; set; }
        public int Numero { get; set; }
        public string Cidade { get; set; }
        public string UfEstado { get; set; }
        public DateTime DHUltimaAtualizacao { get; set; }

    }
}
