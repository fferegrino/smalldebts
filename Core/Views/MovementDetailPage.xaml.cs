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

            Title = Movement.Amount.ToString(AppStrings.AmountFormat);
            MovementAmountLabel.Text = Movement.Amount.ToString(AppStrings.AmountFormat);
            if (Movement.Amount < 0)
            {
                BackgroundColor = App.RealCurrent.NegativeWashedColor;
                MovementAmountLabel.TextColor = App.RealCurrent.NegativeColor;
            }
            else if (Movement.Amount > 0)
            {
                BackgroundColor = App.RealCurrent.PositiveWashedColor;
                MovementAmountLabel.TextColor = App.RealCurrent.PositiveColor;
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