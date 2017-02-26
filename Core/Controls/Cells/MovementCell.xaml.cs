using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        
        Movement Movement => BindingContext as Movement;

        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();
            if (Movement != null)
            {
                DateLabel.Text = Movement.Date.ToString();
                AmountLabel.Text = $"{Movement.Amount:0,000.00}";
                AmountLabel.TextColor = (Color)App.RealCurrent.ColorConverter.Convert(Movement.Amount, null, null, null);
            }
        }
    }
}
