using System;
using Smalldebts.Core.UI.Resources;
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
					MovementReasonLabel.Text = $"{Movement.Date.LocalDateTime:dddd dd, MMMM yyyy}";
                }
                else
                {
					MovementReasonLabel.Text = $"{Movement.Date.LocalDateTime:dddd dd, MMMM yyyy}: {Movement.Reason}";
                }
				AmountLabel.Text =String.Format(AppStrings.AmountFormat, Math.Abs(Movement.Amount));
                AmountLabel.TextColor =
                    (Color) App.RealCurrent.ColorConverter.Convert(Movement.Amount, null, null, null);
            }
        }
    }
}