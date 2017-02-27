using System;
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
            MovementAmountLabel.Text = $"{Math.Abs(Movement.Amount):0,000.00}";
            MovementDateLabel.Text = Movement.CreatedAt.LocalDateTime.ToString();
            MovementReasonLabel.Text = Movement.Reason;
        }
    }
}