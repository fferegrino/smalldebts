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
			DetailList.ItemsSource = debt.Movements;
		}


		void A()
		{
			
		}
	}
}
