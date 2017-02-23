using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace Smalldebts.Core.UI.Views
{
	public partial class HomePage : ContentPage
	{
		public HomePage()
		{
			InitializeComponent();
			DebtList.ItemsSource = DataAccess.Data.Debts;

			DebtList.ItemSelected += DebtList_ItemSelected;
		}

		async void DebtList_ItemSelected(object sender, SelectedItemChangedEventArgs e)
		{
			if (DebtList.SelectedItem != null)
			{
				await Navigation.PushAsync(new DebtDetailPage());
				DebtList.SelectedItem = null;
			}
		}
	}
}
