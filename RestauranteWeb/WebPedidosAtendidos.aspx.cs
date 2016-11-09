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
    public partial class WebPedidosAtendidos : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Table1.Rows.Clear();
            Reload();
        }

        private string ip = "http://10.21.0.137";

        protected void Page_PreInit(object sender, EventArgs e)
        {
            if (Session["Login"] == null)
            {
                Response.Write("<script>window.alert('Faça seu login para acessar esse link.'); self.location = 'LoginGerentePedido.aspx';</script>)");
            }
            else if ("GerentePedidos" == Session["Permissao"])
            {
                Label titulo = Master.FindControl("titulo") as Label;
                titulo.Text = "Visualizar Pedidos Atendidos";
                Label labelu = Master.FindControl("LabelUsuario") as Label;
                if (Session["Login"] != null) labelu.Text = Session["Login"].ToString();
            }
            else
            {
                Session["Login"] = null;
                Response.Write("<script>window.alert('Você não tem permissão para acessar esse link.'); self.location = 'LoginGerentePedido.aspx';</script>)");
            }
        }

        protected async void Reload()
        {
            int idRest = Convert.ToInt16(Session["idRest"]);
            int idFila = Convert.ToInt16(Session["Fila"]);

            HttpClient httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri(ip);

            var response = await httpClient.GetAsync("/20131011110061/api/cardapio");
            var str = response.Content.ReadAsStringAsync().Result;
            List<Models.Cardapio> obj = JsonConvert.DeserializeObject<List<Models.Cardapio>>(str);
            List<Models.Cardapio> listaCardapios = (from Models.Cardapio c in obj where c.Restaurante_id == idRest select c).ToList();

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

            List<Models.Produto> listaProdutoFila = new List<Models.Produto>();
            foreach (Models.Produto p in listaProdutos)
            {
                if (p.Fila_id == idFila)
                {
                    listaProdutoFila.Add(p);
                }
            }
            //(from Models.Produto prod in listaProdutos where prod.Fila_id == idFila select prod).ToList();
            int xablau = listaProdutoFila.Count;
            var response3 = await httpClient.GetAsync("/20131011110061/api/itempedido");
            var str3 = response3.Content.ReadAsStringAsync().Result;
            List<Models.ItemPedido> obj3 = JsonConvert.DeserializeObject<List<Models.ItemPedido>>(str3);

            //Condicao de estar aberto
            obj3 = (from Models.ItemPedido ip in obj3 where ip.Situacao == 2 select ip).ToList();

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

            Table1.Rows.Clear();

            TableHeaderRow th = new TableHeaderRow();
            TableHeaderCell thc = new TableHeaderCell();
            thc.Text = "ID";
            thc.Width = 150;

            TableHeaderCell thc1 = new TableHeaderCell();
            thc1.Text = "Quantidade";

            TableHeaderCell thc2 = new TableHeaderCell();
            thc2.Text = "Hora";

            TableHeaderCell thc3 = new TableHeaderCell();
            thc3.Text = "Situacao";

            TableHeaderCell thc4 = new TableHeaderCell();
            thc4.Text = "Produto";

            TableHeaderCell thc5 = new TableHeaderCell();
            thc5.Text = "Pedido_ID";

            th.Cells.Add(thc);
            th.Cells.Add(thc1);
            th.Cells.Add(thc2);
            th.Cells.Add(thc3);
            th.Cells.Add(thc4);
            th.Cells.Add(thc5);

            Table1.Rows.Add(th);

            foreach (Models.ItemPedido x in listaItemPedidos)
            {
                Label lb2 = new Label();
                lb2.Text = x.ToString();
                TableRow tRow = new TableRow();

                TableCell tc = new TableCell();
                tc.Text = x.Id.ToString();
                TableCell tc2 = new TableCell();
                tc2.Text = x.Quantidade.ToString();
                TableCell tc3 = new TableCell();
                tc3.Text = x.Hora.ToString();
                TableCell tc4 = new TableCell();
                if (x.Situacao == 1)
                    tc4.Text = "Aberto";
                else
                    tc4.Text = "Fechado";

                TableCell tc5 = new TableCell();
                var prod = (from Models.Produto f in listaProdutoFila where f.Id == x.Produto_Id select f).Single();
                tc5.Text = prod.NomeDescricao.ToString();

                TableCell tc6 = new TableCell();
                tc6.Text = x.Pedido_Id.ToString();

                tRow.Cells.Add(tc);
                tRow.Cells.Add(tc2);
                tRow.Cells.Add(tc3);
                tRow.Cells.Add(tc4);
                tRow.Cells.Add(tc5);
                tRow.Cells.Add(tc6);

                tRow.BorderStyle = BorderStyle.Ridge;
                tRow.BorderWidth = 1;

                Table1.Rows.Add(tRow);
            }
            //if (!IsPostBack) DropRest();
        }

        protected async void btnAtender_Click(object sender, EventArgs e)
        {
            HttpClient httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri(ip);

            httpClient.BaseAddress = new Uri(ip);
            var response = await httpClient.GetAsync("/20131011110061/api/itempedido");
            var str = response.Content.ReadAsStringAsync().Result;
            List<Models.ItemPedido> obj = JsonConvert.DeserializeObject<List<Models.ItemPedido>>(str);

            Models.ItemPedido item = (from Models.ItemPedido f in obj where f.Id == int.Parse(textBoxId.Text) select f).Single();
            item.Situacao = 1;

            var content = new StringContent(JsonConvert.SerializeObject(item), Encoding.UTF8, "application/json");
            await httpClient.PutAsync("/20131011110061/api/itempedido/" + item.Id, content);

            Reload();
        }
    }
}