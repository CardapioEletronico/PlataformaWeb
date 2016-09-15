using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace RestauranteWeb.AdmRest
{
    public partial class LoginAdmRest : System.Web.UI.Page
    {
        private string ip = "http://10.21.0.137";

        protected void Page_Load(object sender, EventArgs e)
        {
            
            
        }
        protected async void login_click(object Source, EventArgs e)
        {
            HttpClient httpClient = new HttpClient();

            httpClient.BaseAddress = new Uri(ip);
            var response = await httpClient.GetAsync("/20131011110061/api/adminrest");

            var str = response.Content.ReadAsStringAsync().Result;
            List<Models.AdminRest> obj = JsonConvert.DeserializeObject<List<Models.AdminRest>>(str);


            foreach (Models.AdminRest x in obj)
            {
                if(x.Usuario == TextBoxUsuario.Text && x.Senha == TextBoxSenha.Text)
                {
                    Session["Login"] = TextBoxUsuario.Text;
                    Session["s"] = TextBoxSenha.Text;
                    Session["idRest"] = x.Restaurante_id;
                    Response.Write("<script>window.alert('Logado com sucesso!'); self.location='WebCRUDCardapio.aspx'; </script>");
                    return;
                }
            }
            Response.Write("<script>window.alert('Usuário não encontrado'); </script>");
        }
    }
}