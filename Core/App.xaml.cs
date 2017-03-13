using System;
using System.Threading.Tasks;
using Microsoft.Azure.Mobile;
using Microsoft.Azure.Mobile.Analytics;
using Microsoft.Azure.Mobile.Crashes;
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

	    protected override void OnStart()
	    {
	        base.OnStart();
            MobileCenter.Start("android=b4e7b97f-602a-483e-be47-10c3a406dcc3;" +
                   "ios=92a01fb6-247b-4a42-9493-0776d643acb6;",
                   typeof(Analytics), typeof(Crashes));
        }
	}
}