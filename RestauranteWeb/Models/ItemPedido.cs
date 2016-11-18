using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RestauranteWeb.Models
{
    public class ItemPedido
    {
        public int Id { get; set; }
        public int Quantidade { get; set; }
        public DateTime Hora { get; set; }
        public int Situacao { get; set; }
        public int Produto_Id { get; set; }
        public int Pedido_Id { get; set; }

        public Produto Produto { get; set; }

        public ItemPedido ComProduto(Produto p)
        {
            this.Produto = p;
            return this;
        }
    }
}