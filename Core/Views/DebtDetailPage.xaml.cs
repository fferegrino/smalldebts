using System;
using System.Net.Http;
using Acr.UserDialogs;
using Microsoft.WindowsAzure.MobileServices;
using Smalldebts.Core.UI.Resources;
using Smalldebts.ItermediateObjects;
using Xamarin.Forms;

namespace Smalldebts.Core.UI.Views
{
    public partial class DebtDetailPage : ContentPage
    {
        private readonly MobileServiceClient _serviceClient;

        public DebtDetailPage(MobileServiceClient serviceClient)
        {
            InitializeComponent();
            _serviceClient = serviceClient;
            DetailList.ItemSelected += DetailList_ItemSelected;
        }

        public Debt Debt { get; internal set; }
        public event EventHandler<Debt> DebtUpdated;

        protected override void OnAppearing()
        {
            base.OnAppearing();
            Title = Debt.Name;
            UpdateAmounts();
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


        private async void DetailList_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (DetailList.SelectedItem != null)
            {
                await Navigation.PushAsync(new MovementDetailPage());
                DetailList.SelectedItem = null;
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
            Debt = await _serviceClient.InvokeApiAsync<Debt, Debt>("debts", updated, HttpMethod.Put, null);
            DebtUpdated?.Invoke(sender, Debt);
            AmountEntry.Text = null;
            UpdateAmounts();

            UserDialogs.Instance.HideLoading();
        }
    }
}