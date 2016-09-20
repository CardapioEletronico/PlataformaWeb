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
    public partial class WebCRUDMesa : System.Web.UI.Page
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
                titulo.Text = "Gerenciamento de Mesas";
                Label labelu = Master.FindControl("LabelUsuario") as Label;
                if (Session["Login"] != null) labelu.Text = Session["Login"].ToString();
            }
        }

        protected async void Page_Load(object sender, EventArgs e)
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

            /*foreach (Models.Restaurante x in obj)
            {
                //ID
                //Restaurantes.Items.Add(x.Id.ToString());
                Restaurantes.Items.Add(x.Descricao.ToString());
            }*/
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
            Models.Mesa f = new Models.Mesa
            {
                Id = int.Parse(textBoxId.Text),
                Numero = textBoxNum.Text,
                Restaurante_id = int.Parse(Restaurantes.SelectedValue),
                Disponivel = true        
             };

            if (CheckBoxList1.SelectedItem.Text == "Sim")
            {
                f.Disponivel = true;
            }

            else
            {
                f.Disponivel = false;
            }

            List<Models.Mesa> fl = new List<Models.Mesa>();

            fl.Add(f);

            string s = "=" + JsonConvert.SerializeObject(fl);

            var content = new StringContent(s, Encoding.UTF8, "application/x-www-form-urlencoded");

            await httpClient.PostAsync("/20131011110061/api/mesa", content);


            Carregar();
        }

        protected async void btnUpdate_Click(object sender, EventArgs e)
        {
            HttpClient httpClient = new HttpClient();

            httpClient.BaseAddress = new Uri(ip);
            Models.Mesa f = new Models.Mesa
            {
                Id = int.Parse(textBoxId.Text),
                Numero = textBoxNum.Text,
                Restaurante_id = int.Parse(Restaurantes.SelectedValue),
                Disponivel = true
            };

            if (CheckBoxList1.SelectedItem.Text == "Sim")
                f.Disponivel = true;

            else
                f.Disponivel = false;

            string s = "=" + JsonConvert.SerializeObject(f);

            var content = new StringContent(s, Encoding.UTF8, "application/x-www-form-urlencoded");

            //await httpClient.PutAsync("/20131011110061/api/restaurante/" + f.Id, content);
            await httpClient.PutAsync("/20131011110029/api/mesa/" + f.Id, content);

            Carregar();
        }

        protected async void btnDelete_Click(object sender, EventArgs e)
        {
            HttpClient httpClient = new HttpClient();

            httpClient.BaseAddress = new Uri(ip);

            //await httpClient.DeleteAsync("/20131011110061/api/restaurante/" + textBoxId.Text);
            await httpClient.DeleteAsync("/20131011110029/api/mesa/" + textBoxId.Text);

            Carregar();
        }

        protected async void Carregar()
        {
            int idRest = Convert.ToInt16(Session["idRest"]);
            HttpClient httpClient = new HttpClient();

            httpClient.BaseAddress = new Uri(ip);
            var response = await httpClient.GetAsync("20131011110061/api/mesa");
            var str = response.Content.ReadAsStringAsync().Result;
            List<Models.Mesa> obj = JsonConvert.DeserializeObject<List<Models.Mesa>>(str);

            var ListMesas = from mesa in obj where mesa.Restaurante_id == idRest select mesa;

            TableHeaderRow th = new TableHeaderRow();
            TableHeaderCell thc = new TableHeaderCell();
            thc.Text = "ID";
            thc.Width = 100;

            TableHeaderCell thc1 = new TableHeaderCell();
            thc1.Text = "Numero";

            TableHeaderCell thc2 = new TableHeaderCell();
            thc2.Text = "Restaurante";

            TableHeaderCell thc3 = new TableHeaderCell();
            thc3.Text = "Disponível";

            th.Cells.Add(thc);
            th.Cells.Add(thc1);
            th.Cells.Add(thc2);
            th.Cells.Add(thc3);

            Table1.Rows.Add(th);

            foreach (Models.Mesa x in ListMesas)
            {
                Label lb2 = new Label();
                lb2.Text = x.ToString();
                TableRow tRow = new TableRow();

                TableCell tc = new TableCell();
                tc.Text = x.Id.ToString();
                TableCell tc2 = new TableCell();
                tc2.Text = x.Numero.ToString();
                TableCell tc3 = new TableCell();
                tc3.Text = x.Restaurante_id.ToString();
                TableCell tc4 = new TableCell();
                //CheckBoxList1.SelectedItem.Text = x.Disponivel.ToString();
                tc4.Text = x.Disponivel.ToString();

                tRow.Cells.Add(tc);
                tRow.Cells.Add(tc2);
                tRow.Cells.Add(tc3);
                tRow.Cells.Add(tc4);
                Table1.Rows.Add(tRow);
            }
            if (!IsPostBack) DropRest();

        }
    }
}