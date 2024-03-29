﻿using System;

using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Smalldebts.Core.UI;
using Acr.UserDialogs;
using Xamarin.Forms;
using Smalldebts.Core.UI.Services;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.MobileServices;
using Smalldebts.Core.UI.DataAccess;

namespace Smalldebts.Droid
{
	[Activity(Label = "Smalldebts", Icon = "@drawable/ic_launcher", Theme = "@style/MyTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
	public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {

        protected override void OnCreate(Bundle bundle)
		{
			TabLayoutResource = Resource.Layout.Tabbar;
			ToolbarResource = Resource.Layout.Toolbar;

			base.OnCreate(bundle);

			global::Xamarin.Forms.Forms.Init(this, bundle);
            UserDialogs.Init(() => (Activity)Forms.Context);
            LoadApplication(new App());
		}
	}
}
