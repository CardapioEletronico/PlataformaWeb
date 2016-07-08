using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RestauranteWeb.Models
{
    public class Mesa
    {
        public int Id { get; set; }
        public string Numero { get; set; }
        public bool Disponivel { get; set; }
        public int Restaurante_id { get; set; }
    }
}