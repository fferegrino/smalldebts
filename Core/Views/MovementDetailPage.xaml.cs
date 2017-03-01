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
			MovementAmountLabel.Text = String.Format(AppStrings.AmountFormat, Math.Abs(Movement.Amount));
            MovementDateLabel.Text = Movement.CreatedAt.LocalDateTime.ToString();
            MovementReasonLabel.Text = Movement.Reason;
        }
    }
}