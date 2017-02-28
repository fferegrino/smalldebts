using System;
using System.Collections.Generic;
using System.Net.Http;
using Acr.UserDialogs;
using Microsoft.WindowsAzure.MobileServices;
using Smalldebts.Core.UI.Resources;
using Smalldebts.ItermediateObjects;
using Xamarin.Forms;
using Smalldebts.Core.UI.DataAccess;

namespace Smalldebts.Core.UI.Views
{
    public partial class DebtDetailPage : ContentPage
    {
        private readonly SmalldebtsManager _serviceClient;

        public DebtDetailPage(SmalldebtsManager serviceClient)
        {
            InitializeComponent();
            _serviceClient = serviceClient;
            MovementDetailList.ItemSelected += DetailList_ItemSelected;
        }

        public Debt Debt { get; internal set; }
        public event EventHandler<Debt> DebtUpdated;

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            Title = Debt.Name;
            UpdateAmounts();

            var movements = await _serviceClient.GetMovementsForDebt(Debt.Id);
            MovementDetailList.ItemsSource = movements;
        }

        void UpdateAmounts()
        {
            BalanceLabel.Text = $"{Math.Abs(Debt.Balance):#,##0.00}";

            if (Debt.Balance < 0)
            {

                BalanceLabel.TextColor = App.RealCurrent.NegativeColor;
                BalanceDescriptionLabel.Text = AppStrings.LeDebesDetail;
                PlusButton.Text = AppStrings.LePague;
                MinusButton.Text = AppStrings.MePresto;
            }
            else if (Debt.Balance > 0)
            {

                BalanceLabel.TextColor = App.RealCurrent.PositiveColor;
                BalanceDescriptionLabel.Text = AppStrings.TeDebeDetail;
                PlusButton.Text = AppStrings.LePreste;
                MinusButton.Text = AppStrings.MePago;
            }
            else
            {
                BalanceLabel.TextColor = App.RealCurrent.NeutralColor;
                BalanceDescriptionLabel.Text = AppStrings.AManoDetail;
                PlusButton.Text = AppStrings.LePreste;
                MinusButton.Text = AppStrings.MePresto;
            }
        }

        private MovementDetailPage m = new MovementDetailPage();
        private async void DetailList_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (MovementDetailList.SelectedItem != null)
            {
                m.Movement = MovementDetailList.SelectedItem as Movement;
                await Navigation.PushAsync(m);
                MovementDetailList.SelectedItem = null;
            }
        }

        private async void ButtonClicked(object sender, EventArgs e)
        {
            decimal amount;
            if (decimal.TryParse(AmountEntry.Text, out amount))
                amount *= sender == PlusButton ? 1 : -1;
            else
                return;
            UserDialogs.Instance.ShowLoading();
            var updated = new Debt
            {
                Id = Debt.Id,
                Reason = null,
                Balance = amount
            };
            Debt = await _serviceClient.AddMovementToDebt(updated);
            DebtUpdated?.Invoke(sender, Debt);
            AmountEntry.Text = null;
            UpdateAmounts();

            UserDialogs.Instance.HideLoading();
        }
    }
}