using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Cryptography;
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

        //https://gist.github.com/cmatskas/faee04c7b78afae065e1

        protected async void login_click(object Source, EventArgs e)
        {
            HttpClient httpClient = new HttpClient();

            httpClient.BaseAddress = new Uri(ip);
            var response = await httpClient.GetAsync("/20131011110061/api/adminrest");

            var str = response.Content.ReadAsStringAsync().Result;
            List<Models.UsuarioSistema> obj = JsonConvert.DeserializeObject<List<Models.UsuarioSistema>>(str);

            foreach (Models.UsuarioSistema x in obj)
            {
                if(x.Usuario == TextBoxUsuario.Text && ValidatePassword(TextBoxSenha.Text, x.Senha))
                {
                    Session["Login"] = TextBoxUsuario.Text;
                    Session["idRest"] = x.Restaurante_id;
                    Session["Permissao"] = "AdmRest";
                    Response.Write("<script>window.alert('Logado com sucesso!'); self.location='WebCRUDCardapio.aspx'; </script>");
                    return;
                }
            }
            Response.Write("<script>window.alert('Usuário não encontrado'); </script>");
        }

        public const int SaltByteSize = 24;
        public const int HashByteSize = 20; // to match the size of the PBKDF2-HMAC-SHA-1 hash 
        public const int Pbkdf2Iterations = 1000;
        public const int IterationIndex = 0;
        public const int SaltIndex = 1;
        public const int Pbkdf2Index = 2;

        public static bool ValidatePassword(string password, string correctHash)
        {
            char[] delimiter = { ':' };
            var split = correctHash.Split(delimiter);
            var iterations = Int32.Parse(split[IterationIndex]);
            var salt = Convert.FromBase64String(split[SaltIndex]);
            var hash = Convert.FromBase64String(split[Pbkdf2Index]);

            var testHash = GetPbkdf2Bytes(password, salt, iterations, hash.Length);
            return SlowEquals(hash, testHash);
        }

        private static bool SlowEquals(byte[] a, byte[] b)
        {
            var diff = (uint)a.Length ^ (uint)b.Length;
            for (int i = 0; i < a.Length && i < b.Length; i++)
            {
                diff |= (uint)(a[i] ^ b[i]);
            }
            return diff == 0;
        }

        private static byte[] GetPbkdf2Bytes(string password, byte[] salt, int iterations, int outputBytes)
        {
            var pbkdf2 = new Rfc2898DeriveBytes(password, salt);
            pbkdf2.IterationCount = iterations;
            return pbkdf2.GetBytes(outputBytes);
        }
    }
}
