using System;
using Smalldebts.ItermediateObjects;
using Xamarin.Forms;

namespace Smalldebts.Core.UI.Controls.Cells
{
    public partial class MovementCell : ViewCell
    {
        public MovementCell()
        {
            InitializeComponent();
        }

        private Movement Movement => BindingContext as Movement;

        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();
            if (Movement != null)
            {
                if (String.IsNullOrWhiteSpace(Movement.Reason))
                {
                    MovementReasonLabel.IsVisible = false;
                }
                else
                {
                    MovementReasonLabel.IsVisible = true;
                    MovementReasonLabel.Text = Movement.Reason;
                }

                DateLabel.Text = Movement.Date.LocalDateTime.ToString();
                AmountLabel.Text = $"{Math.Abs(Movement.Amount):#,##0.00}";
                AmountLabel.TextColor =
                    (Color) App.RealCurrent.ColorConverter.Convert(Movement.Amount, null, null, null);
            }
        }
    }
}