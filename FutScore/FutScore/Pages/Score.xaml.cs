using Newtonsoft.Json;
using System;
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
using System.Collections;

// O modelo de item de Página em Branco está documentado em https://go.microsoft.com/fwlink/?LinkId=234238

namespace FutScore.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Score : ContentPage
    {
        Resultado resultado;
        List<Cartao> cartoes;
        List<Gols> gols;
        List<Falta> faltas;
        List<Escalacao> escalacao;
        List<Escanteio> escanteios;
        List<Penalti> penaltis;
        List<Subtituicao> subtituicaos;

        int mandante;
        int visitante;

        public class listViewResult
        {
            string title;
            string text;

            public string Title { get => title; set => title = value; }
            public string Text { get => text; set => text = value; }
        }

        public Score(int id)
        {
            this.InitializeComponent();
            AtualizaDados(id);
        }

        private async void AtualizaDados(int id)
        {
            resultado = await buscarResultado(id);
            ResultadoLista.ItemsSource = ListarResultado(resultado);
            cartoes = await buscarCartoes(id);
            CartaoLista.ItemsSource = ListarCartoes(); ;
            gols = await buscarGols(id);
            GolLista.ItemsSource = ListarGols();
            faltas = await buscarFaltas(id);
            FaltaLista.ItemsSource = ListarFaltas();
            escalacao = await buscarEscalacao(id);
            EscalacaoLista.ItemsSource = ListarEscalacao();
            escanteios = await buscarEscanteios(id);
            EscanteioLista.ItemsSource = ListarEscanteio();
            penaltis = await buscarPenaltis(id);
            PenaltiLista.ItemsSource = ListarPenalti();
            subtituicaos = await buscarSubstituicoes(id);
            SubstituicaoLista.ItemsSource = ListarSubstituicoes();
        }

        private IEnumerable ListarSubstituicoes()
        {
            List<listViewResult> lista = new List<listViewResult>();
            foreach (var item in subtituicaos)
            {
                listViewResult list = new listViewResult();
                list.Title = "Saiu: " + item.jogadorSaiu.nome + " Entrou: " + item.jogadorEntrou.nome;
                list.Text = "";
                lista.Add(list);
            }
            return lista;
        }

        private IEnumerable ListarPenalti()
        {
            var penaltis1 = penaltis.Where(a => a.time.id == mandante).Count();
            var penaltis2 = penaltis.Where(a => a.time.id == visitante).Count();
            List<listViewResult> lista = new List<listViewResult>();
            listViewResult list = new listViewResult();
            list.Title = "Penaltis: " + penaltis1 + " x " + penaltis2;
            list.Text = "";
            lista.Add(list);
            return lista;
        }

        private IEnumerable ListarEscanteio()
        {
            var escanteio1 = escanteios.Where(a => a.time.id == mandante).Count();
            var escanteio2 = escanteios.Where(a => a.time.id == visitante).Count();
            List<listViewResult> lista = new List<listViewResult>();
            listViewResult list = new listViewResult();
            list.Title = "Escanteios: " + escanteio1 + " x " + escanteio2;
            list.Text = "";
            lista.Add(list);
            return lista;
        }

        private IEnumerable ListarEscalacao()
        {
            List<listViewResult> lista = new List<listViewResult>();
            listViewResult list = new listViewResult();
            list.Title = "Time: " + resultado.jogo.mandante.nome;
            list.Text = "";
            lista.Add(list);
            foreach (var item in escalacao.Where(a => a.time.id == mandante))
            {
                list = new listViewResult();
                list.Title = "Jogador: " + item.jogador.nome + " Posicao: " + item.posicao;
                list.Text = "";
                lista.Add(list);
            }
            list = new listViewResult();
            list.Title = "Time: " + resultado.jogo.visitante.nome;
            list.Text = "";
            lista.Add(list);
            foreach (var item in escalacao.Where(a => a.time.id == visitante))
            {
                list = new listViewResult();
                list.Title = "Jogador: " + item.jogador.nome + " Posicao: " + item.posicao;
                list.Text = "";
                lista.Add(list);
            }
            return lista;
        }

        private IEnumerable ListarFaltas()
        {
            var falta1 = faltas.Where(a => a.time.id == mandante).Count();
            var falta2 = faltas.Where(a => a.time.id == visitante).Count();
            List<listViewResult> lista = new List<listViewResult>();
            listViewResult list = new listViewResult();
            list.Title = "Faltas: " + falta1 + " x " + falta2;
            list.Text = "";
            lista.Add(list);
            return lista;
        }

        private IEnumerable ListarGols()
        {
            string tipo = "Gol";
            List<listViewResult> lista = new List<listViewResult>();
            foreach (var item in gols)
            {
                if(item.penalti)
                {
                    tipo = "Penalti";
                }
                if(item.falta)
                {
                    tipo = "Falta";
                }
                if(item.escanteio)
                {
                    tipo = "Escanteio";
                }
                listViewResult list = new listViewResult();
                list.Title = item.time.nome + ": " + item.jogador.nome;
                list.Text = tipo;
                lista.Add(list);
                tipo = "Gol";
            }
            return lista;
        }

        private IEnumerable ListarCartoes()
        {
            List<listViewResult> lista = new List<listViewResult>();
            foreach (var item in cartoes)
            {
                listViewResult list = new listViewResult();
                list.Title = item.jogador.nome;
                list.Text = item.cartao.ToString();
                lista.Add(list);
            }
            return lista;
        }

        private List<listViewResult> ListarResultado(Resultado resultado)
        {
            List<listViewResult> lista = new List<listViewResult>();
            listViewResult list = new listViewResult();
            list.Title = resultado.jogo.mandante.nome + ": " + resultado.gols1 + " x " + resultado.gols2 + " :" + resultado.jogo.visitante.nome;
            list.Text = resultado.jogo.mandante.nome + ": " + resultado.posse1 + "% x %" + resultado.posse2 + " :" + resultado.jogo.visitante.nome;
            lista.Add(list);
            return lista;
        }

        private async Task<Resultado> buscarResultado(int id)
        {
            HttpClient _client = new HttpClient();
            var response = await _client.GetStringAsync("http://futscoreapi.azurewebsites.net/api/Resultados?id="+id);
            var resultados = JsonConvert.DeserializeObject<Resultado>(response);
            mandante = resultados.jogo.mandante.id;
            visitante = resultados.jogo.visitante.id;
            return resultados;
        }

        private async Task<List<Cartao>> buscarCartoes(int id)
        {
            HttpClient _client = new HttpClient();
            var response = await _client.GetStringAsync("http://futscoreapi.azurewebsites.net/api/Cartaos/Jogos?codigo=" + id);
            var cartoes = JsonConvert.DeserializeObject<List<Cartao>>(response);
            return cartoes;
        }

        private async Task<List<Gols>> buscarGols(int id)
        {
            HttpClient _client = new HttpClient();
            var response = await _client.GetStringAsync("http://futscoreapi.azurewebsites.net/api/Gols/Jogos?codigo=" + id);
            var gols = JsonConvert.DeserializeObject<List<Gols>>(response);
            return gols;
        }

        private async Task<List<Escalacao>> buscarEscalacao(int id)
        {
            HttpClient _client = new HttpClient();
            var response = await _client.GetStringAsync("http://futscoreapi.azurewebsites.net/api/Escalacaos/Jogo?codigo=" + id);
            var escalacao = JsonConvert.DeserializeObject<List<Escalacao>>(response);
            return escalacao;
        }

        private async Task<List<Falta>> buscarFaltas(int id)
        {
            HttpClient _client = new HttpClient();
            var response = await _client.GetStringAsync("http://futscoreapi.azurewebsites.net/api/Faltas/Jogos?codigo=" + id);
            var faltas = JsonConvert.DeserializeObject<List<Falta>>(response);
            return faltas;
        }

        private async Task<List<Penalti>> buscarPenaltis(int id)
        {
            HttpClient _client = new HttpClient();
            var response = await _client.GetStringAsync("http://futscoreapi.azurewebsites.net/api/Penaltis/Jogo?codigo=" + id);
            var penaltis = JsonConvert.DeserializeObject<List<Penalti>>(response);
            return penaltis;
        }

        private async Task<List<Subtituicao>> buscarSubstituicoes(int id)
        {
            HttpClient _client = new HttpClient();
            var response = await _client.GetStringAsync("http://futscoreapi.azurewebsites.net/api/Subtituicao/Jogos?codigo=" + id);
            var substituicoes = JsonConvert.DeserializeObject<List<Subtituicao>>(response);
            return substituicoes;
        }
        private async Task<List<Escanteio>> buscarEscanteios(int id)
        {
            HttpClient _client = new HttpClient();
            var response = await _client.GetStringAsync("http://futscoreapi.azurewebsites.net/api/Escanteios/Jogos?codigo=" + id);
            var escanteios = JsonConvert.DeserializeObject<List<Escanteio>>(response);
            return escanteios;
        }
    }
}
