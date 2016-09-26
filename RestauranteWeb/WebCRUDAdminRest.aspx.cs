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

        protected async void Page_Load(object sender, EventArgs e)
        {
            HttpClient httpClient = new HttpClient();

            httpClient.BaseAddress = new Uri(ip);
            var response = await httpClient.GetAsync("/20131011110061/api/adminrest");

            var str = response.Content.ReadAsStringAsync().Result;
            List<Models.AdminRest> obj = JsonConvert.DeserializeObject<List<Models.AdminRest>>(str);


            var response2 = await httpClient.GetAsync("/20131011110061/api/restaurante");
            var str2 = response2.Content.ReadAsStringAsync().Result;
            List<Models.Restaurante> obj2 = JsonConvert.DeserializeObject<List<Models.Restaurante>>(str2);

            Label1.Text = "<h3>Administradores</h3>";
            foreach (Models.AdminRest x in obj)
            {
                TableRow tRow = new TableRow();
                Label lb2 = new Label();
                lb2.Text = x.ToString();
                TableCell tc = new TableCell();
                tc.Text = x.Usuario.ToString() + "  -";
                TableCell tc2 = new TableCell();
                tc2.Text = x.Senha + "  -";
                TableCell tc3 = new TableCell();

                foreach (Models.Restaurante y in obj2)
                {
                    if (y.Id == x.Restaurante_id)
                        tc3.Text = y.Descricao.ToString();
                }
                tRow.Cells.Add(tc);
                tRow.Cells.Add(tc2);
                tRow.Cells.Add(tc3);
                Table1.Rows.Add(tRow);
            }
            if (!IsPostBack) DropRest();
        }

        public async void DropRest()
        {
            HttpClient httpClient = new HttpClient();

            httpClient.BaseAddress = new Uri(ip);
            //var response = await httpClient.GetAsync("/20131011110061/api/restaurante");
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
            Models.AdminRest f = new Models.AdminRest
            {
                Usuario = textBoxUsuario.Text,
                Senha = cu,
                Restaurante_id = int.Parse(Restaurantes.SelectedItem.Value)
            };

            List<Models.AdminRest> fl = new List<Models.AdminRest>();

            fl.Add(f);

            string s = "=" + JsonConvert.SerializeObject(fl, Newtonsoft.Json.Formatting.None,
                            new JsonSerializerSettings
                            {
                                NullValueHandling = NullValueHandling.Ignore
                            });

            var content = new StringContent(s, Encoding.UTF8, "application/x-www-form-urlencoded");

            HttpResponseMessage message = await httpClient.PostAsync("/20131011110061/api/adminrest", content);

            Reload();
        }

        protected async void btnUpdate_Click(object sender, EventArgs e)
        {

            HttpClient httpClient = new HttpClient();

            httpClient.BaseAddress = new Uri(ip);
            Models.AdminRest f = new Models.AdminRest
            {
                Usuario  = textBoxUsuario.Text,
                Senha = textBoxSenha.Text,
                Restaurante_id = int.Parse(Restaurantes.SelectedValue)
            };

            string s = "=" + JsonConvert.SerializeObject(f);

            var content = new StringContent(s, Encoding.UTF8, "application/x-www-form-urlencoded");
            
            await httpClient.PutAsync("/20131011110061/api/adminrest/" + textBoxUsuario.Text, content);

            Reload();
        }

        protected async void btnDelete_Click(object sender, EventArgs e)
        {
            HttpClient httpClient = new HttpClient();

            httpClient.BaseAddress = new Uri(ip);

            await httpClient.DeleteAsync("/20131011110061/api/adminrest/" + textBoxUsuario.Text);
            Reload();

        }

        public async void Reload()
        {
            HttpClient httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri(ip);

            var response = await httpClient.GetAsync("/20131011110061/api/adminrest");
            var str = response.Content.ReadAsStringAsync().Result;
            List<Models.AdminRest> obj = JsonConvert.DeserializeObject<List<Models.AdminRest>>(str);

            var response2 = await httpClient.GetAsync("/20131011110061/api/restaurante");
            var str2 = response2.Content.ReadAsStringAsync().Result;
            List<Models.Restaurante> obj2 = JsonConvert.DeserializeObject<List<Models.Restaurante>>(str2);

            Table1.Rows.Clear();
            foreach (Models.AdminRest x in obj)
            {
                Label lb2 = new Label();
                lb2.Text = x.ToString();
                TableRow tRow = new TableRow();

                TableCell tc = new TableCell();
                tc.Text = x.Usuario.ToString() + "  -";
                TableCell tc2 = new TableCell();
                tc2.Text = x.Senha + "  -";

                TableCell tc3 = new TableCell();
                foreach (Models.Restaurante y in obj2) {
                     if(y.Id == x.Restaurante_id)
                        tc3.Text = y.Descricao;
                }

                tRow.Cells.Add(tc);
                tRow.Cells.Add(tc2);
                tRow.Cells.Add(tc3);
                Table1.Rows.Add(tRow);
            }
        }
    }
}