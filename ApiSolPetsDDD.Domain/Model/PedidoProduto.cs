namespace ApiSolPetsDDD.Domain.Model
{
    public class PedidoProduto
    {
        public Produto Produto { get; set; }
        public int QtdeProdutos { get; set; }
        public bool Finalizado { get; set; }
        public double? TotalVenda { get; set; }
        public int? IdPedido { get; set; }
    }
}
