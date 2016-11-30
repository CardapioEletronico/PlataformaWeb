using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace RestauranteWeb
{
    public partial class WebCRUDAdminSistema : System.Web.UI.Page
    {
        private string ip = "http://10.21.0.137";

        protected void Page_PreInit(object sender, EventArgs e)
        {
            if (Session["Login"] == null)
            {
                Response.Write("<script>window.alert('Faça seu login para acessar esse link.'); self.location = 'LoginAdminSist.aspx';</script>)");
            }
            else if ("AdminSistema" == Session["Permissao"])
            {
                Label titulo = Master.FindControl("titulo") as Label;
                titulo.Text = "Gerenciamento de Administradores do Sistema";
                Label labelu = Master.FindControl("LabelUsuario") as Label;
                if (Session["Login"] != null) labelu.Text = Session["Login"].ToString();
            }
            else
            {
                Session["Login"] = null;
                Response.Write("<script>window.alert('Você não tem permissão para acessar esse link.'); self.location = 'LoginAdminSist.aspx';</script>)");
            }
        }

        protected async void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                Reload();
            }
            Label1.Text = "";
        }

        protected void btnSelect_Click(object sender, EventArgs e)
        {
            Reload();
        }

        public const int SaltByteSize = 24;
        public const int HashByteSize = 20; // to match the size of the PBKDF2-HMAC-SHA-1 hash 
        public const int Pbkdf2Iterations = 1000;
        public const int IterationIndex = 0;
        public const int SaltIndex = 1;
        public const int Pbkdf2Index = 2;

        public static string HashPassword(string password)
        {
            var cryptoProvider = new RNGCryptoServiceProvider();
            byte[] salt = new byte[SaltByteSize];
            cryptoProvider.GetBytes(salt);

            var hash = GetPbkdf2Bytes(password, salt, Pbkdf2Iterations, HashByteSize);
            return Pbkdf2Iterations + ":" + Convert.ToBase64String(salt) + ":" + Convert.ToBase64String(hash);
            //return Convert.ToBase64String(hash);
        }

        private static byte[] GetPbkdf2Bytes(string password, byte[] salt, int iterations, int outputBytes)
        {
            var pbkdf2 = new Rfc2898DeriveBytes(password, salt);
            pbkdf2.IterationCount = iterations;
            return pbkdf2.GetBytes(outputBytes);
        }


        protected async void btnInsert_Click(object sender, EventArgs e)
        {
            HttpClient httpClient = new HttpClient();

            httpClient.BaseAddress = new Uri(ip);

            var response2 = await httpClient.GetAsync("/20131011110061/api/adminsistema");
            var str2 = response2.Content.ReadAsStringAsync().Result;
            List<Models.AdminSistema> obj2 = JsonConvert.DeserializeObject<List<Models.AdminSistema>>(str2);
            var obj = (from Models.AdminSistema a in obj2 orderby a.Usuario select a).ToList();

            if (obj.Any(x => x.Usuario.Contains(textBoxUsuario.Text)))
            {
                Label1.Text = "Esse usuário já existe.";
            }

            else
            {
                string senha = HashPassword(textBoxSenha.Text);
                Models.AdminSistema f = new Models.AdminSistema
                {
                    Usuario = textBoxUsuario.Text,
                    Senha = senha,
                };

                string s = JsonConvert.SerializeObject(f);

                var content = new StringContent(s, Encoding.UTF8, "application/json");

                await httpClient.PostAsync("/20131011110061/api/adminsistema", content);

                Label1.Text = "";

                Reload();
            }
        }

        public async void Reload()
        {
            HttpClient httpClient = new HttpClient();

            httpClient.BaseAddress = new Uri(ip);

            var response2 = await httpClient.GetAsync("/20131011110061/api/adminsistema");
            var str2 = response2.Content.ReadAsStringAsync().Result;
            List<Models.AdminSistema> obj2 = JsonConvert.DeserializeObject<List<Models.AdminSistema>>(str2);
            var obj = (from Models.AdminSistema a in obj2 orderby a.Usuario select a).ToList();

            GridView1.DataSource = obj2;
            GridView1.DataBind();
        }


        protected void OnRowEditing(object sender, GridViewEditEventArgs e)
        {
            GridView1.EditIndex = e.NewEditIndex;
            Reload();
            GridView1.DataBind();
        }

        protected async void OnRowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            GridViewRow row = GridView1.Rows[e.RowIndex];
            string Id = Convert.ToString(GridView1.DataKeys[e.RowIndex].Values[0]);

            HttpClient httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri(ip);

            var response = await httpClient.GetAsync("/20131011110061/api/adminsistema");
            var str = response.Content.ReadAsStringAsync().Result;
            List<Models.AdminSistema> obj = JsonConvert.DeserializeObject<List<Models.AdminSistema>>(str);
            Models.AdminSistema item = (from Models.AdminSistema f in obj where f.Usuario == Id select f).Single();

            item.Usuario = (row.FindControl("txtUsuario") as TextBox).Text;

            var content = new StringContent(JsonConvert.SerializeObject(item), Encoding.UTF8, "application/json");
            await httpClient.PutAsync("/20131011110061/api/adminsistema/" + Id, content);
            GridView1.EditIndex = -1;
            GridView1.DataBind();

            Reload();

        }

        protected void OnRowCancelingEdit(object sender, EventArgs e)
        {
            GridView1.EditIndex = -1;
            Reload();
            GridView1.DataBind();
        }


        protected async void OnRowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            string Id = Convert.ToString(GridView1.DataKeys[e.RowIndex].Values[0]);

            HttpClient httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri(ip);
            await httpClient.DeleteAsync("/20131011110061/api/adminsistema/" + Id);

            Reload();
        }

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                LinkButton l = (LinkButton)e.Row.FindControl("LinkButton1");
                l.Attributes.Add("onclick", "javascript:return " + "confirm('Deseja deletar " +
                DataBinder.Eval(e.Row.DataItem, "Usuario") + "'?)");
            }
        }

        protected async void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Deletar")
            {
                string Id = Convert.ToString(e.CommandArgument);

                HttpClient httpClient = new HttpClient();
                httpClient.BaseAddress = new Uri(ip);
                await httpClient.DeleteAsync("/20131011110061/api/adminsistema/" + Id);

                Reload();
            }
        }
    }
}