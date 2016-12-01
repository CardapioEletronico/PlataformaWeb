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
            else if("AdmRest" == Session["Permissao"]) { 
                Label titulo = Master.FindControl("titulo") as Label;
                titulo.Text = "Gerenciamento de Cardápios";
                Label labelu = Master.FindControl("LabelUsuario") as Label;
                if (Session["Login"] != null) labelu.Text = Session["Login"].ToString();
            }
            else
            {
                Response.Write("<script>window.alert('Você não tem permissão para acessar esse link.'); self.location = 'LoginAdmRest.aspx';</script>)");
            }
        }

        protected async void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                Reload();
            }
        }

        protected void btnSelect_Click(object sender, EventArgs e)
        {
            Reload();
        }

        protected async void btnInsert_Click(object sender, EventArgs e)
        {
            if (Page.IsValid) { 
                int idRest = Convert.ToInt16(Session["idRest"]);
                HttpClient httpClient = new HttpClient();

                httpClient.BaseAddress = new Uri(ip);
                Models.Cardapio f = new Models.Cardapio
                {
                    Descricao = textBoxDesc.Text,
                    Restaurante_id = idRest
                };

                string s = JsonConvert.SerializeObject(f);
                var content = new StringContent(s, Encoding.UTF8, "application/json");
                await httpClient.PostAsync("/20131011110061/api/cardapio", content);

                Reload();
            }
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
            
            /*foreach (Models.Cardapio x in obj)
            {
                if (x.Restaurante_id == idRest)
                {
                    obj3.Add(x);
                }
            }*/

            obj3 = (from Models.Cardapio c in obj where c.Restaurante_id == idRest orderby c.Descricao select c).ToList();

            GridView1.DataSource = obj3;
            GridView1.DataBind();
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

            var response = await httpClient.GetAsync("/20131011110061/api/cardapio");
            var str = response.Content.ReadAsStringAsync().Result;
            List<Models.Cardapio> obj = JsonConvert.DeserializeObject<List<Models.Cardapio>>(str);
            Models.Cardapio item = (from Models.Cardapio f in obj where f.Id == Id select f).Single();

            item.Descricao = (row.FindControl("txtDescricao") as TextBox).Text;

            var content = new StringContent(JsonConvert.SerializeObject(item), Encoding.UTF8, "application/json");
            await httpClient.PutAsync("/20131011110061/api/cardapio/" + item.Id, content);

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
            await httpClient.DeleteAsync("/20131011110061/api/cardapio/" + Id);

            Reload();
        }

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
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
                await httpClient.DeleteAsync("/20131011110061/api/cardapio/" + Id);

                Reload();
            }
        }
    }
}