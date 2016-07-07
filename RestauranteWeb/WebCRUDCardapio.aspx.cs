using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace RestauranteWeb
{
    public partial class WebCRUDCardapio : System.Web.UI.Page
    {
        private string ip = "http://10.21.0.137";
        protected async void Page_Load(object sender, EventArgs e)
        {
            HttpClient httpClient = new HttpClient();

            httpClient.BaseAddress = new Uri(ip);
            //var response = await httpClient.GetAsync("/20131011110061/api/restaurante");
            var response = await httpClient.GetAsync("/20131011110029/api/cardapio");

            var str = response.Content.ReadAsStringAsync().Result;

            List<Models.Cardapio> obj = JsonConvert.DeserializeObject<List<Models.Cardapio>>(str);

            //GridView1.AutoGenerateColumns = true;
            //GridView1.DataSource = obj.ToList();
            Label1.Text = "<h3>Cardapio</h3>";
            foreach (Models.Cardapio x in obj)
            {
                Label lb2 = new Label();
                lb2.Text = x.ToString();
                TableRow tRow = new TableRow();

                TableCell tc = new TableCell();
                tc.Text = x.Id.ToString() + "  -";
                TableCell tc2 = new TableCell();
                tc2.Text = x.Descricao.ToString();
                tRow.Cells.Add(tc);
                tRow.Cells.Add(tc2);
                Table1.Rows.Add(tRow);
            }
        }

        protected async void btnSelect_Click(object sender, EventArgs e)
        {
            HttpClient httpClient = new HttpClient();

            httpClient.BaseAddress = new Uri(ip);
            var response = await httpClient.GetAsync("/20131011110029/api/cardapio");

            var str = response.Content.ReadAsStringAsync().Result;

            List<Models.Cardapio> obj = JsonConvert.DeserializeObject<List<Models.Cardapio>>(str);
            Table1.Rows.Clear();
            //GridView1.AutoGenerateColumns = true;
            //GridView1.DataSource = obj;
            //Label1.Text = str;
            foreach (Models.Cardapio x in obj)
            {
                Label lb2 = new Label();
                lb2.Text = x.ToString();
                TableRow tRow = new TableRow();

                TableCell tc = new TableCell();
                tc.Text = x.Id.ToString() + "  -";
                TableCell tc2 = new TableCell();
                tc2.Text = x.Descricao.ToString();
                tRow.Cells.Add(tc);
                tRow.Cells.Add(tc2);
                Table1.Rows.Add(tRow);
            }
        }
    }
}