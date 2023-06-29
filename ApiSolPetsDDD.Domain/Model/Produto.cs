using System;

namespace ApiSolPetsDDD.Domain.Model
{
    public class Produto : Categoria
    {
        public int IdProduto { get; set; }
        public string IsbnProduto { get; set; }
        public string NomeProduto { get; set; }
        public string MarcaProduto { get; set; }
        public double ValorUnitarioCusto { get; set; }
        public double ValorUnitarioVenda { get; set; }
        public int QtdeEstoque { get; set; }
        public Fornecedor Fornecedor { get; set; }
        public double PesoProduto { get; set; }
        public DateTime DhUltimaAtualizacao { get; set; }
        public DateTime DataValidade { get; set; }
        public int IdadeAplicavel { get; set; }
        public double PesoAplicavel { get; set; }
        public string FotoProduto { get; set; }
        public int? QtdeProduto { get; set; }
        public bool ValorVendaEditavel { get; set; }
        public double ValorProdutoPedido { get; set; }
    }
}
