using System;
using System.Collections.Generic;
using Smalldebts.Core.UI.Views;
using Xamarin.Forms;
using Smalldebts.Core.UI.Services;

namespace Smalldebts.Core.UI
{
	public partial class App : Application
	{
		public App()
		{
			InitializeComponent();
			SetupCodedStyles();

            if (Device.OS == TargetPlatform.iOS || Device.OS == TargetPlatform.Android)
            {
                var ci = DependencyService.Get<ILocalization>().GetCurrentCultureInfo();
                //Resx.AppResources.Culture = ci; // set the RESX for resource localization
                DependencyService.Get<ILocalization>().SetLocale(ci); // set the Thread for locale-aware methods
            }

            MainPage = new NavigationPage(new HomePage());
		}

		public static App RealCurrent => App.Current as App;


		protected override void OnStart()
		{
			// Handle when your app starts
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
