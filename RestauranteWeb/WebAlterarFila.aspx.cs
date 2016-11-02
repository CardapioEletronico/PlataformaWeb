using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace RestauranteWeb
{
    public partial class WebAlterarFila : System.Web.UI.Page
    {
        private string ip = "http://10.21.0.137";

        protected void Page_PreInit(object sender, EventArgs e)
        {
            if (Session["Login"] == null)
            {
                Response.Write("<script>window.alert('Faça seu login para acessar esse link.'); self.location = 'LoginGerentePedido.aspx';</script>)");
            }
            else if ("GerentePedidos" == Session["Permissao"])
            {
                Label titulo = Master.FindControl("titulo") as Label;
                titulo.Text = "Alterar Fila";
                Label labelu = Master.FindControl("LabelUsuario") as Label;
                if (Session["Login"] != null) labelu.Text = Session["Login"].ToString();
            }
            else
            {
                Session["Login"] = null;
                Response.Write("<script>window.alert('Você não tem permissão para acessar esse link.'); self.location = 'LoginGerentePedido.aspx';</script>)");
            }
        }

        protected async void Page_Load(object sender, EventArgs e)
        {
            int idFila = Convert.ToInt16(Session["Fila"]);
            DropFila();

            HttpClient httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri(ip);
            var response3 = await httpClient.GetAsync("/20131011110061/api/fila");
            var str3 = response3.Content.ReadAsStringAsync().Result;
            List<Models.Fila> c = JsonConvert.DeserializeObject<List<Models.Fila>>(str3);
            var listafila = (from Models.Fila card in c where card.Id == idFila select card).Single();

            FilaLabel.Text = listafila.Descricao.ToString();
        }

        public async void DropFila()
        {
            int idRest = Convert.ToInt16(Session["idRest"]);

            HttpClient httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri(ip);
            var response3 = await httpClient.GetAsync("/20131011110061/api/cardapio");
            var str3 = response3.Content.ReadAsStringAsync().Result;
            List<Models.Cardapio> c = JsonConvert.DeserializeObject<List<Models.Cardapio>>(str3);
            var listaCardapio = from Models.Cardapio card in c where card.Restaurante_id == idRest select card;
            var response2 = await httpClient.GetAsync("/20131011110061/api/fila");
            var str2 = response2.Content.ReadAsStringAsync().Result;
            List<Models.Fila> fil = JsonConvert.DeserializeObject<List<Models.Fila>>(str2);

            List<Models.Fila> listafila = new List<Models.Fila>();
            foreach (Models.Cardapio c1 in listaCardapio)
            {
                listafila = (from Models.Fila f in fil where f.Cardapio_id == c1.Id select f).ToList();
            }

            Filas.DataSource = listafila;
            Filas.DataTextField = "Descricao";
            Filas.DataValueField = "Id";
            Filas.DataBind();
        }

        protected void btnTrocar_Click(object sender, EventArgs e)
        {
            Session["Fila"] = Filas.SelectedValue;
            FilaLabel.Text = Filas.SelectedItem.Text;
        }

    }
}