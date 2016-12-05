using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Web;
using System.Web.Management;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;
using System.Web.UI.HtmlControls;

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
            else if ("AdmRest" == Session["Permissao"])
            {
                Label titulo = Master.FindControl("titulo") as Label;
                titulo.Text = "Gerenciamento de Produtos";
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
            var base64String = Convert.ToBase64String(FileUpload1.FileBytes);
            HttpClient httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri(ip);
            Models.Produto f = new Models.Produto
            {
                Descricao = textBoxDesc.Text,
                NomeDescricao = textBoxNomeDescr.Text,
                Cardapio_id = int.Parse(Cardapios.SelectedItem.Value),
                Fila_id = int.Parse(Filas.SelectedItem.Value),
                Foto = base64String,
                Preco = double.Parse(textBoxPreco.Text),
                ArquivoFoto = "Imagens/" + DateTime.Now.ToString("yyyyMMddHHmmss") + FileUpload1.FileName,
            };

            FileUpload1.PostedFile.SaveAs(Server.MapPath("~/Imagens/" + DateTime.Now.ToString("yyyyMMddHHmmss") + FileUpload1.FileName));

            string s = JsonConvert.SerializeObject(f);

            var content = new StringContent(s, Encoding.UTF8, "application/json");

            await httpClient.PostAsync("/20131011110061/api/produto", content);

            Reload();
        }

        protected async void btnUpdate_Click(object sender, EventArgs e)
        {
            var base64String = Convert.ToBase64String(FileUpload1.FileBytes);
            HttpClient httpClient = new HttpClient();

            httpClient.BaseAddress = new Uri(ip);
            Models.Produto f = new Models.Produto
            {
                Id = int.Parse(textBoxId.Text),
                Descricao = textBoxDesc.Text,
                Cardapio_id = int.Parse(Cardapios.SelectedValue),
                ArquivoFoto = "Imagens/" + DateTime.Now.ToString("yyyyMMddHHmmss") + FileUpload1.FileName,
                Foto = base64String,
                NomeDescricao = textBoxNomeDescr.Text,
                Fila_id = int.Parse(Filas.SelectedItem.Value),
                Preco = double.Parse(textBoxPreco.Text),
            };

            int idRest = Convert.ToInt16(Session["idRest"]);

            var response = await httpClient.GetAsync("/20131011110061/api/produto");
            var str = response.Content.ReadAsStringAsync().Result;
            List<Models.Produto> obj = JsonConvert.DeserializeObject<List<Models.Produto>>(str);
            var deletarFoto = (from Models.Produto p in obj where p.Id == f.Id select p).Single();

            string strPhysicalFolder = Server.MapPath("~/" + deletarFoto.ArquivoFoto);
            string strFileFullPath = strPhysicalFolder;
            if (System.IO.File.Exists(strFileFullPath))
            {
                System.IO.File.Delete(strFileFullPath);
            }

            FileUpload1.PostedFile.SaveAs(Server.MapPath("~/Imagens/" + DateTime.Now.ToString("yyyyMMddHHmmss") + FileUpload1.FileName));

            var content = new StringContent(JsonConvert.SerializeObject(f), Encoding.UTF8, "application/json");
            await httpClient.PutAsync("/20131011110061/api/produto/" + f.Id, content);

            Reload();
        }

        protected async void btnDelete_Click(object sender, EventArgs e)
        {
            HttpClient httpClient = new HttpClient();

            httpClient.BaseAddress = new Uri(ip);

            await httpClient.DeleteAsync("/20131011110061/api/produto/" + textBoxId.Text);

            Reload();
        }

        protected async void Reload()
        {
            int idRest = Convert.ToInt16(Session["idRest"]);
            HttpClient httpClient = new HttpClient();

            httpClient.BaseAddress = new Uri(ip);
            var response = await httpClient.GetAsync("/20131011110061/api/produto");
            var str = response.Content.ReadAsStringAsync().Result;
            List<Models.Produto> obj = JsonConvert.DeserializeObject<List<Models.Produto>>(str);

            var response2 = await httpClient.GetAsync("/20131011110061/api/cardapio");
            var str2 = response2.Content.ReadAsStringAsync().Result;
            List<Models.Cardapio> obj2 = JsonConvert.DeserializeObject<List<Models.Cardapio>>(str2);

            List<Models.Cardapio> Cardapios = (from Models.Cardapio f in obj2 where f.Restaurante_id == idRest select f).ToList();
            List<Models.Produto> Produtos = new List<Models.Produto>();
            foreach (Models.Cardapio c in Cardapios)
            {
               foreach (Models.Produto p in obj)
                {
                    if (p.Cardapio_id == c.Id)
                        Produtos.Add(p);
                }
            }

            GridView1.DataSource = Produtos;
            GridView1.DataBind();

            if (!IsPostBack) DropRest();
            if (!IsPostBack) DropFilas();
        }

        public async void DropRest()
        {
            int idRest = Convert.ToInt32(Session["idRest"]);
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

        public async void DropFilas()
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

            List<Models.Cardapio> obj3 = new List<Models.Cardapio>();
            List<Models.Fila> obj4 = new List<Models.Fila>();

            foreach (Models.Cardapio x in cardapiolista)
                if (x.Restaurante_id == idRest)
                    obj3.Add(x);

            foreach (Models.Fila y in filalista)
            {
                foreach (Models.Cardapio o in obj3)
                    if (y.Cardapio_id == o.Id)
                        obj4.Add(y);
            }

            Filas.DataSource = obj4;
            Filas.DataTextField = "Descricao";
            Filas.DataValueField = "Id";
            Filas.DataBind();
        }

        public System.Drawing.Image Base64ToImage(string base64String)
        {
            byte[] imageBytes = Convert.FromBase64String(base64String);
            MemoryStream ms = new MemoryStream(imageBytes, 0,
              imageBytes.Length);
            ms.Write(imageBytes, 0, imageBytes.Length);
            System.Drawing.Image image = System.Drawing.Image.FromStream(ms, true);
            return image;
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

            var response = await httpClient.GetAsync("/20131011110061/api/produto");
            var str = response.Content.ReadAsStringAsync().Result;
            List<Models.Produto> obj = JsonConvert.DeserializeObject<List<Models.Produto>>(str);
            Models.Produto item = (from Models.Produto f in obj where f.Id == Id select f).Single();

            item.Descricao = (row.FindControl("txtDescricao") as TextBox).Text;

            var content = new StringContent(JsonConvert.SerializeObject(item), Encoding.UTF8, "application/json");
            await httpClient.PutAsync("/20131011110061/api/produto/" + item.Id, content);

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
            await httpClient.DeleteAsync("/20131011110061/api/produto/" + Id);

            Reload();
        }


        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                LinkButton l = (LinkButton)e.Row.FindControl("LinkButton1");
                l.Attributes.Add("onclick", "javascript:return " + "confirm('Deseja deletar " +
                DataBinder.Eval(e.Row.DataItem, "NomeDescricao") + "'?)");
            }
        }

        protected async void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Deletar")
            {
                int Id = Convert.ToInt32(e.CommandArgument);

                HttpClient httpClient = new HttpClient();
                httpClient.BaseAddress = new Uri(ip);
                await httpClient.DeleteAsync("/20131011110061/api/produto/" + Id);

                Reload();
            }
        }
    }
}