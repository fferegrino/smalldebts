﻿using Smalldebts.Core.UI.Services;
using Smalldebts.Core.UI.Views;
using Xamarin.Forms;

namespace Smalldebts.Core.UI
{
    public partial class App : Application
    {
        public static IAuthenticate Authenticator { get; private set; }

        public static void Init(IAuthenticate authenticator)
        {
            Authenticator = authenticator;
        }

        public App()
        {
            InitializeComponent();
            SetupCodedStyles();
            SetupLanguage();
			MainPage = new NavigationPage(new LoginPage());
        }

        public static App RealCurrent => Current as App;



        protected override async void OnStart()
        {
            // Handle when your app starts
            //var authed = await Authenticator.Authenticate();
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}