using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace RestauranteWeb
{
    public partial class WebMesasSituacao : System.Web.UI.Page
    {
       
        private string ip = "http://10.21.0.137";

        protected async void Page_PreInit(object sender, EventArgs e)
        {
            if (Session["Login"] == null)
            {
                Response.Write("<script>window.alert('Faça seu login para acessar esse link.'); self.location = 'LoginGerentePedido.aspx';</script>)");
            }
            else if ("Caixa" == Session["Permissao"])
            {
                Label titulo = Master.FindControl("titulo") as Label;
                titulo.Text = "Visualizar Mesas";
                Label labelu = Master.FindControl("LabelUsuario") as Label;
                if (Session["Login"] != null) labelu.Text = Session["Login"].ToString();
            }
            else
            {
                Session["Login"] = null;
                Response.Write("<script>window.alert('Você não tem permissão para acessar esse link.'); self.location = 'LoginCaixa.aspx';</script>)");
            }
        }

        protected async void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                Reload();
            }
        }

        protected async void Reload()
        {
            int idRest = Convert.ToInt16(Session["idRest"]);

            HttpClient httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri(ip);

            //mesas desse restaurante
            var response = await httpClient.GetAsync("/20131011110061/api/mesa");
            var str = response.Content.ReadAsStringAsync().Result;
            List<Models.Mesa> obj = JsonConvert.DeserializeObject<List<Models.Mesa>>(str);
            List<Models.Mesa> listamesas = (from Models.Mesa c in obj where c.Restaurante_id == idRest && c.Disponivel == false select c).ToList();

            var response2 = await httpClient.GetAsync("/20131011110061/api/pedido");
            var str2 = response2.Content.ReadAsStringAsync().Result;
            List<Models.Pedido> ped = JsonConvert.DeserializeObject<List<Models.Pedido>>(str2);

            var result = from Models.Pedido pedido in ped
                         join Models.Mesa mesa in listamesas
                         on pedido.Mesa_Id equals mesa.Id
                         select mesa.comPedido(pedido);

            GridView1.DataSource = result.ToList();
            GridView1.DataBind();
        }

        protected async void GridView1_RowCommand1(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "EncerrarMesa")
            {
                int MesaId = (int)GridView1.DataKeys[Convert.ToInt32(e.CommandArgument)].Value;

                HttpClient httpClient = new HttpClient();
                httpClient.BaseAddress = new Uri(ip);

                var response = await httpClient.GetAsync("/20131011110061/api/mesa");
                var str = response.Content.ReadAsStringAsync().Result;
                List<Models.Mesa> obj = JsonConvert.DeserializeObject<List<Models.Mesa>>(str);
                Models.Mesa mesa = (from Models.Mesa f in obj where f.Id == MesaId select f).Single();

                mesa.Disponivel = true;

                var content = new StringContent(JsonConvert.SerializeObject(mesa), Encoding.UTF8, "application/json");
                await httpClient.PutAsync("/20131011110061/api/mesa/" + mesa.Id, content);

                var response2 = await httpClient.GetAsync("/20131011110061/api/pedido");
                var str2 = response2.Content.ReadAsStringAsync().Result;
                List<Models.Pedido> ped = JsonConvert.DeserializeObject<List<Models.Pedido>>(str2);
                
                foreach(Models.Pedido p in ped)
                {
                    if(p.Mesa_Id == MesaId)
                    {
                        await httpClient.DeleteAsync("20131011110061/api/pedido" + p.Id);
                    }
                }

                Reload();
            }
        }
    }
}