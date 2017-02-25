using System;
using System.Collections.Generic;
using Rg.Plugins.Popup.Pages;
using Xamarin.Forms;

namespace Smalldebts.Core.UI.Views.PopUps
{

	public enum FilterKind
	{
		All = 0,
		IOweToTheyOweMe = 1,
		IOweTo =  3,
		TheyOweMe = 4,
		ImEven = 5
	}
	
	public partial class FilterSettingsPage : PopupPage
	{
		public event EventHandler<FilterKind> FilterChanged;
		public FilterSettingsPage(FilterKind SelectedFilter = FilterKind.All)
		{
			InitializeComponent();
			FilterPicker.SelectedIndex = (int)SelectedFilter;
		}

		void Handle_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			var filterKind = (FilterKind)FilterPicker.SelectedIndex;
			FilterChanged?.Invoke(this, filterKind);
		}
	}
}
