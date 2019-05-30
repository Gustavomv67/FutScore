using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FutScore.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FutScore : ContentPage
    {
        public class listViewResult
        {
            int id;
            string title;
            string text;

            public string Title { get => title; set => title = value; }
            public string Text { get => text; set => text = value; }
            public int Id { get => id; set => id = value; }
        }

        List<Resultado> items;
        public FutScore()
        {
            InitializeComponent();
            AtualizaDados();
        }

        private async void AtualizaDados()
        {
            items = await buscarJogos();
            ResultadoLista.ItemsSource = Listar();
        }

        private async Task<List<Resultado>> buscarJogos()
        {
            HttpClient _client = new HttpClient();
            var response = await _client.GetStringAsync("http://futscoreapi.azurewebsites.net/api/Resultados");
            var resultados = JsonConvert.DeserializeObject<List<Resultado>>(response);
            return resultados;
        }

        public List<listViewResult> Listar(string filtro = "")
        {
            List<listViewResult> lista = new List<listViewResult>();
            foreach (var item in items)
            {
                listViewResult list = new listViewResult();
                list.Id = item.jogo.id;
                list.Title = item.jogo.mandante.nome + " " + item.gols1.ToString() + " x " + item.gols2.ToString() + " " + item.jogo.visitante.nome;
                list.Text = "Posse de bola: " + item.posse1.ToString() + " x " + item.posse2.ToString() + " Data: " + item.jogo.data.ToShortDateString();
                lista.Add(list);
            }
            return lista;
        }

        private void Resultado_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            listViewResult item = (listViewResult)ResultadoLista.SelectedItem;
            Navigation.PushModalAsync(new Score(item.Id));

        }
    }
}