using System;
using System.Collections.Generic;
using System.Net.Http;
using Acr.UserDialogs;
using Microsoft.WindowsAzure.MobileServices;
using Smalldebts.Core.UI.Resources;
using Smalldebts.ItermediateObjects;
using Xamarin.Forms;
using Smalldebts.Core.UI.DataAccess;
using System.Collections.ObjectModel;

namespace Smalldebts.Core.UI.Views
{
    public partial class DebtDetailPage : ContentPage
    {
        private readonly SmalldebtsManager _serviceClient;

        public DebtDetailPage(SmalldebtsManager serviceClient)
        {
            InitializeComponent();
            _serviceClient = serviceClient;
            _movementDetailPage = new MovementDetailPage(serviceClient);
            MovementDetailList.ItemSelected += DetailList_ItemSelected;
        }

        public Debt Debt { get; internal set; }
        public event EventHandler<Debt> DebtUpdated;
        private ObservableCollection<Movement> Movements;

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            UpdateAmounts();
			NavigationPage.SetBackButtonTitle(this, Debt.Name);
            var movements = await _serviceClient.GetMovementsForDebt(Debt.Id);
            Movements = new ObservableCollection<Movement>(movements);
            MovementDetailList.ItemsSource = Movements;
        }

        void UpdateAmounts()
        {
            BalanceLabel.Text = String.Format(AppStrings.AmountFormat, Math.Abs(Debt.Balance));
			BalanceLabel.FontSize = Debt.Balance.SizeForDigit();

            if (Debt.Balance < 0)
            {
                DebtBackground.BackgroundColor = App.RealCurrent.NegativeWashedColor;
                Title = AppStrings.LeDebesDetail + Debt.Name;
                BalanceLabel.TextColor = App.RealCurrent.NegativeColor;
                PlusButton.Text = AppStrings.LePague;
                MinusButton.Text = AppStrings.MePresto;
            }
            else if (Debt.Balance > 0)
            {
                DebtBackground.BackgroundColor = App.RealCurrent.PositiveWashedColor;
                Title = Debt.Name + AppStrings.TeDebeDetail;
                BalanceLabel.TextColor = App.RealCurrent.PositiveColor;
                PlusButton.Text = AppStrings.LePreste;
                MinusButton.Text = AppStrings.MePago;
            }
            else
            {
                DebtBackground.BackgroundColor = App.RealCurrent.NeutralWashedColor;
                Title = AppStrings.AManoDetail + Debt.Name;
                BalanceLabel.TextColor = App.RealCurrent.NeutralColor;
                PlusButton.Text = AppStrings.LePreste;
                MinusButton.Text = AppStrings.MePresto;
            }
        }

        private MovementDetailPage _movementDetailPage;
        private async void DetailList_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (MovementDetailList.SelectedItem != null)
            {
                _movementDetailPage.Movement = MovementDetailList.SelectedItem as Movement;
                await Navigation.PushAsync(_movementDetailPage);
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
			Movements.Insert(0, new Movement { Amount = amount, Date = Debt.UpdatedAt.Value, Reason = Debt.Reason });
            AmountEntry.Text = null;
            UpdateAmounts();

            UserDialogs.Instance.HideLoading();
        }
    }
}