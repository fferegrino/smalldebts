using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace Smalldebts.Core.UI.Views
{
	public partial class DebtDetailPage : ContentPage
	{
		public DebtDetailPage()
		{
			InitializeComponent();
			var debt = DataAccess.Data.Detail;

			Title = debt.Name;
			BalanceLabel.Text = $"{debt.Balance:0,000.00}";
			//DetailList.ItemsSource = debt;
			DetailList.ItemSelected += DetailList_ItemSelected;
		}


		async void DetailList_ItemSelected(object sender, SelectedItemChangedEventArgs e)
		{
			if (DetailList.SelectedItem != null)
			{
				await Navigation.PushAsync(new MovementDetailPage());
				DetailList.SelectedItem = null;
			}
		}

		void ButtonClicked(object sender, System.EventArgs e)
		{
			AmountEntry.Text = "";
		}
	}
}
