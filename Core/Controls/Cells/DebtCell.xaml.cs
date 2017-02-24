using System;
using System.Diagnostics;
using System.Collections.Generic;
using Smalldebts.Core.Models;
using Xamarin.Forms;
using System.Threading.Tasks;

namespace Smalldebts.Core.UI.Controls.Cells
{
	public partial class DebtCell : ViewCell
	{
		public DebtCell()
		{
			InitializeComponent();
			var moreAction = new MenuItem { Text = "Modify", };

			var deleteAction = new MenuItem { Text = "Delete", IsDestructive = true };

			ContextActions.Add(moreAction);
			ContextActions.Add(deleteAction);
		}

		protected override void OnBindingContextChanged()
		{
			base.OnBindingContextChanged();
			var debt = BindingContext as Debt;
			if (debt != null)
			{
				NameLabel.Text = debt.Name;
				AmountLabel.Text = $"{debt.Balance:0,000.00}";
				AmountLabel.TextColor = (Color)App.RealCurrent.ColorConverter.Convert(debt.Balance, null, null, null);
			}
		}
	}
}
