using System;
using System.Diagnostics;
using System.Collections.Generic;
using Smalldebts.Core.Models;
using Xamarin.Forms;
using System.Threading.Tasks;
using Smalldebts.Core.UI.ViewModels;

namespace Smalldebts.Core.UI.Controls.Cells
{
	public partial class DebtCell : ViewCell
	{
		public DebtCell()
		{
			InitializeComponent();
			var moreAction = new MenuItem { Text = "Modify" };
            moreAction.Clicked += (s, a) =>
            {
                var argument = new DebtManipulationViewModel();
                argument.Id = Debt?.Id;
                argument.Amount = -20;
                MessagingCenter.Send<DebtCell, DebtManipulationViewModel>(this, "update", argument);
            };


            var deleteAction = new MenuItem { Text = "Delete", IsDestructive = true };
            deleteAction.Clicked += (s, a) =>
            {
                var argument = new DebtManipulationViewModel();
                argument.Id = Debt?.Id;
                MessagingCenter.Send<DebtCell, DebtManipulationViewModel>(this, "deleted", argument);
            };


            ContextActions.Add(moreAction);
			ContextActions.Add(deleteAction);
		}

        Debt Debt => BindingContext as Debt;

        protected override void OnBindingContextChanged()
		{
			base.OnBindingContextChanged();
			if (Debt != null)
			{
				NameLabel.Text = Debt.Name;
				AmountLabel.Text = $"{Debt.Balance:0,000.00}";
				AmountLabel.TextColor = (Color)App.RealCurrent.ColorConverter.Convert(Debt.Balance, null, null, null);
			}
		}
	}
}
