using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RestauranteWeb.Models
{
    public class Cardapio
    {
        public int Id { get; set; }
        public string Descricao { get; set; }
        public int Id_Restaurante { get; set; }
    }
}