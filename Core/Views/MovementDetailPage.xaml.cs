using System;
using System.Threading.Tasks;
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
			ReasonEntry.TextChanged += ReasonEntry_TextChanged;
			ReasonEntry.Unfocused += ReasonEntry_Unfocused;
        }

		void ReasonEntry_TextChanged(object sender, TextChangedEventArgs e)
		{
			MovementReasonLabel.Text = ReasonEntry.Text;
		}

		async void ReasonEntry_Unfocused(object sender, FocusEventArgs e)
		{
			IsBusy = true;
			await Task.Delay(1000);
			IsBusy = false;
		}

		public Movement Movement { get; set; }

        protected override void OnAppearing()
        {
			Title = String.Format(AppStrings.AmountFormat, Math.Abs(Movement.Amount));
			var time = Movement.Date.LocalDateTime;
            MovementAmountLabel.Text = String.Format(AppStrings.AmountFormat, Math.Abs(Movement.Amount));
			MovementAmountLabel.FontSize = Movement.Amount.SizeForDigit();
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
			MovementDateLabel.Text = time.ToString("MMMM yyyy\ndddd dd");
			MovementTimeLabel.Text = time.ToString("hh:mm tt");
			if (String.IsNullOrEmpty(Movement.Reason))
			{
				ReasonEntry.Placeholder = "No reason, write one here";
				ReasonEntry.Text = "";
			}
			else
			{
				ReasonEntry.Text = Movement.Reason;
				MovementReasonLabel.Text = Movement.Reason;
			}
        }
    }
}