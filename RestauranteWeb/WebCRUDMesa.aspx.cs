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
            else if("AdmRest" == Session["Permissao"])
            {
                Label titulo = Master.FindControl("titulo") as Label;
                titulo.Text = "Gerenciamento de Mesas";
                Label labelu = Master.FindControl("LabelUsuario") as Label;
                if (Session["Login"] != null) labelu.Text = Session["Login"].ToString();
            }
            else
            {
                Response.Write("<script>window.alert('Você não tem permissão para acessar esse link.'); self.location = 'LoginAdmRest.aspx';</script>)");
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            Reload();
        }

        protected void btnSelect_Click(object sender, EventArgs e)
        {
            Reload();
        }

        protected async void btnInsert_Click(object sender, EventArgs e)
        {
            int idRest = Convert.ToInt16(Session["idRest"]);
            HttpClient httpClient = new HttpClient();
          
            httpClient.BaseAddress = new Uri(ip);
            Models.Mesa f = new Models.Mesa
            {
                Numero = textBoxNum.Text,
                Restaurante_id = idRest,
                Disponivel = true        
             };

            if (CheckBoxList1.SelectedItem.Text == "Sim")
                f.Disponivel = true;

            else
                f.Disponivel = false;

            string s = JsonConvert.SerializeObject(f);
            var content = new StringContent(s, Encoding.UTF8, "application/json");
            await httpClient.PostAsync("/20131011110061/api/mesa", content);
            Reload();
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

            var content = new StringContent(JsonConvert.SerializeObject(f), Encoding.UTF8, "application/json");
            await httpClient.PutAsync("/20131011110061/api/mesa/" + f.Id, content);
      
            Reload();
        }

        protected async void btnDelete_Click(object sender, EventArgs e)
        {
            HttpClient httpClient = new HttpClient();

            httpClient.BaseAddress = new Uri(ip);

            await httpClient.DeleteAsync("/20131011110061/api/mesa/" + textBoxId.Text);

            Reload();
        }

        protected async void Reload()
        {
            Table1.Rows.Clear();
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

            TableHeaderCell thc3 = new TableHeaderCell();
            thc3.Text = "Disponível";

            th.Cells.Add(thc);
            th.Cells.Add(thc1);
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
                TableCell tc4 = new TableCell();
                //CheckBoxList1.SelectedItem.Text = x.Disponivel.ToString();
                tc4.Text = x.Disponivel.ToString();

                tRow.Cells.Add(tc);
                tRow.Cells.Add(tc2);
                tRow.Cells.Add(tc4);
                Table1.Rows.Add(tRow);
            }
            //if (!IsPostBack) DropRest();

        }
    }
}