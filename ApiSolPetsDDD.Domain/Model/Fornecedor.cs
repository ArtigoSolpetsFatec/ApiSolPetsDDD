using System.Collections.Generic;

namespace ApiSolPetsDDD.Domain.Model
{
    public class Fornecedor
    {
        public int IdFornecedor { get; set; }
        public string NomeFornecedor { get; set; }
        public string CnpjFornecedor { get; set; }
        public List<Contato> Contatos { get; set; }
    }
}
