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

        protected async void Page_Load(object sender, EventArgs e)
        {
            Table1.Rows.Clear();
            Reload();
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
            Models.AdminSistema f = new Models.AdminSistema
            {
                Usuario = textBoxUsuario.Text,
                Senha = cu,
            };

            List<Models.AdminSistema> fl = new List<Models.AdminSistema>();

            fl.Add(f);

            string s = "=" + JsonConvert.SerializeObject(fl, Newtonsoft.Json.Formatting.None,
                            new JsonSerializerSettings
                            {
                                NullValueHandling = NullValueHandling.Ignore
                            });

            var content = new StringContent(s, Encoding.UTF8, "application/x-www-form-urlencoded");

            HttpResponseMessage message = await httpClient.PostAsync("/20131011110061/api/adminsistema", content);

            
        }

        protected async void btnUpdate_Click(object sender, EventArgs e)
        {

            HttpClient httpClient = new HttpClient();

            httpClient.BaseAddress = new Uri(ip);
            Models.AdminSistema f = new Models.AdminSistema
            {
                Usuario = textBoxUsuario.Text,
                Senha = textBoxSenha.Text,
            };

            string s = "=" + JsonConvert.SerializeObject(f);

            var content = new StringContent(s, Encoding.UTF8, "application/x-www-form-urlencoded");

            await httpClient.PutAsync("/20131011110061/api/adminsistema/" + textBoxUsuario.Text, content);

        }

        protected async void btnDelete_Click(object sender, EventArgs e)
        {
            HttpClient httpClient = new HttpClient();

            httpClient.BaseAddress = new Uri(ip);

            await httpClient.DeleteAsync("/20131011110061/api/adminsistema/" + textBoxUsuario.Text);
        }

        public async void Reload()
        {
            HttpClient httpClient = new HttpClient();

            httpClient.BaseAddress = new Uri(ip);

            var response2 = await httpClient.GetAsync("/20131011110061/api/adminsistema");
            var str2 = response2.Content.ReadAsStringAsync().Result;
            List<Models.AdminSistema> obj2 = JsonConvert.DeserializeObject<List<Models.AdminSistema>>(str2);

            Table1.Rows.Clear();

            TableHeaderRow th = new TableHeaderRow();
            TableHeaderCell thc = new TableHeaderCell();
            thc.Text = "Gerentes do Sistema";

            th.Cells.Add(thc);

            Table1.Rows.Add(th);

            foreach (Models.AdminSistema x in obj2)
            {
                Label lb2 = new Label();
                lb2.Text = x.ToString();
                TableRow tRow = new TableRow();

                TableCell tc = new TableCell();
                tc.Text = x.Usuario.ToString();

                tRow.Cells.Add(tc);

                tRow.BorderStyle = BorderStyle.Ridge;
                tRow.BorderWidth = 1;

                Table1.Rows.Add(tRow);
            }
        }
    }
}