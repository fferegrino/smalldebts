using Acr.UserDialogs;
using Smalldebts.Core.UI.Controls.Cells;
using Smalldebts.Core.UI.ViewModels;
using System;
using System.Collections.Generic;
using Rg.Plugins.Popup.Services;
using Xamarin.Forms;
using Microsoft.WindowsAzure.MobileServices;
using Smalldebts.Core.UI.Models;
using System.Linq;

namespace Smalldebts.Core.UI.Views
{
    public partial class HomePage : ContentPage
    {
        private ModifyDebtPage DebtModificationPage;
        public HomePage()
        {
            InitializeComponent();
			NavigationPage.SetBackButtonTitle(this, "Debts");
            DebtModificationPage = new ModifyDebtPage();
            BindingContext = this;


            if (DataAccess.Data.Debts.Count > 0)
            {
                DebtList.ItemsSource = DataAccess.Data.Debts;
                AddNewDebtOptionPanel.IsVisible = false;
            }
            else
            {
                DebtList.IsVisible = false;
                AddNewDebtOptionPanel.IsVisible = true;
                var tap = new TapGestureRecognizer();
                tap.Tapped += Tap_Tapped;
                AddNewDebtOptionPanel.GestureRecognizers.Add(tap);
            }

            DebtList.ItemSelected += DebtList_ItemSelected;

			var item = new ToolbarItem() { Text = "Add", Icon="add" };
            item.Clicked += ItemOnClicked;
            ToolbarItems.Add(item);

            MessagingCenter.Subscribe<DebtCell, DebtManipulationViewModel>(this, "update", Edit);

            MessagingCenter.Subscribe<DebtCell, DebtManipulationViewModel>(this, "deleted", Delete);

        }

		protected override async void OnAppearing()
		{
			MobileServiceClient client = new MobileServiceClient("http://192.168.7.64/smalldebts");
			var items = await client.GetTable<TodoItem>().ReadAsync();
			var listItem = items.ToList();
			base.OnAppearing();
		}

        private async void Tap_Tapped(object sender, EventArgs e)
        {
            await PopupNavigation.PushAsync(DebtModificationPage);
        }

        private async void ItemOnClicked(object sender, EventArgs eventArgs)
        {
            await PopupNavigation.PushAsync(DebtModificationPage);
        }

        async void Edit(DebtCell cell, DebtManipulationViewModel vm)
        {
            DebtModificationPage.DebtManipulation = vm;
            await  PopupNavigation.PushAsync(DebtModificationPage);
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
