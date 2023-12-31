﻿using System;

namespace ApiSolPetsDDD.Domain.Model
{
    public class Contato
    {
        public int IdContato { get; set; }
        public int IdCliente { get; set; }
        public int IdFornecedor { get; set; }
        public string EmailPrincipal { get; set; }
        public string EmailSecundario { get; set; }
        public string TelefoneFixo { get; set; }
        public string TelefoneCelular { get; set; }
        public string OutroCelular { get; set; }
        public DateTime DHUltimaAtualizacao { get; set; }

    }
}
