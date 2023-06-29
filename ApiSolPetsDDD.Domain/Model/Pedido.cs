using APISolPets.Domain.Models;
using System;
using System.Collections.Generic;

namespace ApiSolPetsDDD.Domain.Model
{
    public class Pedido
    {
        public Pedido()
        {
            Produtos = new List<Produto>();
        }

        public int IdPedido { get; set; }
        public DateTime DhVenda { get; set; }
        public char StatusVenda { get; set; }
        public bool Entregar { get; set; }
        public Cliente Cliente { get; set; }
        public double TotalVenda { get; set; }
        public int QtdeProdutos { get; set; }
        public bool Finalizado { get; set; }
        public List<Produto> Produtos { get; set; }
        public List<PedidoProduto> Pedidos { get; set; }
        public double ValorDesconto { get; set; }
    }


}
