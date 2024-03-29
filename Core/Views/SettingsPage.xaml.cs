﻿using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace Smalldebts.Core.UI.Views
{
	public partial class SettingsPage : ContentPage
	{
		public SettingsPage()
		{
			InitializeComponent();
		}

		public event EventHandler LoggedOut;

		void SettingCellTapped(object sender, System.EventArgs e)
		{
			if (sender == DisconnectCell)
			{
				LoggedOut?.Invoke(this, new EventArgs());
				Navigation.PopAsync();
			}
			else if (sender == ThanksCell)
			{
				Device.OpenUri(new Uri(Constants.ApplicationUrl + "/home/thanks"));
			}
			else if (sender == ReportBugCell)
			{
				Device.OpenUri(new Uri("http://messier16.com/support.html"));
			}
		}
	}
}
