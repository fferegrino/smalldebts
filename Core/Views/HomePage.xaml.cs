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
using System.Collections.ObjectModel;
using Smalldebts.Core.Models;
using Smalldebts.Core.UI.Views.PopUps;
using System.Threading.Tasks;

namespace Smalldebts.Core.UI.Views
{
    public partial class HomePage : ContentPage
    {
		private ModifyDebtPage DebtModificationPage;
		private FilterSettingsPage FilterSettingsPage;
		private SortingSettingsPage SortingSettingsPage;

		ObservableCollection<Debt> OriginalDebts;
		ObservableCollection<Debt> ShownDebts;

        public HomePage()
        {
            InitializeComponent();
			NavigationPage.SetBackButtonTitle(this, "Debts");

			DebtModificationPage = new ModifyDebtPage();

			FilterSettingsPage = new FilterSettingsPage();
			FilterSettingsPage.FilterChanged += FilterChanged;

			SortingSettingsPage = new SortingSettingsPage();
			SortingSettingsPage.SortingChanged += SortingChanged;

            var tapFilter  = new TapGestureRecognizer();
			tapFilter.Tapped += async (sender, e) => await PopupNavigation.PushAsync(FilterSettingsPage);
			FilterImage.GestureRecognizers.Add(tapFilter);

			var tapSort = new TapGestureRecognizer();
			tapSort.Tapped += async (sender, e) => await PopupNavigation.PushAsync(SortingSettingsPage);
			SortImage.GestureRecognizers.Add(tapSort);




            DebtList.ItemSelected += DebtList_ItemSelected;

			var item = new ToolbarItem() { Text = "Add", Icon="add" };
			item.Clicked += async (s,a) => await OpenDebtModificationPage();
            ToolbarItems.Add(item);


			MessagingCenter.Subscribe<DebtCell, DebtManipulationViewModel>(this, "update", async (sender, debtManipulation) => 
			                                                               await OpenDebtModificationPage(debtManipulation));

			MessagingCenter.Subscribe<DebtCell, DebtManipulationViewModel>(this, "deleted", Delete);

        }

		protected override void OnAppearing()
		{

			OriginalDebts = new ObservableCollection<Debt>(DataAccess.Data.Debts);
			ShownDebts = new ObservableCollection<Debt>(DataAccess.Data.Debts);
			if (OriginalDebts.Count > 0)
			{
				DebtList.ItemsSource = ShownDebts;
				AddNewDebtOptionPanel.IsVisible = false;
			}
			else
			{
				DebtList.IsVisible = false;
				AddNewDebtOptionPanel.IsVisible = true;
				var tap = new TapGestureRecognizer();
				tap.Tapped += async (s, a) => await OpenDebtModificationPage();
				AddNewDebtOptionPanel.GestureRecognizers.Add(tap);
			}

			base.OnAppearing();
		}

		async Task OpenDebtModificationPage(DebtManipulationViewModel debtModification = null)
		{
			DebtModificationPage.DebtManipulation = debtModification;
			await PopupNavigation.PushAsync(DebtModificationPage);
		}

		void SortingChanged(object sender, PopUps.SortingKind e)
		{
			
			switch (e)
			{
				case SortingKind.ByDate:
					DebtList.ItemsSource = ShownDebts.OrderBy(d => d.Id);
					break;
				case SortingKind.ByName:
					DebtList.ItemsSource = ShownDebts.OrderBy(d => d.Name);
					break;
				case SortingKind.ByAmount:
					DebtList.ItemsSource = ShownDebts.OrderBy(d => d.Balance);
					break;
			}
		}

		void FilterChanged(object sender, PopUps.FilterKind filterKind)
		{
			switch (filterKind)
			{
				case FilterKind.ImEven:
					DebtList.ItemsSource = ShownDebts.Where(d => d.Balance == 0);
					break;
				case FilterKind.IOweToTheyOweMe:
					DebtList.ItemsSource = ShownDebts.Where(d => d.Balance != 0);
					break;
				case FilterKind.TheyOweMe:
					DebtList.ItemsSource = ShownDebts.Where(d => d.Balance > 0);
					break;
				case FilterKind.IOweTo:
					DebtList.ItemsSource = ShownDebts.Where(d => d.Balance > 0);
					break;
				default:
					DebtList.ItemsSource = ShownDebts;
					break;
			}
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

		void SearchTextChanged(object sender, Xamarin.Forms.TextChangedEventArgs e)
		{
			ShownDebts = new ObservableCollection<Debt>( OriginalDebts.Where(debt => debt.Name.Contains(e.NewTextValue)));
			DebtList.ItemsSource = ShownDebts;
		}

    }
}
