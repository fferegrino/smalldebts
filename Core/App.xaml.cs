using System;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.MobileServices;
using Smalldebts.Core.UI.DataAccess;
using Smalldebts.Core.UI.Services;
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
			SetupLanguage();

			var mainPage = new HomePage();
			var navMainPage = new NavigationPage(mainPage);

			navMainPage.BarBackgroundColor = BrandLightColor;
			navMainPage.BarTextColor = BrandColor;

			MainPage = navMainPage;
		}

		public static App RealCurrent => Current as App;
        
	}
}