using Acr.UserDialogs;
using Microsoft.WindowsAzure.MobileServices;
using Smalldebts.ItermediateObjects;
using System;
using System.Collections.Generic;
using System.Net.Http;
using Xamarin.Forms;

namespace Smalldebts.Core.UI.Views
{
    public partial class DebtDetailPage : ContentPage
    {
        public Debt Debt { get; internal set; }

        MobileServiceClient _serviceClient;
        public event EventHandler<Debt> DebtUpdated;

        public DebtDetailPage(MobileServiceClient serviceClient)
        {
            InitializeComponent();
            _serviceClient = serviceClient;
            DetailList.ItemSelected += DetailList_ItemSelected;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            Title = Debt.Name;
            BalanceLabel.Text = $"{Debt.Balance:#,##0.00}";
        }


        async void DetailList_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (DetailList.SelectedItem != null)
            {
                await Navigation.PushAsync(new MovementDetailPage());
                DetailList.SelectedItem = null;
            }
        }

        async void ButtonClicked(object sender, System.EventArgs e)
        {
            decimal amount;
            if (decimal.TryParse(AmountEntry.Text, out amount))
            {
                amount *= (sender == PlusButton ? 1 : -1);
            }
            else
            {
                return;
            }
            UserDialogs.Instance.ShowLoading();
            var updated = new Debt
            {
                Id = Debt.Id,
                Reason = null,
                Balance = amount
            };
            var result = await _serviceClient.InvokeApiAsync<Debt, Debt>("debts", updated, HttpMethod.Put, null);
            DebtUpdated?.Invoke(sender, result);
            AmountEntry.Text = null;
            
            BalanceLabel.Text = $"{result.Balance:#,##0.00}";

            UserDialogs.Instance.HideLoading();
        }
    }
}
