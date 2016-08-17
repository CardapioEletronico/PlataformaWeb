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
    public partial class CRUDProduto : System.Web.UI.Page
    {
        private string ip = "http://10.21.0.137";
        protected async void Page_Load(object sender, EventArgs e)
        {
            HttpClient httpClient = new HttpClient();

            httpClient.BaseAddress = new Uri(ip);
            //var response = await httpClient.GetAsync("/20131011110061/api/restaurante");
            var response = await httpClient.GetAsync("20131011110061/api/produto");

            var str = response.Content.ReadAsStringAsync().Result;
            //ERROOOORR
            List<Models.Produto> obj = JsonConvert.DeserializeObject<List<Models.Produto>>(str);

            Label1.Text = "<h3>Produtos</h3>";
            foreach (Models.Produto x in obj)
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

        public async void DropRest()
        {
            HttpClient httpClient = new HttpClient();

            httpClient.BaseAddress = new Uri(ip);
            //var response = await httpClient.GetAsync("/20131011110061/api/restaurante");
            var response = await httpClient.GetAsync("/20131011110061/api/cardapio");
            var str = response.Content.ReadAsStringAsync().Result;

            List<Models.Cardapio> obj = JsonConvert.DeserializeObject<List<Models.Cardapio>>(str);

            Cardapios.DataSource = obj;
            Cardapios.DataTextField = "Descricao";
            Cardapios.DataValueField = "Id";
            Cardapios.DataBind();

        }

        protected async void btnSelect_Click(object sender, EventArgs e)
        {
            HttpClient httpClient = new HttpClient();

            httpClient.BaseAddress = new Uri(ip);
            var response = await httpClient.GetAsync("/20131011110061/api/produto");

            var str = response.Content.ReadAsStringAsync().Result;

            List<Models.Produto> obj = JsonConvert.DeserializeObject<List<Models.Produto>>(str);
            Table1.Rows.Clear();
            //GridView1.AutoGenerateColumns = true;
            //GridView1.DataSource = obj;
            //Label1.Text = str;
            foreach (Models.Produto x in obj)
            {
                Label lb2 = new Label();
                lb2.Text = x.ToString();
                TableRow tRow = new TableRow();

                TableCell tc = new TableCell();
                tc.Text = x.Id.ToString() + "  -";
                TableCell tc2 = new TableCell();
                tc2.Text = x.Descricao.ToString();
                TableCell tc3 = new TableCell();
                tc3.Text = x.Cardapio_id.ToString();

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
            Models.Produto f = new Models.Produto
            {
                Id = int.Parse(textBoxId.Text),
                Descricao = textBoxDesc.Text,
                NomeDescricao = textBoxNomeDescr.Text,
                Cardapio_id = int.Parse(Cardapios.SelectedItem.Value),
                Preco = double.Parse(textBoxPreco.Text),
            };

            List<Models.Produto> fl = new List<Models.Produto>();

            fl.Add(f);

            string s = "=" + JsonConvert.SerializeObject(fl);

            var content = new StringContent(s, Encoding.UTF8, "application/x-www-form-urlencoded");

            await httpClient.PostAsync("/20131011110061/api/cardapio", content);


            var response = await httpClient.GetAsync("/20131011110061/api/cardapio");
            var str = response.Content.ReadAsStringAsync().Result;
            List<Models.Produto> obj = JsonConvert.DeserializeObject<List<Models.Produto>>(str);
            Table1.Rows.Clear();
            foreach (Models.Produto x in obj)
            {
                Label lb2 = new Label();
                lb2.Text = x.ToString();
                TableRow tRow = new TableRow();

                TableCell tc = new TableCell();
                tc.Text = x.Id.ToString() + "  -";
                TableCell tc2 = new TableCell();
                tc2.Text = x.Descricao.ToString();
                TableCell tc3 = new TableCell();
                tc3.Text = x.Cardapio_id.ToString();
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
            Models.Produto f = new Models.Produto
            {
                Id = int.Parse(textBoxId.Text),
                Descricao = textBoxDesc.Text,
                Cardapio_id = int.Parse(Cardapios.SelectedValue)

            };

            string s = "=" + JsonConvert.SerializeObject(f);

            var content = new StringContent(s, Encoding.UTF8, "application/x-www-form-urlencoded");

            //await httpClient.PutAsync("/20131011110061/api/restaurante/" + f.Id, content);
            await httpClient.PutAsync("/20131011110061/api/cardapio/" + f.Id, content);
        }

        protected async void btnDelete_Click(object sender, EventArgs e)
        {
            HttpClient httpClient = new HttpClient();

            httpClient.BaseAddress = new Uri(ip);

            //await httpClient.DeleteAsync("/20131011110061/api/restaurante/" + textBoxId.Text);
            await httpClient.DeleteAsync("/20131011110061/api/cardapio/" + textBoxId.Text);
        }
    }
}