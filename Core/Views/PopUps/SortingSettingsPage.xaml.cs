using System;
using System.Collections.Generic;
using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using Xamarin.Forms;

namespace Smalldebts.Core.UI.Views.PopUps
{

	public enum SortingKind
	{
		ByDate = 0,
		ByAmount  = 1,
		ByName = 3
	}

	public partial class SortingSettingsPage : PopupPage
	{
		public event EventHandler<SortingKind> SortingChanged;

		public SortingSettingsPage()
		{
			InitializeComponent();
		}


		async void SortingClicked(object sender, System.EventArgs e)
		{
			SortingKind sortingKind;
			if (sender == SortByDate)
				sortingKind = SortingKind.ByDate;
			else if (sender == SortByName)
				sortingKind = SortingKind.ByName;
			else
				sortingKind = SortingKind.ByAmount;

			SortingChanged?.Invoke(this, sortingKind);
			await PopupNavigation.PopAsync();
		}
	}
}
