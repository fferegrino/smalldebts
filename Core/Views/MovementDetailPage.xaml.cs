using System;
using Smalldebts.Core.UI.Resources;
using Smalldebts.ItermediateObjects;
using Xamarin.Forms;

namespace Smalldebts.Core.UI.Views
{
    public partial class MovementDetailPage : ContentPage
    {
        public MovementDetailPage()
        {
            InitializeComponent();
        }

        public Movement Movement { get; set; }

        protected override void OnAppearing()
        {
			Title = String.Format(AppStrings.AmountFormat, Movement.Amount);
            MovementAmountLabel.Text = String.Format(AppStrings.AmountFormat, Movement.Amount);
            if (Movement.Amount < 0)
            {
                BackgroundColor = App.RealCurrent.NegativeWashedColor;
				MovementAmountLabel.TextColor = App.RealCurrent.NegativeStrongColor;
            }
            else if (Movement.Amount > 0)
            {
                BackgroundColor = App.RealCurrent.PositiveWashedColor;
				MovementAmountLabel.TextColor = App.RealCurrent.PositiveStrongColor;
            }
            else
            {
                BackgroundColor = App.RealCurrent.NeutralWashedColor;
                MovementAmountLabel.TextColor = App.RealCurrent.NeutralColor;
            }
            MovementDateLabel.Text = Movement.CreatedAt.LocalDateTime.ToString();
			MovementReasonLabel.Text = Movement.Reason;

        }
    }
}