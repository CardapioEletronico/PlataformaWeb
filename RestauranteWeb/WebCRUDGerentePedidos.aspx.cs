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
    public partial class WebCRUDGerentePedidos : System.Web.UI.Page
    {
        private string ip = "http://10.21.0.137";

        protected void Page_PreInit(object sender, EventArgs e)
        {
            if (Session["Login"] == null)
            {
                Response.Write("<script>window.alert('Faça seu login para acessar esse link.'); self.location = 'LoginAdmRest.aspx';</script>)");
            }
            else
            {
                Label titulo = Master.FindControl("titulo") as Label;
                titulo.Text = "Gerenciamento de Cardápios";
                Label labelu = Master.FindControl("LabelUsuario") as Label;
                if (Session["Login"] != null) labelu.Text = Session["Login"].ToString();
            }
        }

        protected void Page_Load(object sender, EventArgs e)
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
            int idRest = Convert.ToInt16(Session["idRest"]);
            HttpClient httpClient = new HttpClient();

            string cu = HashPassword(textBoxSenha.Text);

            httpClient.BaseAddress = new Uri(ip);
            Models.GerentePedido f = new Models.GerentePedido
            {
                Usuario = textBoxUsuario.Text,
                Senha = cu,
                Restaurante_id = idRest
            };

            List<Models.GerentePedido> fl = new List<Models.GerentePedido>();

            fl.Add(f);

            string s = "=" + JsonConvert.SerializeObject(fl, Newtonsoft.Json.Formatting.None,
                            new JsonSerializerSettings
                            {
                                NullValueHandling = NullValueHandling.Ignore
                            });

            var content = new StringContent(s, Encoding.UTF8, "application/x-www-form-urlencoded");

            HttpResponseMessage message = await httpClient.PostAsync("/20131011110061/api/gerentepedido", content);

            Reload();
        }

        protected async void btnUpdate_Click(object sender, EventArgs e)
        {
            int idRest = Convert.ToInt16(Session["idRest"]);
            HttpClient httpClient = new HttpClient();

            httpClient.BaseAddress = new Uri(ip);
            Models.GerentePedido f = new Models.GerentePedido
            {
                Usuario = textBoxUsuario.Text,
                Senha = textBoxSenha.Text,
                Restaurante_id = idRest
            };

            string s = "=" + JsonConvert.SerializeObject(f);

            var content = new StringContent(s, Encoding.UTF8, "application/x-www-form-urlencoded");

            await httpClient.PutAsync("/20131011110061/api/gerentepedido/" + textBoxUsuario.Text, content);

            Reload();
        }

        protected async void btnDelete_Click(object sender, EventArgs e)
        {
            HttpClient httpClient = new HttpClient();

            httpClient.BaseAddress = new Uri(ip);

            await httpClient.DeleteAsync("/20131011110061/api/gerentepedido/" + textBoxUsuario.Text);
            Reload();

        }

        public async void Reload()
        {
            int idRest = Convert.ToInt16(Session["idRest"]);

            HttpClient httpClient = new HttpClient();

            httpClient.BaseAddress = new Uri(ip);

            var response2 = await httpClient.GetAsync("/20131011110061/api/gerentepedido");
            var str2 = response2.Content.ReadAsStringAsync().Result;
            List<Models.GerentePedido> obj2 = JsonConvert.DeserializeObject<List<Models.GerentePedido>>(str2);

            var response = await httpClient.GetAsync("/20131011110061/api/restaurante");
            var str = response.Content.ReadAsStringAsync().Result;
            List<Models.Restaurante> obj = JsonConvert.DeserializeObject<List<Models.Restaurante>>(str);

            List<Models.GerentePedido> obj3 = new List<Models.GerentePedido>();

            foreach (Models.GerentePedido x in obj2)
            {
                if (x.Restaurante_id == idRest)
                {
                    obj3.Add(x);
                }
            }

            Table1.Rows.Clear();

            TableHeaderRow th = new TableHeaderRow();

            TableHeaderCell thc1 = new TableHeaderCell();
            thc1.Text = "Usuario";

            th.Cells.Add(thc1);

            Table1.Rows.Add(th);

            foreach (Models.GerentePedido x in obj3)
            {

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