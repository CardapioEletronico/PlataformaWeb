using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RestauranteWeb.Models
{
    public class UsuarioSistema
    {
        public string Usuario { get; set; } //Usuário
        public int Restaurante_id { get; set; }
        public bool Garcom { get; set; }
        public bool GerentePedidos { get; set; }
        public bool AdminRest { get; set; }
        public bool Caixa { get; set; }
        public string Senha { get; set; }
    }
}