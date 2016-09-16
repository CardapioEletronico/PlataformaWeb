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
    public partial class WebCRUDFila : System.Web.UI.Page
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
                titulo.Text = "Gerenciamento de Filas";
                Label labelu = Master.FindControl("LabelUsuario") as Label;
                if (Session["Login"] != null) labelu.Text = Session["Login"].ToString();
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            Carregar();
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
            Carregar();
        }

        protected async void btnInsert_Click(object sender, EventArgs e)
        {
            HttpClient httpClient = new HttpClient();

            httpClient.BaseAddress = new Uri(ip);
            Models.Fila f = new Models.Fila
            {
                Id = int.Parse(textBoxId.Text),
                Descricao = textBoxDesc.Text,
                Cardapio_id = int.Parse(Restaurantes.SelectedValue)
            };

            List<Models.Fila> fl = new List<Models.Fila>();

            fl.Add(f);

            string s = "=" + JsonConvert.SerializeObject(fl);

            var content = new StringContent(s, Encoding.UTF8, "application/x-www-form-urlencoded");

            await httpClient.PostAsync("/20131011110061/api/fila", content);

            Carregar();

        }

        protected async void btnUpdate_Click(object sender, EventArgs e)
        {
            HttpClient httpClient = new HttpClient();

            httpClient.BaseAddress = new Uri(ip);
            Models.Fila f = new Models.Fila
            {
                Id = int.Parse(textBoxId.Text),
                Descricao = textBoxDesc.Text,
                Cardapio_id = int.Parse(Restaurantes.SelectedValue)

            };

            string s = "=" + JsonConvert.SerializeObject(f);

            var content = new StringContent(s, Encoding.UTF8, "application/x-www-form-urlencoded");

            await httpClient.PutAsync("/20131011110061/api/fila/" + f.Id, content);

            Carregar();
        }

        protected async void btnDelete_Click(object sender, EventArgs e)
        {
            HttpClient httpClient = new HttpClient();

            httpClient.BaseAddress = new Uri(ip);

            //await httpClient.DeleteAsync("/20131011110061/api/restaurante/" + textBoxId.Text);
            await httpClient.DeleteAsync("/20131011110061/api/fila/" + textBoxId.Text);

            Carregar();

        }

        private async void Carregar()
        {
            HttpClient httpClient = new HttpClient();

            int idRest = Convert.ToInt16(Session["idRest"]);

            httpClient.BaseAddress = new Uri(ip);
            var response = await httpClient.GetAsync("20131011110061/api/fila");
            var str = response.Content.ReadAsStringAsync().Result;
            List<Models.Fila> filalista = JsonConvert.DeserializeObject<List<Models.Fila>>(str);

            var responseCard = await httpClient.GetAsync("20131011110061/api/cardapio");
            var str1 = responseCard.Content.ReadAsStringAsync().Result;
            List<Models.Cardapio> cardapiolista = JsonConvert.DeserializeObject<List<Models.Cardapio>>(str1);

            List<Models.Cardapio> obj3 = new List<Models.Cardapio>();
            List<Models.Fila> obj4 = new List<Models.Fila>();

            foreach (Models.Cardapio x in cardapiolista)
                if (x.Restaurante_id == idRest)
                    obj3.Add(x);

            foreach (Models.Fila y in filalista)
            {
                foreach (Models.Cardapio o in obj3)
                    if (y.Cardapio_id == o.Id)
                        obj4.Add(y);
            }

            foreach (Models.Fila x in obj4)
            {
                Label lb2 = new Label();
                lb2.Text = x.ToString();
                TableRow tRow = new TableRow();
                TableCell tc = new TableCell();
                tc.Text = x.Id.ToString() + "  -";
                TableCell tc2 = new TableCell();
                tc2.Text = x.Descricao.ToString() + "  -";
                TableCell tc3 = new TableCell();
                tc3.Text = x.Cardapio_id.ToString();
                tRow.Cells.Add(tc);
                tRow.Cells.Add(tc2);
                tRow.Cells.Add(tc3);
                Table1.Rows.Add(tRow);
            }

            if (!IsPostBack) DropRest();
        }

    }
}