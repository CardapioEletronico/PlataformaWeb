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
                titulo.Text = "Gerenciamento de Administradores de Restaurante";
                Label labelu = Master.FindControl("LabelUsuario") as Label;
                if (Session["Login"] != null) labelu.Text = Session["Login"].ToString();
            }
            else
            {
                Session["Login"] = null;
                Response.Write("<script>window.alert('Você não tem permissão para acessar esse link.'); self.location = 'LoginAdminSist.aspx';</script>)");
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            Table1.Rows.Clear();
            Reload();
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

        protected async void btnUpdate_Click(object sender, EventArgs e)
        {
     
            HttpClient httpClient = new HttpClient();

            httpClient.BaseAddress = new Uri(ip);
            Models.UsuarioSistema f = new Models.UsuarioSistema
            {
                Usuario  = textBoxUsuario.Text,
                Senha = textBoxSenha.Text,
                Garcom = Convert.ToBoolean(Garçom.Checked),
                AdminRest = Convert.ToBoolean(AdminRest.Checked),
                GerentePedidos = Convert.ToBoolean(GerentePedidos.Checked),
                Caixa = Convert.ToBoolean(Caixa.Checked),
                Restaurante_id = int.Parse(Restaurantes.SelectedValue)
            };

            string s = "=" + JsonConvert.SerializeObject(f);

            var content = new StringContent(s, Encoding.UTF8, "application/x-www-form-urlencoded");
            
            await httpClient.PutAsync("/20131011110061/api/usuariosistema/" + textBoxUsuario.Text, content);

            Reload();
        }

        protected async void btnDelete_Click(object sender, EventArgs e)
        {
            HttpClient httpClient = new HttpClient();

            httpClient.BaseAddress = new Uri(ip);

            await httpClient.DeleteAsync("/20131011110061/api/usuariosistema/" + textBoxUsuario.Text);
            Reload();

        }

        public async void Reload()
        {
            HttpClient httpClient = new HttpClient();

            httpClient.BaseAddress = new Uri(ip);

            var response2 = await httpClient.GetAsync("/20131011110061/api/usuariosistema");
            var str2 = response2.Content.ReadAsStringAsync().Result;
            List<Models.UsuarioSistema> obj2 = JsonConvert.DeserializeObject<List<Models.UsuarioSistema>>(str2);

            Table1.Rows.Clear();

            TableHeaderRow th = new TableHeaderRow();
            TableHeaderCell thc = new TableHeaderCell();
            thc.Text = "Usuários do Sistema";
            TableHeaderCell thc1 = new TableHeaderCell();
            thc1.Text = "Garçom";
            TableHeaderCell thc2 = new TableHeaderCell();
            thc2.Text = "Administrador Restaurante";
            TableHeaderCell thc3 = new TableHeaderCell();
            thc3.Text = "Caixa";
            TableHeaderCell thc4 = new TableHeaderCell();
            thc4.Text = "Gerente de Pedidos";


            th.Cells.Add(thc);
            th.Cells.Add(thc1);
            th.Cells.Add(thc2);
            th.Cells.Add(thc3);
            th.Cells.Add(thc4);

            Table1.Rows.Add(th);

            foreach (Models.UsuarioSistema x in obj2)
            {
                Label lb2 = new Label();
                lb2.Text = x.ToString();
                TableRow tRow = new TableRow();

                TableCell tc = new TableCell();
                tc.Text = x.Usuario.ToString();

                TableCell tc1 = new TableCell();
                tc1.Text = x.Garcom.ToString();

                TableCell tc2 = new TableCell();
                tc2.Text = x.AdminRest.ToString();

                TableCell tc3 = new TableCell();
                tc3.Text = x.Caixa.ToString();

                TableCell tc4 = new TableCell();
                tc4.Text = x.GerentePedidos.ToString();

                tRow.Cells.Add(tc);
                tRow.Cells.Add(tc1);
                tRow.Cells.Add(tc2);
                tRow.Cells.Add(tc3);
                tRow.Cells.Add(tc4);

                tRow.BorderStyle = BorderStyle.Ridge;
                tRow.BorderWidth = 1;

                Table1.Rows.Add(tRow);
            }
            if (!IsPostBack) DropRest();
        }
    }
}