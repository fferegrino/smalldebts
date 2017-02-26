using Rg.Plugins.Popup.Pages;
using Smalldebts.Core.UI.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rg.Plugins.Popup.Services;
using Xamarin.Forms;
using Smalldebts.Core.Models;
using Acr.UserDialogs;

namespace Smalldebts.Core.UI.Views.PopUps
{
	public enum ActionPerformed
	{
		Deleted,
		Updated,
		Created,
		Nope
	}

    public partial class ModifyDebtPage : PopupPage
    {
        public ModifyDebtPage()
        {
            InitializeComponent();
        }

        public DebtManipulationViewModel DebtManipulation { get; set; }
		public event EventHandler<Debt> DebtCreated;
		public event EventHandler<Debt> DebtUpdated;

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
				return;
            }

			var id = DebtManipulation?.Id;
			decimal amount;
			if (decimal.TryParse(DebtAmountEntry.Text, out amount))
			{
				amount *= (sender == PaidButton ? -1 : 1);
			}
			else
			{
				return;
			}
			UserDialogs.Instance.ShowLoading();
			if (id != null) // modify debt
			{
				var result = await DataAccess.Data.ModifyDebt(new Core.Models.Debt
				{
					Id =DebtManipulation.Id,
					Balance = amount
				});
				DebtUpdated?.Invoke(sender, result);
			}
			else
			{
				var result = await DataAccess.Data.CreateNewDebt(new Core.Models.Debt
				{
					Name = DebtNameEntry.Text,
					Balance = amount
				});
				DebtCreated?.Invoke(sender, result);

			}
			UserDialogs.Instance.HideLoading();
			await PopupNavigation.PopAsync();
        }
    }
}
