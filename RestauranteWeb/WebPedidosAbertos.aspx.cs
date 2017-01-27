using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace RestauranteWeb
{
    public partial class WebPedidosAbertos : System.Web.UI.Page
    {
        private string ip = "http://10.21.0.137";

        protected async void Page_PreInit(object sender, EventArgs e)
        {
            if (Session["Login"] == null)
            {
                Response.Write("<script>window.alert('Faça seu login para acessar esse link.'); self.location = 'LoginGerentePedido.aspx';</script>)");
            }
            else if ("GerentePedidos" == Session["Permissao"])
            {
                Label titulo = Master.FindControl("titulo") as Label;
                titulo.Text = "Visualizar Pedidos Abertos - ";
                Label labelu = Master.FindControl("LabelUsuario") as Label;
                if (Session["Login"] != null) labelu.Text = Session["Login"].ToString();

                if (Session["Fila"] != null)
                {
                    int idFila = Convert.ToInt16(Session["Fila"]);
                    HttpClient httpClient = new HttpClient();
                    httpClient.BaseAddress = new Uri(ip);
                    var response3 = await httpClient.GetAsync("/20131011110061/api/fila");
                    var str3 = response3.Content.ReadAsStringAsync().Result;
                    List<Models.Fila> c = JsonConvert.DeserializeObject<List<Models.Fila>>(str3);
                    var listafila = (from Models.Fila fila in c where fila.Id == idFila select fila).Single();

                    titulo.Text += listafila.Descricao.ToString();
                }
                else
                {
                    
                }

                
            }
            else
            {
                Session["Login"] = null;
                Response.Write("<script>window.alert('Você não tem permissão para acessar esse link.'); self.location = 'LoginGerentePedido.aspx';</script>)");
            }
        }

        protected async void Page_Load(object sender, EventArgs e)
        {
            if(!this.IsPostBack)
            {
                Reload();
                DropFila();
            }
        }

        protected async void Reload()
        {
            int idRest = Convert.ToInt16(Session["idRest"]);
            int idFila = Convert.ToInt16(Session["Fila"]);

            HttpClient httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri(ip);

            //Cardapios desse restaurante
            var response = await httpClient.GetAsync("/20131011110061/api/cardapio");
            var str = response.Content.ReadAsStringAsync().Result;
            List<Models.Cardapio> obj = JsonConvert.DeserializeObject<List<Models.Cardapio>>(str);
            List<Models.Cardapio> listaCardapios = (from Models.Cardapio c in obj where c.Restaurante_id == idRest select c).ToList();

            //Produtos que estão no cardapio desse restaurante
            var response2 = await httpClient.GetAsync("/20131011110061/api/produto");
            var str2 = response2.Content.ReadAsStringAsync().Result;
            List<Models.Produto> obj2 = JsonConvert.DeserializeObject<List<Models.Produto>>(str2);
            List<Models.Produto> listaProdutos = new List<Models.Produto>();

            foreach (Models.Cardapio c in listaCardapios)
            {
                foreach (Models.Produto p in obj2)
                {
                    if (p.Cardapio_id == c.Id)
                    {
                        listaProdutos.Add(p);
                    }
                }
            }

            //Produtos que estão na fila selecionada
            List<Models.Produto> listaProdutoFila = new List<Models.Produto>();
            foreach (Models.Produto p in listaProdutos)
            {
                if (p.Fila_id == idFila)
                {
                    listaProdutoFila.Add(p);
                }
            }

            var response3 = await httpClient.GetAsync("/20131011110061/api/itempedido");
            var str3 = response3.Content.ReadAsStringAsync().Result;
            List<Models.ItemPedido> obj3 = JsonConvert.DeserializeObject<List<Models.ItemPedido>>(str3);
            obj3 = (from Models.ItemPedido ip in obj3 where ip.Situacao == 1 select ip).ToList();

            List<Models.ItemPedido> listaItemPedidos = new List<Models.ItemPedido>();
            foreach (Models.Produto p in listaProdutoFila)
            {
                foreach (Models.ItemPedido ip in obj3)
                {
                    if (ip.Produto_Id == p.Id)
                    {
                        listaItemPedidos.Add(ip);
                    }
                }
            }

            var result = from Models.ItemPedido ip in listaItemPedidos
                         join Models.Produto p in listaProdutoFila
                         on ip.Produto_Id equals p.Id
                         select ip.ComProduto(p);

            result = (from Models.ItemPedido ip in result orderby ip.Hora.TimeOfDay descending select ip).ToList();

            GridView1.DataSource = result.ToList();
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

            Reload();
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

            Models.ItemPedido item = (from Models.ItemPedido f in obj where f.Id == itemPedidoId select f).Single();

            await httpClient.DeleteAsync("/20131011110061/api/itempedido/" + item.Id);

            Reload();
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


                var response5 = await httpClient.GetAsync("/20131011110061/api/produto");
                var str5 = response5.Content.ReadAsStringAsync().Result;
                List<Models.Produto> obj5 = JsonConvert.DeserializeObject<List<Models.Produto>>(str5);
                Models.Produto p = (from Models.Produto f in obj5 where f.Id == item.Produto_Id select f).Single();

                item.ComProduto(p);

                GridView1.EditIndex = -1;
                GridView1.DataBind();

                //Pegar a mesa
                //Pedidos
                var response3 = await httpClient.GetAsync("/20131011110061/api/pedido");
                var str3 = response3.Content.ReadAsStringAsync().Result;
                List<Models.Pedido> obj3 = JsonConvert.DeserializeObject<List<Models.Pedido>>(str3);
                Models.Pedido ped = (from Models.Pedido c in obj3 where c.Id == item.Pedido_Id select c).Single();

                //Mesas
                var response2 = await httpClient.GetAsync("/20131011110061/api/mesa");
                var str2 = response2.Content.ReadAsStringAsync().Result;
                List<Models.Mesa> obj2 = JsonConvert.DeserializeObject<List<Models.Mesa>>(str2);
                Models.Mesa mesa = (from Models.Mesa c in obj2 where c.Id == ped.Mesa_Id select c).Single();


                // get a temporary file name so we don't conflict with concurrent user
                string fileName = Path.GetRandomFileName();
                fileName = Path.ChangeExtension(fileName, "txt");

                // Now map the file to a path within the host directory
                string outputFileName = MapPath(fileName);

                // write some data to the file
                FileStream fs = new FileStream(outputFileName, FileMode.OpenOrCreate, FileAccess.Write);
                StreamWriter writer = new StreamWriter(fs);

                writer.WriteLine("Pedido: " + item.Produto.NomeDescricao);
                writer.WriteLine("Quantidade: " + item.Quantidade);
                writer.WriteLine("Mesa: " + mesa.Numero);
                writer.WriteLine(string.Empty);
                writer.WriteLine(DateTime.Now.ToLongTimeString());
                writer.Close();

                // now open the url in another browser tab
                Page.ClientScript.RegisterStartupScript(this.GetType(), "windowKey", "window.open('" + fileName + "');", true);

                Reload();

            }

            if (e.CommandName == "CancelarPedido")
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

                Reload();
            }
        }

        public async void DropFila()
        {
            int idRest = Convert.ToInt16(Session["idRest"]);

            HttpClient httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri(ip);
            var response3 = await httpClient.GetAsync("/20131011110061/api/cardapio");
            var str3 = response3.Content.ReadAsStringAsync().Result;
            List<Models.Cardapio> c = JsonConvert.DeserializeObject<List<Models.Cardapio>>(str3);
            var listaCardapio = from Models.Cardapio card in c where card.Restaurante_id == idRest select card;
            var response2 = await httpClient.GetAsync("/20131011110061/api/fila");
            var str2 = response2.Content.ReadAsStringAsync().Result;
            List<Models.Fila> fil = JsonConvert.DeserializeObject<List<Models.Fila>>(str2);

            List<Models.Fila> listafila = new List<Models.Fila>();
            foreach (Models.Cardapio c1 in listaCardapio)
            {
                foreach (Models.Fila f1 in fil)
                {
                    if (f1.Cardapio_id == c1.Id)
                    {
                        listafila.Add(f1);
                    }
                }
            }

            Filas.DataSource = listafila;
            Filas.DataTextField = "Descricao";
            Filas.DataValueField = "Id";
            Filas.DataBind();
        }

        protected async void btnTrocar_Click(object sender, EventArgs e)
        {
            Session["Fila"] = Filas.SelectedValue;
            Label titulo = Master.FindControl("titulo") as Label;
            titulo.Text = "Visualizar Pedidos Abertos - ";
            Label labelu = Master.FindControl("LabelUsuario") as Label;
            if (Session["Login"] != null) labelu.Text = Session["Login"].ToString();

            if (Session["Fila"] != null)
            {
                int idFila = Convert.ToInt16(Session["Fila"]);
                HttpClient httpClient = new HttpClient();
                httpClient.BaseAddress = new Uri(ip);
                var response3 = await httpClient.GetAsync("/20131011110061/api/fila");
                var str3 = response3.Content.ReadAsStringAsync().Result;
                List<Models.Fila> c = JsonConvert.DeserializeObject<List<Models.Fila>>(str3);
                var listafila = (from Models.Fila fila in c where fila.Id == idFila select fila).Single();

                titulo.Text += listafila.Descricao.ToString();
            }

            Reload();
        }

    }
}