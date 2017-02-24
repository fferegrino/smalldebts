using Acr.UserDialogs;
using Smalldebts.Core.UI.Controls.Cells;
using Smalldebts.Core.UI.ViewModels;
using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace Smalldebts.Core.UI.Views
{
    public partial class HomePage : ContentPage
    {


        public HomePage()
        {
            InitializeComponent();

            BindingContext = this;
            DebtList.ItemsSource = DataAccess.Data.Debts;

            DebtList.ItemSelected += DebtList_ItemSelected;

            MessagingCenter.Subscribe<DebtCell, DebtManipulationViewModel>(this, "update", Edit);

            MessagingCenter.Subscribe<DebtCell, DebtManipulationViewModel>(this, "deleted", Delete);

        }

        async void Edit(DebtCell cell, DebtManipulationViewModel vm)
        {
            await UserDialogs.Instance.PromptAsync("Cantidad");
        }

        async void Delete(DebtCell cell, DebtManipulationViewModel vm)
        {
            var result = await UserDialogs.Instance.ConfirmAsync("Seguro");
        }

        async void DebtList_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (DebtList.SelectedItem != null)
            {
                await Navigation.PushAsync(new DebtDetailPage());
                DebtList.SelectedItem = null;
            }
        }

        public Command DoSomethingCommand { get; set; }
    }
}
