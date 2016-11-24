using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RestauranteWeb.Models
{
    public class Produto
    {
        public int Id { get; set; }
        public string Descricao { get; set; }
        public double Preco { get; set; }
        public int Cardapio_id { get; set; }
        public int Fila_id { get; set; }
        public string Foto { get; set; }
        public string NomeDescricao { get; set; }
        public string ArquivoFoto { get; set; }
    }
}