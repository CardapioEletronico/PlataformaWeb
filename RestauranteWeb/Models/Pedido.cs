using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RestauranteWeb.Models
{
    public class Pedido
    {
        public int Id { get; set; }
        public double ValorTotal { get; set; }
        public DateTime Data { get; set; }
        public string Cliente { get; set; }
        public int Mesa_Id { get; set; }
    }
}