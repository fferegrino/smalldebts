using System;
using System.Threading.Tasks;
using Smalldebts.Core.UI.Resources;
using Smalldebts.ItermediateObjects;
using Xamarin.Forms;
using Smalldebts.Core.UI.DataAccess;

namespace Smalldebts.Core.UI.Views
{
    public partial class MovementDetailPage : ContentPage
    {
        private readonly SmalldebtsManager _serviceClient;
        public MovementDetailPage(SmalldebtsManager serviceClient)
        {
            InitializeComponent();
            _serviceClient = serviceClient;
        }

        public Movement Movement { get; set; }

        protected override void OnAppearing()
        {
            Title = String.Format(AppStrings.AmountFormat, Math.Abs(Movement.Amount));
            var time = Movement.Date.LocalDateTime;
            MovementAmountLabel.Text = String.Format(AppStrings.AmountFormat, Math.Abs(Movement.Amount));
            MovementAmountLabel.FontSize = Movement.Amount.SizeForDigit();

            Color color;
            Color washedColor;
            Color strongColor;

            if (Movement.Amount < 0)
            {
                color = App.RealCurrent.NegativeColor;
                washedColor = App.RealCurrent.NegativeWashedColor;
                strongColor = App.RealCurrent.NegativeStrongColor;
            }
            else if (Movement.Amount > 0)
            {
                color = App.RealCurrent.PositiveColor;
                washedColor = App.RealCurrent.PositiveWashedColor;
                strongColor = App.RealCurrent.PositiveStrongColor;
            }
            else
            {
                color = App.RealCurrent.NeutralColor;
                washedColor = App.RealCurrent.NeutralWashedColor;
                strongColor = App.RealCurrent.NeutralColor;
            }

            UpdateButton.BackgroundColor = color;
            BackgroundColor = washedColor;
            ReasonLabel.TextColor = strongColor;
            MovementAmountLabel.TextColor = strongColor;

            MovementDateLabel.Text = time.ToString("MMMM yyyy\ndddd dd");
            MovementTimeLabel.Text = time.ToString("hh:mm tt");
            if (String.IsNullOrEmpty(Movement.Reason))
            {
                ReasonEntry.Placeholder = AppStrings.WriteAReasonHere;
                ReasonEntry.Text = "";
            }
            else
            {
                ReasonEntry.Text = Movement.Reason;
            }
        }

        private async void SaveClicked(object sender, EventArgs e)
        {
            var updatedMovement = new ItermediateObjects.Movement
            {
                DebtId = Movement.DebtId,
                Id = Movement.Id,
                Reason = ReasonEntry.Text
            };
            await _serviceClient.UpdateMovement(updatedMovement);
        }
    }
}