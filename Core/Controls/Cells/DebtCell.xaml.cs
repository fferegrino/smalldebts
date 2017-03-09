using System;
using Smalldebts.Core.UI.Resources;
using Smalldebts.Core.UI.ViewModels;
using Smalldebts.ItermediateObjects;
using Xamarin.Forms;

namespace Smalldebts.Core.UI.Controls.Cells
{
    public partial class DebtCell : ViewCell
    {
		private static readonly double FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label)) * 0.9;
		private static readonly double FontSizeBig = Device.GetNamedSize(NamedSize.Large, typeof(Label)) * 0.9;
        private static readonly Span YouOweToSpan = new Span { Text = AppStrings.LeDebes, FontSize = FontSize };
        private static readonly Span TheyOweYouSpan = new Span { Text = AppStrings.TeDebe, FontSize = FontSize };
        private static readonly Span YouAreEvenWithSpan = new Span { Text = AppStrings.AMano, FontSize = FontSize };

        public DebtCell()
        {
            InitializeComponent();

            var deleteAction = new MenuItem { Text = AppStrings.PayDebt, IsDestructive = true, Icon = "substract" };
            deleteAction.Clicked += (s, a) =>
            {
                var argument = new DebtManipulationViewModel 
				{ 
					Id = Debt?.Id,
                	Name = Debt?.Name,
					Amount = Debt.Balance
				};
                MessagingCenter.Send(this, "deleted", argument);
            };

            ContextActions.Add(deleteAction);
        }

        private Debt Debt => BindingContext as Debt;

        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();
            if (Debt != null)
            {
                var fs = new FormattedString();
                var nameSpan = new Span { Text = Debt.Name, FontSize = FontSizeBig, FontAttributes = FontAttributes.Bold };
				var moneySpan = new Span { Text = String.Format(" " + AppStrings.AmountFormat, Math.Abs(Debt.Balance)), FontSize = FontSizeBig };
                if (Debt.Balance < 0) // you owe money
                {
                    moneySpan.ForegroundColor = App.RealCurrent.NegativeColor;
                    fs.Spans.Add(YouOweToSpan);
                    fs.Spans.Add(nameSpan);
                    fs.Spans.Add(moneySpan);
                }
                else if (Debt.Balance > 0) // Someone owes money to you
                {
                    moneySpan.ForegroundColor = App.RealCurrent.PositiveColor;
                    fs.Spans.Add(nameSpan);
                    fs.Spans.Add(TheyOweYouSpan);
                    fs.Spans.Add(moneySpan);
                }
                else // even
                {
                    fs.Spans.Add(YouAreEvenWithSpan);
                    fs.Spans.Add(nameSpan);
                }
                Explanation.FormattedText = fs;
            }
        }

        private void EditDebtClicked(object sender, EventArgs e)
        {
            var argument = new DebtManipulationViewModel
            {
                Id = Debt?.Id,
                Name = Debt?.Name,
                Amount = Debt.Balance
            };
            MessagingCenter.Send(this, "update", argument);
        }
    }
}