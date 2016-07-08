using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RestauranteWeb.Models
{
    public class Fila
    {
        public int Id { get; set; }
        public string Descricao { get; set; }
        public int Restaurante_id { get; set; }
    }
}