using Acr.UserDialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.MobileServices;
using Smalldebts.Core.UI.DataAccess;
using Xamarin.Forms;

namespace Smalldebts.Core.UI.Views
{
    public partial class LoginPage : ContentPage
    {
        private readonly SmalldebtsManager _serviceClient;
        // Track whether the user has authenticated.
        bool authenticated = false;

        bool signUp = false;

        public event EventHandler LoggedIn;

        public LoginPage(SmalldebtsManager serviceClient)
        {
            _serviceClient = serviceClient;
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

            try
            {
                var token = await _serviceClient.GetAuthenticationToken(LoginEmailEntry.Text, LoginPassEntry.Text);

                MobileServiceUser user = new MobileServiceUser(token.Guid);
                user.MobileServiceAuthenticationToken = token.AccessToken;
                _serviceClient.CurrentClient.CurrentUser = user;

                App.LoggedIn = true;
                LoggedIn?.Invoke(this, new EventArgs());
                await Navigation.PopModalAsync();
            }
            catch (Exception xe)
            {
                UserDialogs.Instance.ShowError(xe.Message);
            }

        }

        private async void SignUpButtonClicked(object sender, EventArgs e)
        {
            try
            {
                var newUser = await _serviceClient.RegisterUser(SignupEmailEntry.Text, SignupEmailEntry.Text,
                    SignupPassEntry.Text,
                    SignupPassConfirmationEntry.Text);
                await UserDialogs.Instance.AlertAsync($"{newUser.JoinDate}");
            }
            catch (Exception xe)
            {
                UserDialogs.Instance.ShowError(xe.Message);
            }

        }
    }
}
