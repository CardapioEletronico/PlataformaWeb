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
    public partial class WebGridPedidos : System.Web.UI.Page
    {
        private string ip = "http://10.21.0.137";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                getPedidos();
            }

        }

        public async void getPedidos()
        {
            HttpClient httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri(ip);

            var response = await httpClient.GetAsync("/20131011110061/api/itempedido");
            var str = response.Content.ReadAsStringAsync().Result;
            List<Models.ItemPedido> obj = JsonConvert.DeserializeObject<List<Models.ItemPedido>>(str);

            var response2 = await httpClient.GetAsync("/20131011110061/api/produto");
            var str2 = response2.Content.ReadAsStringAsync().Result;
            List<Models.Produto> obj2 = JsonConvert.DeserializeObject<List<Models.Produto>>(str2);

            var result = from Models.ItemPedido ip in obj
                         join Models.Produto p in obj2
                         on ip.Produto_Id equals p.Id
                         select ip.ComProduto(p);

            GridView1.DataSource = result.ToList();
            GridView1.DataBind();
        }

        protected void OnRowEditing(object sender, GridViewEditEventArgs e)
        {
            GridView1.EditIndex = e.NewEditIndex;
            getPedidos();
            GridView1.DataBind();
        }

        protected async  void OnRowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            GridViewRow row = GridView1.Rows[e.RowIndex];
            int itemPedidoId = Convert.ToInt32(GridView1.DataKeys[e.RowIndex].Values[0]);

            HttpClient httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri(ip);

            var response = await httpClient.GetAsync("/20131011110061/api/itempedido");
            var str = response.Content.ReadAsStringAsync().Result;
            List<Models.ItemPedido> obj = JsonConvert.DeserializeObject<List<Models.ItemPedido>>(str);
            Models.ItemPedido item = (from Models.ItemPedido f in obj where f.Id == itemPedidoId select f).Single();

            item.Situacao = int.Parse((row.FindControl("txtSituacao") as TextBox).Text);

            var content = new StringContent(JsonConvert.SerializeObject(item), Encoding.UTF8, "application/json");
            await httpClient.PutAsync("/20131011110061/api/itempedido/" + item.Id, content);

            GridView1.EditIndex = -1;
            GridView1.DataBind();

            getPedidos();
        }

        protected void OnRowCancelingEdit(object sender, EventArgs e)
        {
            GridView1.EditIndex = -1;
            GridView1.DataBind();
        }


        protected async void OnRowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int itemPedidoId = Convert.ToInt32(GridView1.DataKeys[e.RowIndex].Values[0]);

            HttpClient httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri(ip);

            httpClient.BaseAddress = new Uri(ip);
            var response = await httpClient.GetAsync("/20131011110061/api/itempedido");
            var str = response.Content.ReadAsStringAsync().Result;
            List<Models.ItemPedido> obj = JsonConvert.DeserializeObject<List<Models.ItemPedido>>(str);

            Models.ItemPedido item = (from Models.ItemPedido f in obj where f.Id == itemPedidoId  select f).Single();

            await httpClient.DeleteAsync("/20131011110061/api/itempedido/" + item.Id);

            getPedidos();
        }


        protected async void GridView1_RowCommand1(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "AtenderPedido")
            {
                int itemPedidoId = (int)GridView1.DataKeys[Convert.ToInt32(e.CommandArgument)].Value;

                HttpClient httpClient = new HttpClient();
                httpClient.BaseAddress = new Uri(ip);

                var response = await httpClient.GetAsync("/20131011110061/api/itempedido");
                var str = response.Content.ReadAsStringAsync().Result;
                List<Models.ItemPedido> obj = JsonConvert.DeserializeObject<List<Models.ItemPedido>>(str);
                Models.ItemPedido item = (from Models.ItemPedido f in obj where f.Id == itemPedidoId select f).Single();

                item.Situacao = 2;

                var content = new StringContent(JsonConvert.SerializeObject(item), Encoding.UTF8, "application/json");
                await httpClient.PutAsync("/20131011110061/api/itempedido/" + item.Id, content);

                GridView1.EditIndex = -1;
                GridView1.DataBind();

                getPedidos();

            }

            if(e.CommandName == "CancelarPedido")
            {
                int itemPedidoId = (int)GridView1.DataKeys[Convert.ToInt32(e.CommandArgument)].Value;

                HttpClient httpClient = new HttpClient();
                httpClient.BaseAddress = new Uri(ip);

                httpClient.BaseAddress = new Uri(ip);
                var response = await httpClient.GetAsync("/20131011110061/api/itempedido");
                var str = response.Content.ReadAsStringAsync().Result;
                List<Models.ItemPedido> obj = JsonConvert.DeserializeObject<List<Models.ItemPedido>>(str);

                Models.ItemPedido item = (from Models.ItemPedido f in obj where f.Id == itemPedidoId select f).Single();

                await httpClient.DeleteAsync("/20131011110061/api/itempedido/" + item.Id);

                getPedidos();
            }
        }

        /*protected void OnRowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow && e.Row.RowIndex != GridView1.EditIndex)
            {
                (e.Row.Cells[2].Controls[2] as LinkButton).Attributes["onclick"] = "return confirm('Do you want to delete this row?');";
            }
        }*/

    }
}