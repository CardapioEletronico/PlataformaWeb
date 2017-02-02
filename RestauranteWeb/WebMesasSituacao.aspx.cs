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
    public partial class WebMesasSituacao : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

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
    }
}