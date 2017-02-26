using System;
using System.Diagnostics;
using System.Collections.Generic;
using Smalldebts.ItermediateObjects;
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
            var moreAction = new MenuItem { Text = "Modify", Icon ="add" };
            moreAction.Clicked += (s, a) =>
            {
                var argument = new DebtManipulationViewModel();
                argument.Id = Debt?.Id;
                argument.Name = Debt?.Name;
                argument.Amount = -20;
                MessagingCenter.Send<DebtCell, DebtManipulationViewModel>(this, "update", argument);
            };


			var deleteAction = new MenuItem { Text = "Delete", IsDestructive = true, Icon="substract" };
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

        private static Double FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Label)) * 0.9;
        private static Span YouOweToSpan = new Span() { Text = "You owe to ", FontSize = FontSize };
        private static Span TheyOweYouSpan = new Span() { Text = " owes you", FontSize = FontSize };
        private static Span YouAreEvenWithSpan = new Span() { Text = "You are even with ", FontSize = FontSize };

        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();
            if (Debt != null)
            {
                var fs = new FormattedString();
                var nameSpan = new Span { Text = Debt.Name, FontSize = FontSize, FontAttributes = FontAttributes.Bold };
                var moneySpan = new Span { Text = $" {Math.Abs(Debt.Balance):#,##0.##} ", FontSize = FontSize };
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
    }
}
