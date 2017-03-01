using Acr.UserDialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace Smalldebts.Core.UI.Views
{
    public partial class LoginPage : ContentPage
    { // Track whether the user has authenticated.
        bool authenticated = false;

        bool signUp = false;

        public event EventHandler LoggedIn;

        public LoginPage()
        {
            InitializeComponent();
            LoginPanel.IsVisible = !signUp;
            SignupPanel.IsVisible = signUp;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            // Refresh items only when authenticated.
            if (authenticated == true)
            {
                // Set syncItems to true in order to synchronize the data
                // on startup when running in offline mode.
                //await RefreshItems(true, syncItems: false);

                // Hide the Sign-in button.
                this.LoginButton.IsVisible = false;
            }
        }

        void ActionButtonClicked(object sender, EventArgs e)
        {
            signUp = !signUp;
            LoginPanel.IsVisible = !signUp;
            SignupPanel.IsVisible = signUp;
        }


        Random r = new Random();
        private async void LoginButtonClicked(object sender, EventArgs e)
        {
            await Task.Delay(500);
            var result = r.Next(0, 152) % 2 == 0;

            if(result)
            {
                App.LoggedIn = true;
                LoggedIn?.Invoke(this, new EventArgs());
                await Navigation.PopAsync();
            }
            else
            {
                UserDialogs.Instance.ShowError("Wooops, credenciales invalidas", 1000);
            }
        }

        private async void SignUpButtonClicked(object sender, EventArgs e)
        {
            await Task.Delay(1000);
            await UserDialogs.Instance.AlertAsync("Cuenta creada chido");
        }
    }
}
