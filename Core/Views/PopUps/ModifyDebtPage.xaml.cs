using System;
using System.Net.Http;
using System.Threading.Tasks;
using Acr.UserDialogs;
using Microsoft.WindowsAzure.MobileServices;
using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using Smalldebts.Core.UI.Resources;
using Smalldebts.Core.UI.ViewModels;
using Smalldebts.ItermediateObjects;
using Xamarin.Forms;
using Smalldebts.Core.UI.DataAccess;

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
        private readonly SmalldebtsManager _serviceClient;

        public ModifyDebtPage(SmalldebtsManager serviceClient)
        {
            _serviceClient = serviceClient;
            InitializeComponent();
        }

        public DebtManipulationViewModel DebtManipulation { get; set; }
        public event EventHandler<Debt> DebtCreated;
        public event EventHandler<Debt> DebtUpdated;

        protected override void OnAppearing()
        {
            

            if (DebtManipulation?.Name != null) // Modification
            {
                if (DebtManipulation.Amount < 0)
                {
                    PlusButton.Text = AppStrings.LePague;
                    MinusButton.Text = AppStrings.MePresto;
                }
                else if (DebtManipulation.Amount > 0)
                {

                    PlusButton.Text = AppStrings.LePreste;
                    MinusButton.Text = AppStrings.MePago;
                }
                else
                {
                    PlusButton.Text = AppStrings.LePreste;
                    MinusButton.Text = AppStrings.MePresto;
                }
                DebtTitleLabel.Text = String.Format(AppStrings.ModificaLaDeudaDe, DebtManipulation.Name);
                DebtTitleLabel.IsVisible = true;
                DebtNameEntry.IsVisible = false;
            }
            else // Creation
            {
                PlusButton.Text = AppStrings.LePreste;
                MinusButton.Text = AppStrings.MePresto;
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
            return Content.FadeTo(1);
            ;
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
                amount *= sender == PlusButton ? 1 : -1;
            else
                return;
            UserDialogs.Instance.ShowLoading(AppStrings.Wait);
            if (id != null) // modify debt
            {
                var updated = new Debt
                {
                    Id = DebtManipulation.Id,
                    Reason = DebtReasonEntry.Text,
                    Balance = amount
                };
                var result = await _serviceClient.AddMovementToDebt(updated);
                DebtUpdated?.Invoke(sender, result);
            }
            else
            {
                var created = new Debt
                {
                    Name = DebtNameEntry.Text,
                    Reason = DebtReasonEntry.Text,
                    Balance = amount
                };

                var result = await _serviceClient.AddNewDebt(created);
                DebtCreated?.Invoke(sender, result);
            }
            DebtReasonEntry.Text = "";
            DebtNameEntry.Text = "";
            DebtAmountEntry.Text = "";
            UserDialogs.Instance.HideLoading();
            await PopupNavigation.PopAsync();
        }
    }
}