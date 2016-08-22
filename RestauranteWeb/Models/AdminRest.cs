using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RestauranteWeb.Models
{
    public class AdminRest
    {
        public int Id { get; set; } //Usuário
        public int Restaurante_id { get; set; }
        public Restaurante Restaurante { get; set; }
        public string Senha { get; set; }
    }
}