using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace RestauranteWeb
{
    public partial class WebCRUDCardapio : System.Web.UI.Page
    {
        private string ip = "http://10.21.0.137";

        protected void Page_PreInit(object sender, EventArgs e)
        {
            if (Session["Login"] == null)
            {
                Response.Write("<script>window.alert('Faça seu login para acessar esse link.'); self.location = 'LoginAdmRest.aspx';</script>)");
            }
            else { 
            Label titulo = Master.FindControl("titulo") as Label;
            titulo.Text = "Gerenciamento de Cardápios";
            Label labelu = Master.FindControl("LabelUsuario") as Label;
            if (Session["Login"] != null) labelu.Text = Session["Login"].ToString();
            }
        }

        protected async void Page_Load(object sender, EventArgs e)
        {
            Table1.Rows.Clear();
            Reload();
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
            Table1.Rows.Clear();
            Reload();
        }

        protected async void btnInsert_Click(object sender, EventArgs e)
        {
            HttpClient httpClient = new HttpClient();

            httpClient.BaseAddress = new Uri(ip);
            Models.Cardapio f = new Models.Cardapio
            {
                Id = int.Parse(textBoxId.Text),
                Descricao = textBoxDesc.Text,
                Restaurante_id = int.Parse(Restaurantes.SelectedItem.Value)
            };

            List<Models.Cardapio> fl = new List<Models.Cardapio>();

            fl.Add(f);

            string s = "=" + JsonConvert.SerializeObject(fl);

            var content = new StringContent(s, Encoding.UTF8, "application/x-www-form-urlencoded");

            await httpClient.PostAsync("/20131011110061/api/cardapio", content);

            Table1.Rows.Clear();

            Reload();
        }

        protected async void btnUpdate_Click(object sender, EventArgs e)
        {

            HttpClient httpClient = new HttpClient();

            httpClient.BaseAddress = new Uri(ip);
            Models.Cardapio f = new Models.Cardapio
            {
                Id = int.Parse(textBoxId.Text),
                Descricao = textBoxDesc.Text,
                Restaurante_id = int.Parse(Restaurantes.SelectedValue)
                
            };

            string s = "=" + JsonConvert.SerializeObject(f);

            var content = new StringContent(s, Encoding.UTF8, "application/x-www-form-urlencoded");

            //await httpClient.PutAsync("/20131011110061/api/restaurante/" + f.Id, content);
            await httpClient.PutAsync("/20131011110061/api/cardapio/" + f.Id, content);

            Table1.Rows.Clear();

            Reload();
        }

        protected async void btnDelete_Click(object sender, EventArgs e)
        {
            HttpClient httpClient = new HttpClient();

            httpClient.BaseAddress = new Uri(ip);

            await httpClient.DeleteAsync("/20131011110061/api/cardapio/" + textBoxId.Text);

            Reload();
        }

        public async void Reload()
        {
            int idRest = Convert.ToInt16(Session["idRest"]);

            HttpClient httpClient = new HttpClient();

            httpClient.BaseAddress = new Uri(ip);

            var response2 = await httpClient.GetAsync("/20131011110061/api/restaurante");
            var str2 = response2.Content.ReadAsStringAsync().Result;
            List<Models.Restaurante> obj2 = JsonConvert.DeserializeObject<List<Models.Restaurante>>(str2);

            var response = await httpClient.GetAsync("/20131011110061/api/cardapio");
            var str = response.Content.ReadAsStringAsync().Result;
            List<Models.Cardapio> obj = JsonConvert.DeserializeObject<List<Models.Cardapio>>(str);

            List<Models.Cardapio> obj3 = new List<Models.Cardapio>();
            foreach (Models.Cardapio x in obj)
            {
                if (x.Restaurante_id == idRest)
                {
                    obj3.Add(x);
                }
            }

            Table1.Rows.Clear();

            TableHeaderRow th = new TableHeaderRow();
            TableHeaderCell thc = new TableHeaderCell();
            thc.Text = "ID";

            TableHeaderCell thc1 = new TableHeaderCell();
            thc1.Text = "Descricao";

            TableHeaderCell thc2 = new TableHeaderCell();
            thc2.Text = "Restaurante";

            th.Cells.Add(thc);
            th.Cells.Add(thc1);
            th.Cells.Add(thc2);

            Table1.Rows.Add(th);


            foreach (Models.Cardapio x in obj3)
            {
                Label lb2 = new Label();
                lb2.Text = x.ToString();
                TableRow tRow = new TableRow();

                TableCell tc = new TableCell();
                tc.Text = x.Id.ToString();
                TableCell tc2 = new TableCell();
                tc2.Text = x.Descricao;
                TableCell tc3 = new TableCell();

                foreach (Models.Restaurante y in obj2)
                {
                    if (y.Id == x.Restaurante_id)
                        tc3.Text = y.Descricao.ToString();
                }

                tRow.Cells.Add(tc);
                tRow.Cells.Add(tc2);
                tRow.Cells.Add(tc3);

                tRow.BorderStyle = BorderStyle.Ridge;
                tRow.BorderWidth = 1;

                Table1.Rows.Add(tRow);
            }
            if (!IsPostBack) DropRest();
        }
    }
}