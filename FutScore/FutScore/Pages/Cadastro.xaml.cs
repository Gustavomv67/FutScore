using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FutScore.Pages
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Cadastro : ContentPage
	{
		public Cadastro ()
		{
			InitializeComponent ();
            CancelButton.Clicked += CancelButton_Clicked;
            RegisterButton.Clicked += RegisterButton_Clicked;
		}

        private async void RegisterButton_Clicked(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(EmailEntry.Text))
            {
                await DisplayAlert("Erro", "Digite um email valido", "Aceitar");
                EmailEntry.Focus();
                return;
            }
            if (string.IsNullOrEmpty(SennhaEntry.Text))
            {
                await DisplayAlert("Erro", "Digite uma senha", "Aceitar");
                EmailEntry.Focus();
                return;
            }
            if(SennhaEntry.Text != ConfirmaSennhaEntry.Text)
            {
                await DisplayAlert("Erro", "Senhas estão diferentes", "Aceitar");
                EmailEntry.Focus();
                return;
            }
            this.Cadastrar();
        }

        private async void Cadastrar()
        {
            Usuario usuario = new Usuario();
            usuario.email = EmailEntry.Text;
            usuario.senha = SennhaEntry.Text;
            HttpClient _client = new HttpClient();
            var uri = "http://futscoreapi.azurewebsites.net/api/Usuarios";
            var myContent = JsonConvert.SerializeObject(usuario);
            var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            var result = await _client.PostAsync(uri, byteContent);
            if (result.IsSuccessStatusCode)
            {
                await Navigation.PushModalAsync(new FutScore());
            }
            else
            {
                await DisplayAlert("Erro", "Ocorreu um erro ao cadastrar o usuario", "Ok");
            }
        }

        private async void CancelButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }
    }
}