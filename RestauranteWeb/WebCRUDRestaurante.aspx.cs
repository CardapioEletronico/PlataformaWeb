using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net.Http;
using Newtonsoft.Json;
using System.Text;

namespace RestauranteWeb
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        private string ip = "http://10.21.0.137";


        protected void Page_Load(object sender, EventArgs e)
        {
            Table1.Rows.Clear();
            Reload();
        }

        protected void btnSelect_Click(object sender, EventArgs e)
        {
            Reload();
        }

        protected async void btnInsert_Click(object sender, EventArgs e)
        {
            HttpClient httpClient = new HttpClient();

            httpClient.BaseAddress = new Uri(ip);
            Models.Restaurante f = new Models.Restaurante
            {
                Descricao = textBoxDesc.Text
            };
           
            string s = JsonConvert.SerializeObject(f);

            var content = new StringContent(s, Encoding.UTF8, "application/json");

            await httpClient.PostAsync("/20131011110061/api/restaurante", content);

            Reload();
        }

        protected async void btnUpdate_Click(object sender, EventArgs e)
        {
            HttpClient httpClient = new HttpClient();

            httpClient.BaseAddress = new Uri(ip);
            Models.Restaurante f = new Models.Restaurante
            {
                Id = int.Parse(textBoxId.Text),
                Descricao = textBoxDesc.Text
            };

            var content = new StringContent(JsonConvert.SerializeObject(f), Encoding.UTF8,"application/json");

            await httpClient.PutAsync("/20131011110061/api/restaurante/" + f.Id, content);

            Reload();
        }

        protected async void btnDelete_Click(object sender, EventArgs e)
        {
            HttpClient httpClient = new HttpClient();

            httpClient.BaseAddress = new Uri(ip);

            await httpClient.DeleteAsync("/20131011110061/api/restaurante/" + textBoxId.Text);

            Reload();
        }

        protected async void Reload()
        {
            HttpClient httpClient = new HttpClient();

            httpClient.BaseAddress = new Uri(ip);

            var response2 = await httpClient.GetAsync("/20131011110061/api/restaurante");
            var str2 = response2.Content.ReadAsStringAsync().Result;
            List<Models.Restaurante> obj2 = JsonConvert.DeserializeObject<List<Models.Restaurante>>(str2);

            Table1.Rows.Clear();

            TableHeaderRow th = new TableHeaderRow();
            TableHeaderCell thc = new TableHeaderCell();
            thc.Text = "ID";
            thc.Width = 150;

            TableHeaderCell thc1 = new TableHeaderCell();
            thc1.Text = "Descricao";

            th.Cells.Add(thc);
            th.Cells.Add(thc1);

            Table1.Rows.Add(th);

            foreach (Models.Restaurante x in obj2)
            {
                Label lb2 = new Label();
                lb2.Text = x.ToString();
                TableRow tRow = new TableRow();

                TableCell tc = new TableCell();
                tc.Text = x.Id.ToString();
                TableCell tc2 = new TableCell();
                tc2.Text = x.Descricao;

                tRow.Cells.Add(tc);
                tRow.Cells.Add(tc2);

                tRow.BorderStyle = BorderStyle.Ridge;
                tRow.BorderWidth = 1;

                Table1.Rows.Add(tRow);
            }
            //if (!IsPostBack) DropRest();
        }
    }
}