using Rg.Plugins.Popup.Pages;
using Smalldebts.Core.UI.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rg.Plugins.Popup.Services;
using Xamarin.Forms;

namespace Smalldebts.Core.UI.Views
{
    public partial class ModifyDebtPage : PopupPage
    {
        public ModifyDebtPage()
        {
            InitializeComponent();
        }

        public DebtManipulationViewModel DebtManipulation { get; set; }

        protected override void OnAppearing()
        {
            if (DebtManipulation?.Name != null)
            {
                DebtTitleLabel.Text = "Modify " + DebtManipulation.Name + "'s debt";
                DebtTitleLabel.IsVisible = true;
                DebtNameEntry.IsVisible = false;
            }
            else
            {
                DebtTitleLabel.IsVisible = false;
                DebtNameEntry.IsVisible = true;
            }
            base.OnAppearing();
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
        }

        // Method for animation child in PopupPage
        // Invoced after custom animation end
        protected virtual Task OnAppearingAnimationEnd()
        {
            return Content.FadeTo(0.5);
        }

        // Method for animation child in PopupPage
        // Invoked before custom animation begin
        protected virtual Task OnDisappearingAnimationBegin()
        {
            return Content.FadeTo(1); ;
        }

        protected override bool OnBackButtonPressed()
        {
            //Prevent hide popup
            return base.OnBackButtonPressed();
            //return true;
        }

        // Invoced when background is clicked
        protected override bool OnBackgroundClicked()
        {
            return false;
        }

        private async void Button_OnClicked(object sender, EventArgs e)
        {
            if (sender == CancelButton)
            {
                await PopupNavigation.PopAsync();
            }
        }
    }
}
