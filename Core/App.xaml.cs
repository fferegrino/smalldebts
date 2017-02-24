using System;
using System.Collections.Generic;
using Smalldebts.Core.UI.Views;
using Xamarin.Forms;

namespace Smalldebts.Core.UI
{
	public partial class App : Application
	{
		public App()
		{
			InitializeComponent();
			SetupCodedStyles();

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
