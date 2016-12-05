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
            else if (CheckBoxList1.SelectedItem.Text == "Não")
                f.Disponivel = false;
            else
                f.Disponivel = true;

            string s = JsonConvert.SerializeObject(f);
            var content = new StringContent(s, Encoding.UTF8, "application/json");
            await httpClient.PostAsync("/20131011110061/api/mesa", content);
            Reload();
        }

        /*protected async void btnUpdate_Click(object sender, EventArgs e)
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
        }*/


        protected async void Reload()
        {
            int idRest = Convert.ToInt16(Session["idRest"]);
            HttpClient httpClient = new HttpClient();

            httpClient.BaseAddress = new Uri(ip);
            var response = await httpClient.GetAsync("20131011110061/api/mesa");
            var str = response.Content.ReadAsStringAsync().Result;
            List<Models.Mesa> obj = JsonConvert.DeserializeObject<List<Models.Mesa>>(str);

            var ListMesas = from mesa in obj where mesa.Restaurante_id == idRest orderby mesa.Disponivel descending select mesa;

            GridView1.DataSource = ListMesas;
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

            var response = await httpClient.GetAsync("/20131011110061/api/mesa");
            var str = response.Content.ReadAsStringAsync().Result;
            List<Models.Mesa> obj = JsonConvert.DeserializeObject<List<Models.Mesa>>(str);
            Models.Mesa item = (from Models.Mesa f in obj where f.Id == Id select f).Single();

            item.Numero = (row.FindControl("txtNumero") as TextBox).Text;

            var content = new StringContent(JsonConvert.SerializeObject(item), Encoding.UTF8, "application/json");
            await httpClient.PutAsync("/20131011110061/api/mesa/" + item.Id, content);

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
            await httpClient.DeleteAsync("/20131011110061/api/mesa/" + Id);

            Reload();
        }

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                LinkButton l = (LinkButton)e.Row.FindControl("LinkButton1");
                l.Attributes.Add("onclick", "javascript:return " + "confirm('Deseja deletar " +
                DataBinder.Eval(e.Row.DataItem, "Numero") + "'?)");
            }
        }

        protected async void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Deletar")
            {
                int Id = Convert.ToInt32(e.CommandArgument);

                HttpClient httpClient = new HttpClient();
                httpClient.BaseAddress = new Uri(ip);
                await httpClient.DeleteAsync("/20131011110061/api/mesa/" + Id);

                Reload();
            }

            else if (e.CommandName == "Situacao")
            {
                int Id = Convert.ToInt32(e.CommandArgument);

                HttpClient httpClient = new HttpClient();
                httpClient.BaseAddress = new Uri(ip);

                Reload();

                var response = await httpClient.GetAsync("/20131011110061/api/mesa");
                var str = response.Content.ReadAsStringAsync().Result;
                List<Models.Mesa> obj = JsonConvert.DeserializeObject<List<Models.Mesa>>(str);
                Models.Mesa item = (from Models.Mesa f in obj where f.Id == Id select f).Single();

                if (item.Disponivel)
                    item.Disponivel = false;
                else
                    item.Disponivel = true;

                var content = new StringContent(JsonConvert.SerializeObject(item), Encoding.UTF8, "application/json");
                await httpClient.PutAsync("/20131011110061/api/mesa/" + item.Id, content);

                GridView1.EditIndex = -1;
                GridView1.DataBind();

                Reload();
            }
        }

    }
}