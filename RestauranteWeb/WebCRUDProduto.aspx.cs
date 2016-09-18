﻿using Newtonsoft.Json;
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
    public partial class CRUDProduto : System.Web.UI.Page
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
                titulo.Text = "Gerenciamento de Produtos";
                Label labelu = Master.FindControl("LabelUsuario") as Label;
                if (Session["Login"] != null) labelu.Text = Session["Login"].ToString();
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            Carregar();
        }

        public async void DropRest()
        {
            HttpClient httpClient = new HttpClient();

            httpClient.BaseAddress = new Uri(ip);
            //var response = await httpClient.GetAsync("/20131011110061/api/restaurante");
            var response = await httpClient.GetAsync("/20131011110061/api/cardapio");
            var str = response.Content.ReadAsStringAsync().Result;

            List<Models.Cardapio> obj = JsonConvert.DeserializeObject<List<Models.Cardapio>>(str);

            Cardapios.DataSource = obj;
            Cardapios.DataTextField = "Descricao";
            Cardapios.DataValueField = "Id";
            Cardapios.DataBind();

        }

        protected void btnSelect_Click(object sender, EventArgs e)
        {
            Carregar();
        }

        protected async void btnInsert_Click(object sender, EventArgs e)
        {
            HttpClient httpClient = new HttpClient();

            httpClient.BaseAddress = new Uri(ip);
            Models.Produto f = new Models.Produto
            {
                Id = int.Parse(textBoxId.Text),
                Descricao = textBoxDesc.Text,
                NomeDescricao = textBoxNomeDescr.Text,
                Cardapio_id = int.Parse(Cardapios.SelectedItem.Value),
                Preco = double.Parse(textBoxPreco.Text),
            };

            List<Models.Produto> fl = new List<Models.Produto>();

            fl.Add(f);

            string s = "=" + JsonConvert.SerializeObject(fl);

            var content = new StringContent(s, Encoding.UTF8, "application/x-www-form-urlencoded");

            await httpClient.PostAsync("/20131011110061/api/cardapio", content);

            Carregar();
        }

        protected async void btnUpdate_Click(object sender, EventArgs e)
        {
            HttpClient httpClient = new HttpClient();

            httpClient.BaseAddress = new Uri(ip);
            Models.Produto f = new Models.Produto
            {
                Id = int.Parse(textBoxId.Text),
                Descricao = textBoxDesc.Text,
                Cardapio_id = int.Parse(Cardapios.SelectedValue)

            };

            string s = "=" + JsonConvert.SerializeObject(f);

            var content = new StringContent(s, Encoding.UTF8, "application/x-www-form-urlencoded");

            //await httpClient.PutAsync("/20131011110061/api/restaurante/" + f.Id, content);
            await httpClient.PutAsync("/20131011110061/api/cardapio/" + f.Id, content);
        }

        protected async void btnDelete_Click(object sender, EventArgs e)
        {
            HttpClient httpClient = new HttpClient();

            httpClient.BaseAddress = new Uri(ip);

            //await httpClient.DeleteAsync("/20131011110061/api/restaurante/" + textBoxId.Text);
            await httpClient.DeleteAsync("/20131011110061/api/cardapio/" + textBoxId.Text);
        }

        protected async void Carregar()
        {
            int idRest = Convert.ToInt16(Session["idRest"]);
            HttpClient httpClient = new HttpClient();

            httpClient.BaseAddress = new Uri(ip);
            var response = await httpClient.GetAsync("/20131011110061/api/produto");
            var str = response.Content.ReadAsStringAsync().Result;
            List<Models.Produto> obj = JsonConvert.DeserializeObject<List<Models.Produto>>(str);

            var response2 = await httpClient.GetAsync("/20131011110061/api/cardapio");
            var str2 = response.Content.ReadAsStringAsync().Result;
            List<Models.Cardapio> obj2 = JsonConvert.DeserializeObject<List<Models.Cardapio>>(str);

            /*List<Models.Cardapio> obj3 = new List<Models.Cardapio>();
            foreach (Models.Cardapio x in obj2)
            {
                if (x.Restaurante_id == idRest)
                {
                    obj3.Add(x);
                }
            }*/

            var ListaCardapios = from cardapio in obj2 where cardapio.Restaurante_id == idRest select cardapio;

            List<Models.Produto> Produtos = new List<Models.Produto>();
            foreach (Models.Produto prod in obj)
            {
                foreach(Models.Cardapio card in ListaCardapios)
                {
                    if(prod.Cardapio_id == card.Id)
                    {
                        Produtos.Add(prod);
                    }
                }
            }

            Label1.Text = "<h3>Produtos</h3>";
            foreach (Models.Produto x in Produtos)
            {
                Label lb2 = new Label();
                lb2.Text = x.ToString();
                TableRow tRow = new TableRow();

                TableCell tc = new TableCell();
                tc.Text = x.Id.ToString() + "  -";
                TableCell tc2 = new TableCell();
                tc2.Text = x.Descricao.ToString() + "  -";
                TableCell tc3 = new TableCell();
                tc3.Text = x.Cardapio_id.ToString();
                tRow.Cells.Add(tc);
                tRow.Cells.Add(tc2);
                tRow.Cells.Add(tc3);
                Table1.Rows.Add(tRow);
            }
            if (!IsPostBack) DropRest();
        }
    }
}