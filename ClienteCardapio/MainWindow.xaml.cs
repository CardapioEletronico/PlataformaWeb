using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Net.Http;
using Newtonsoft.Json;

namespace ClienteCardapio
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        //private string ip = "http://10.21.0.137";
        private string ip = "http://localhost:3906/";
        private async void btnSelect_Click(object sender, RoutedEventArgs e)
        {
            HttpClient httpClient = new HttpClient();

            httpClient.BaseAddress = new Uri(ip);
            //var response = await httpClient.GetAsync("/20131011110061/api/restaurante");
            var response = await httpClient.GetAsync("/api/restaurante");

            var str = response.Content.ReadAsStringAsync().Result;

            List<Models.Restaurante> obj = JsonConvert.DeserializeObject<List<Models.Restaurante>>(str);
            ListBoxFabricantes.ItemsSource = obj;

        }

        private async void btnInsert_Click(object sender, RoutedEventArgs e)
        {
            HttpClient httpClient = new HttpClient();

            httpClient.BaseAddress = new Uri(ip);

            Models.Restaurante f = new Models.Restaurante
            {

                Id = int.Parse(textBoxId.Text),
                Descricao = textBoxDesc.Text
            };

            List<Models.Restaurante> fl = new List<Models.Restaurante>();

            fl.Add(f);

            string s = "=" + JsonConvert.SerializeObject(fl);

            var content = new StringContent(s, Encoding.UTF8,
            "application/x-www-form-urlencoded");

            await httpClient.PostAsync("/api/restaurante", content);

        }

        private async void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            HttpClient httpClient = new HttpClient();

            httpClient.BaseAddress = new Uri(ip);
            Models.Restaurante f = new Models.Restaurante
            {
                Id = int.Parse(textBoxId.Text),
                Descricao = textBoxDesc.Text
            };

            string s = "=" + JsonConvert.SerializeObject(f);

            var content = new StringContent(s, Encoding.UTF8, "application/x-www-form-urlencoded");

            //await httpClient.PutAsync("/20131011110061/api/restaurante/" + f.Id, content);
            await httpClient.PutAsync("/api/restaurante/" + f.Id, content);
        }

        private async void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            HttpClient httpClient = new HttpClient();

            httpClient.BaseAddress = new Uri(ip);

            await httpClient.DeleteAsync("/api/restaurante/" + textBoxId.Text);

        }
    }
}
