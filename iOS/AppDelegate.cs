using System;
using System.Collections.Generic;
using System.Linq;

using Foundation;
using Microsoft.WindowsAzure.MobileServices;
using Smalldebts.Core.UI;
using UIKit;
using System.Threading.Tasks;
using Smalldebts.Core.UI.Services;
using Smalldebts.Core.UI.DataAccess;
using Smalldebts.iOS.Services;

namespace Smalldebts.iOS
{
	[Register("AppDelegate")]
	public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
	{

        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
		{
			System.Diagnostics.Debug.WriteLine("Finished launching");
			global::Xamarin.Forms.Forms.Init();
			CurrentPlatform.Init();
			LoadApplication(new App());
			UIApplication.SharedApplication.SetStatusBarStyle(UIStatusBarStyle.BlackOpaque, false);

			return base.FinishedLaunching(app, options);
		}

	}
}
