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
        protected async void Page_Load(object sender, EventArgs e)
        {
            HttpClient httpClient = new HttpClient();

            httpClient.BaseAddress = new Uri(ip);
            //var response = await httpClient.GetAsync("/20131011110061/api/restaurante");
            var response = await httpClient.GetAsync("20131011110029/api/fila");

            var str = response.Content.ReadAsStringAsync().Result;
            //ERROOOORR
            List<Models.Fila> obj = JsonConvert.DeserializeObject<List<Models.Fila>>(str);

            Label1.Text = "<h3>Fila</h3>";
            foreach (Models.Fila x in obj)
            {
                Label lb2 = new Label();
                lb2.Text = x.ToString();
                TableRow tRow = new TableRow();

                TableCell tc = new TableCell();
                tc.Text = x.Id.ToString() + "  -";
                TableCell tc2 = new TableCell();
                tc2.Text = x.Descricao.ToString() + "  -";
                TableCell tc3 = new TableCell();
                tc3.Text = x.Restaurante_id.ToString();
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
            var response = await httpClient.GetAsync("/20131011110029/api/restaurante");
            var str = response.Content.ReadAsStringAsync().Result;

            List<Models.Restaurante> obj = JsonConvert.DeserializeObject<List<Models.Restaurante>>(str);

            Label1.Text = "<h3>Fila</h3>";
            Restaurantes.DataSource = obj;
            Restaurantes.DataTextField = "Descricao";
            Restaurantes.DataValueField = "Id";
            Restaurantes.DataBind();

        }

        protected async void btnSelect_Click(object sender, EventArgs e)
        {
            HttpClient httpClient = new HttpClient();

            httpClient.BaseAddress = new Uri(ip);
            var response = await httpClient.GetAsync("/20131011110029/api/fila");

            var str = response.Content.ReadAsStringAsync().Result;

            List<Models.Fila> obj = JsonConvert.DeserializeObject<List<Models.Fila>>(str);
            Table1.Rows.Clear();
            //GridView1.AutoGenerateColumns = true;
            //GridView1.DataSource = obj;
            //Label1.Text = str;
            foreach (Models.Fila x in obj)
            {
                Label lb2 = new Label();
                lb2.Text = x.ToString();
                TableRow tRow = new TableRow();

                TableCell tc = new TableCell();
                tc.Text = x.Id.ToString() + "  -";
                TableCell tc2 = new TableCell();
                tc2.Text = x.Descricao.ToString();
                TableCell tc3 = new TableCell();
                tc3.Text = x.Restaurante_id.ToString();

                tRow.Cells.Add(tc);
                tRow.Cells.Add(tc2);
                tRow.Cells.Add(tc3);
                Table1.Rows.Add(tRow);
            }

        }

        protected async void btnInsert_Click(object sender, EventArgs e)
        {
            HttpClient httpClient = new HttpClient();

            httpClient.BaseAddress = new Uri(ip);
            Models.Fila f = new Models.Fila
            {
                Id = int.Parse(textBoxId.Text),
                Descricao = textBoxDesc.Text,
                Restaurante_id = int.Parse(Restaurantes.SelectedValue)
            };

            List<Models.Fila> fl = new List<Models.Fila>();

            fl.Add(f);

            string s = "=" + JsonConvert.SerializeObject(fl);

            var content = new StringContent(s, Encoding.UTF8, "application/x-www-form-urlencoded");

            await httpClient.PostAsync("/20131011110029/api/fila", content);


            var response = await httpClient.GetAsync("/20131011110029/api/fila");
            var str = response.Content.ReadAsStringAsync().Result;
            List<Models.Fila> obj = JsonConvert.DeserializeObject<List<Models.Fila>>(str);
            Table1.Rows.Clear();
            foreach (Models.Fila x in obj)
            {
                Label lb2 = new Label();
                lb2.Text = x.ToString();
                TableRow tRow = new TableRow();

                TableCell tc = new TableCell();
                tc.Text = x.Id.ToString() + "  -";
                TableCell tc2 = new TableCell();
                tc2.Text = x.Descricao.ToString();
                TableCell tc3 = new TableCell();
                tc3.Text = x.Restaurante_id.ToString();
                tRow.Cells.Add(tc);
                tRow.Cells.Add(tc2);
                tRow.Cells.Add(tc3);
                Table1.Rows.Add(tRow);
            }

        }

        protected async void btnUpdate_Click(object sender, EventArgs e)
        {
            HttpClient httpClient = new HttpClient();

            httpClient.BaseAddress = new Uri(ip);
            Models.Fila f = new Models.Fila
            {
                Id = int.Parse(textBoxId.Text),
                Descricao = textBoxDesc.Text,
                Restaurante_id = int.Parse(Restaurantes.SelectedValue)

            };

            string s = "=" + JsonConvert.SerializeObject(f);

            var content = new StringContent(s, Encoding.UTF8, "application/x-www-form-urlencoded");

            //await httpClient.PutAsync("/20131011110061/api/restaurante/" + f.Id, content);
            await httpClient.PutAsync("/20131011110029/api/fila/" + f.Id, content);


            var response = await httpClient.GetAsync("/20131011110029/api/fila");
            var str = response.Content.ReadAsStringAsync().Result;
            List<Models.Fila> obj = JsonConvert.DeserializeObject<List<Models.Fila>>(str);
            Table1.Rows.Clear();
            foreach (Models.Fila x in obj)
            {
                Label lb2 = new Label();
                lb2.Text = x.ToString();
                TableRow tRow = new TableRow();

                TableCell tc = new TableCell();
                tc.Text = x.Id.ToString() + "  -";
                TableCell tc2 = new TableCell();
                tc2.Text = x.Descricao.ToString();
                TableCell tc3 = new TableCell();
                tc3.Text = x.Restaurante_id.ToString();
                tRow.Cells.Add(tc);
                tRow.Cells.Add(tc2);
                tRow.Cells.Add(tc3);
                Table1.Rows.Add(tRow);
            }

        }

        protected async void btnDelete_Click(object sender, EventArgs e)
        {
            HttpClient httpClient = new HttpClient();

            httpClient.BaseAddress = new Uri(ip);

            //await httpClient.DeleteAsync("/20131011110061/api/restaurante/" + textBoxId.Text);
            await httpClient.DeleteAsync("/20131011110029/api/fila/" + textBoxId.Text);



            var response = await httpClient.GetAsync("/20131011110029/api/fila");
            var str = response.Content.ReadAsStringAsync().Result;
            List<Models.Fila> obj = JsonConvert.DeserializeObject<List<Models.Fila>>(str);
            Table1.Rows.Clear();
            foreach (Models.Fila x in obj)
            {
                Label lb2 = new Label();
                lb2.Text = x.ToString();
                TableRow tRow = new TableRow();

                TableCell tc = new TableCell();
                tc.Text = x.Id.ToString() + "  -";
                TableCell tc2 = new TableCell();
                tc2.Text = x.Descricao.ToString();
                TableCell tc3 = new TableCell();
                tc3.Text = x.Restaurante_id.ToString();
                tRow.Cells.Add(tc);
                tRow.Cells.Add(tc2);
                tRow.Cells.Add(tc3);
                Table1.Rows.Add(tRow);
            }

        }
    }
}