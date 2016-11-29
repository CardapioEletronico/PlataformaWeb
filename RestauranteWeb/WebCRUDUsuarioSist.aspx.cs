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
    public partial class WebCRUDAdminRest : System.Web.UI.Page
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
                titulo.Text = "Gerenciamento de Usuários do Sistema";
                Label labelu = Master.FindControl("LabelUsuario") as Label;
                if (Session["Login"] != null) labelu.Text = Session["Login"].ToString();
            }
            else
            {
                Response.Write("<script>window.alert('Você não tem permissão para acessar esse link.'); self.location = 'LoginAdminSist.aspx';</script>)");
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                Reload();
            }
        }

        public async void DropRest()
        {
            HttpClient httpClient = new HttpClient();

            httpClient.BaseAddress = new Uri(ip);
            var response = await httpClient.GetAsync("/20131011110061/api/restaurante");
            var str = response.Content.ReadAsStringAsync().Result;

            List<Models.Restaurante> obj = JsonConvert.DeserializeObject<List<Models.Restaurante>>(str);

            Restaurantes.DataSource = obj;
            Restaurantes.DataTextField = "Descricao";
            Restaurantes.DataValueField = "Id";
            Restaurantes.DataBind();


            RestaurantesSelect.DataSource = obj;
            RestaurantesSelect.DataTextField = "Descricao";
            RestaurantesSelect.DataValueField = "Id";
            RestaurantesSelect.DataBind();

        }

        protected async void btnSelect_Click(object sender, EventArgs e)
        {
            HttpClient httpClient = new HttpClient();

            httpClient.BaseAddress = new Uri(ip);

            var response2 = await httpClient.GetAsync("/20131011110061/api/usuariosistema");
            var str2 = response2.Content.ReadAsStringAsync().Result;
            List<Models.UsuarioSistema> obj2 = JsonConvert.DeserializeObject<List<Models.UsuarioSistema>>(str2);

            var obj = (from Models.UsuarioSistema r in obj2 where r.Restaurante_id == int.Parse(RestaurantesSelect.SelectedValue) orderby r.Usuario select r).ToList();

            var response3 = await httpClient.GetAsync("/20131011110061/api/restaurante");
            var str3 = response3.Content.ReadAsStringAsync().Result;
            List<Models.Restaurante> obj3 = JsonConvert.DeserializeObject<List<Models.Restaurante>>(str3);

            var result = from Models.UsuarioSistema us in obj
                         join Models.Restaurante p in obj3
                         on us.Restaurante_id equals p.Id
                         select us.ComRestaurante(p);

            GridView1.DataSource = result.ToList();
            GridView1.DataBind();
            DropRest();
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
            string cu = HashPassword(textBoxSenha.Text);
            httpClient.BaseAddress = new Uri(ip);

            Models.UsuarioSistema f = new Models.UsuarioSistema
            {
                Usuario = textBoxUsuario.Text.ToString(),
                Senha = cu,
                Garcom = Convert.ToBoolean(Garçom.Checked),
                AdminRest = Convert.ToBoolean(AdminRest.Checked),
                GerentePedidos = Convert.ToBoolean(GerentePedidos.Checked),
                Caixa = Convert.ToBoolean(Caixa.Checked),
                Restaurante_id = int.Parse(Restaurantes.SelectedItem.Value)
            };

            List<Models.UsuarioSistema> fl = new List<Models.UsuarioSistema>();

            fl.Add(f);

            string s = "=" + JsonConvert.SerializeObject(fl, Newtonsoft.Json.Formatting.None,
                            new JsonSerializerSettings
                            {
                                NullValueHandling = NullValueHandling.Ignore
                            });

            var content = new StringContent(s, Encoding.UTF8, "application/x-www-form-urlencoded");

            HttpResponseMessage message = await httpClient.PostAsync("/20131011110061/api/usuariosistema", content);

            Reload();
        }

        public async void Reload()
        {
            HttpClient httpClient = new HttpClient();

            httpClient.BaseAddress = new Uri(ip);

            var response2 = await httpClient.GetAsync("/20131011110061/api/usuariosistema");
            var str2 = response2.Content.ReadAsStringAsync().Result;
            List<Models.UsuarioSistema> obj2 = JsonConvert.DeserializeObject<List<Models.UsuarioSistema>>(str2);

            var obj = (from Models.UsuarioSistema r in obj2 orderby r.Usuario select r).ToList();

            var response3 = await httpClient.GetAsync("/20131011110061/api/restaurante");
            var str3 = response3.Content.ReadAsStringAsync().Result;
            List<Models.Restaurante> obj3 = JsonConvert.DeserializeObject<List<Models.Restaurante>>(str3);

            var result = from Models.UsuarioSistema us in obj
                         join Models.Restaurante p in obj3
                         on us.Restaurante_id equals p.Id
                         select us.ComRestaurante(p);

            GridView1.DataSource = result.ToList();
            GridView1.DataBind();
            DropRest();

        }

        protected async void OnRowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            string Id = Convert.ToString(GridView1.DataKeys[e.RowIndex].Values[0]);

            HttpClient httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri(ip);
            await httpClient.DeleteAsync("/20131011110061/api/usuariosistema/" + Id);

            Reload();
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
            string Usuario = Convert.ToString(GridView1.DataKeys[e.RowIndex].Values[0]);

            HttpClient httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri(ip);

            var response = await httpClient.GetAsync("/20131011110061/api/usuariosistema");
            var str = response.Content.ReadAsStringAsync().Result;
            List<Models.UsuarioSistema> obj = JsonConvert.DeserializeObject<List<Models.UsuarioSistema>>(str);
            Models.UsuarioSistema item = (from Models.UsuarioSistema f in obj where f.Usuario == Usuario select f).Single();

            item.Usuario = (row.FindControl("txtUsuario") as TextBox).Text;
            item.AdminRest = Convert.ToBoolean((row.FindControl("AdminRestCheck") as CheckBox).Checked);
            item.Garcom = Convert.ToBoolean((row.FindControl("GarcomCheck") as CheckBox).Checked);
            item.Caixa = Convert.ToBoolean((row.FindControl("CaixaCheck") as CheckBox).Checked);
            item.GerentePedidos = Convert.ToBoolean((row.FindControl("GerPedidosCheck") as CheckBox).Checked);
            int valor = Convert.ToInt32((row.FindControl("Restaurante") as DropDownList).SelectedValue);
            item.Restaurante_id = Convert.ToInt32((row.FindControl("Restaurante") as DropDownList).SelectedValue);

            var content = new StringContent(JsonConvert.SerializeObject(item), Encoding.UTF8, "application/json");
            await httpClient.PutAsync("/20131011110061/api/usuariosistema/" + Usuario, content);

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

        protected async void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow && GridView1.EditIndex == e.Row.RowIndex)
            {
                DropDownList RestauranteDropDown = (DropDownList)e.Row.FindControl("Restaurante");
                HttpClient httpClient = new HttpClient();
                httpClient.BaseAddress = new Uri(ip);
                var response = await httpClient.GetAsync("/20131011110061/api/restaurante");
                var str = response.Content.ReadAsStringAsync().Result;
                List<Models.Restaurante> obj = JsonConvert.DeserializeObject<List<Models.Restaurante>>(str);
                RestauranteDropDown.DataSource = obj;
                RestauranteDropDown.DataTextField = "Descricao";
                RestauranteDropDown.DataValueField = "Id";
                RestauranteDropDown.DataBind();
            }

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                LinkButton l = (LinkButton)e.Row.FindControl("LinkButton1");
                l.Attributes.Add("onclick", "javascript:return " +
                "confirm('Deseja deletar " +
                DataBinder.Eval(e.Row.DataItem, "Usuario") + "'?)");
            }
        }

        protected async void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Delete")
            {
                string Id = Convert.ToString(e.CommandArgument);

                HttpClient httpClient = new HttpClient();
                httpClient.BaseAddress = new Uri(ip);
                await httpClient.DeleteAsync("/20131011110061/api/usuariosistema/" + Id);
  
            }
            Reload();
        }
    }
}