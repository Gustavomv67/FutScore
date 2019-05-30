using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FutScore.Pages
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Login : ContentPage
	{
		public Login ()
		{
			InitializeComponent();
            this.Padding = Device.OnPlatform(new 
                Thickness(10, 20, 10, 10),
                new Thickness(10),
                new Thickness(10));

            LoginButton.Clicked += LoginButton_Clicked;
            RegisterButton.Clicked += RegisterButton_Clicked;
		}

        private async void RegisterButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Cadastro());
        }

        private async void LoginButton_Clicked(object sender, EventArgs e)
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
            this.Logar();
        }

        private async void Logar()
        {
            HttpClient _client = new HttpClient();
            var uri = "http://futscoreapi.azurewebsites.net/api/Usuarios/Login?email=" + EmailEntry.Text + "&senha="+SennhaEntry.Text;
            var result = await _client.GetAsync(uri);
            if (result.IsSuccessStatusCode)
            {
                await Navigation.PushModalAsync(new FutScore());
            }
            else
            {
                await DisplayAlert("Usuario não encontrado", "Email ou senha incorretos", "Ok");
            }
            
        }
    }
}