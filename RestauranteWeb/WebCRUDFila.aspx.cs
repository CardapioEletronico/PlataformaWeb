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
            else if("AdmRest" == Session["Permissao"])
            {
                Label titulo = Master.FindControl("titulo") as Label;
                titulo.Text = "Gerenciamento de Filas";
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
            if (!this.IsPostBack)
            {
                Reload();
            }
        }

        public async void DropRest()
        {
            int idRest = Convert.ToInt16(Session["idRest"]);
            HttpClient httpClient = new HttpClient();

            httpClient.BaseAddress = new Uri(ip);
            var response = await httpClient.GetAsync("/20131011110061/api/cardapio");
            var str = response.Content.ReadAsStringAsync().Result;

            List<Models.Cardapio> obj = JsonConvert.DeserializeObject<List<Models.Cardapio>>(str);

            List<Models.Cardapio> obj2 = (from Models.Cardapio c in obj where c.Restaurante_id == idRest select c).ToList();

            Cardapios.DataSource = obj2;
            Cardapios.DataTextField = "Descricao";
            Cardapios.DataValueField = "Id";
            Cardapios.DataBind();

        }

        protected void btnSelect_Click(object sender, EventArgs e)
        {
            Reload();
        }

        protected async void btnInsert_Click(object sender, EventArgs e)
        {
            if (Page.IsValid) { 
                HttpClient httpClient = new HttpClient();
                httpClient.BaseAddress = new Uri(ip);
                Models.Fila f = new Models.Fila
                {
                    Descricao = textBoxDesc.Text,
                    Cardapio_id = int.Parse(Cardapios.SelectedValue)
                };

                string s = JsonConvert.SerializeObject(f);

                var content = new StringContent(s, Encoding.UTF8, "application/json");

                await httpClient.PostAsync("/20131011110061/api/fila", content);

                Reload();
            }

        }

        private async void Reload()
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

            List<Models.Cardapio> cardapioRestaurante = (from Models.Cardapio c in cardapiolista where c.Restaurante_id == idRest select c).ToList();
            List<Models.Fila> Filas = new List<Models.Fila>();

            foreach (Models.Fila y in filalista)
            {
                foreach (Models.Cardapio o in cardapioRestaurante)
                    if (y.Cardapio_id == o.Id)
                        Filas.Add(y);
            }

            var result = from Models.Fila fila in Filas
                         join Models.Cardapio c in cardapioRestaurante
                         on fila.Cardapio_id equals c.Id
                         select fila.ComCardapio(c);

            result = (from Models.Fila f in result orderby f.Descricao select f).ToList();

            GridView1.DataSource = result;
            GridView1.DataBind();
            DropRest();

        }

        protected void OnRowEditing(object sender, GridViewEditEventArgs e)
        {
            GridView1.EditIndex = e.NewEditIndex;
            Reload();
            GridView1.DataBind();
        }

        protected async void OnRowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            GridViewRow row = GridView1.Rows[e.RowIndex];
            int Id = Convert.ToInt32(GridView1.DataKeys[e.RowIndex].Values[0]);

            HttpClient httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri(ip);

            var response = await httpClient.GetAsync("/20131011110061/api/fila");
            var str = response.Content.ReadAsStringAsync().Result;
            List<Models.Fila> obj = JsonConvert.DeserializeObject<List<Models.Fila>>(str);
            Models.Fila item = (from Models.Fila f in obj where f.Id == Id select f).Single();

            item.Descricao = (row.FindControl("txtDescricao") as TextBox).Text;
            int valor = Convert.ToInt32((row.FindControl("CardapiosSelect") as DropDownList).SelectedValue);
            item.Cardapio_id = Convert.ToInt32((row.FindControl("CardapiosSelect") as DropDownList).SelectedValue);

            var content = new StringContent(JsonConvert.SerializeObject(item), Encoding.UTF8, "application/json");
            await httpClient.PutAsync("/20131011110061/api/fila/" + item.Id, content);

            GridView1.EditIndex = -1;
            GridView1.DataBind();

            Reload();
        }

        protected void OnRowCancelingEdit(object sender, EventArgs e)
        {
            GridView1.EditIndex = -1;
            Reload();
            GridView1.DataBind();
        }


        protected async void OnRowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int Id = Convert.ToInt32(GridView1.DataKeys[e.RowIndex].Values[0]);

            HttpClient httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri(ip);
            await httpClient.DeleteAsync("/20131011110061/api/fila/" + Id);

            Reload();
        }

        protected async void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow && GridView1.EditIndex == e.Row.RowIndex)
            {
                DropDownList Cardapio = (DropDownList)e.Row.FindControl("CardapiosSelect");
                HttpClient httpClient = new HttpClient();
                httpClient.BaseAddress = new Uri(ip);
                var response = await httpClient.GetAsync("/20131011110061/api/cardapio");
                var str = response.Content.ReadAsStringAsync().Result;
                List<Models.Cardapio> obj = JsonConvert.DeserializeObject<List<Models.Cardapio>>(str);

                int idRest = Convert.ToInt16(Session["idRest"]);

                List<Models.Cardapio> rest = (from Models.Cardapio c in obj where c.Restaurante_id == idRest select c).ToList();

                Cardapio.DataSource = rest;
                Cardapio.DataTextField = "Descricao";
                Cardapio.DataValueField = "Id";
                Cardapio.DataBind();
            }

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                LinkButton l = (LinkButton)e.Row.FindControl("LinkButton1");
                l.Attributes.Add("onclick", "javascript:return " + "confirm('Deseja deletar " +
                DataBinder.Eval(e.Row.DataItem, "Descricao") + "'?)");
            }
        }

        protected async void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Deletar")
            {
                int Id = Convert.ToInt32(e.CommandArgument);

                HttpClient httpClient = new HttpClient();
                httpClient.BaseAddress = new Uri(ip);
                await httpClient.DeleteAsync("/20131011110061/api/fila/" + Id);

                Reload();
            }
        }
    }
}