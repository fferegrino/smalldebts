using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Acr.UserDialogs;
using Microsoft.WindowsAzure.MobileServices;
using Rg.Plugins.Popup.Services;
using Smalldebts.Core.UI.Controls.Cells;
using Smalldebts.Core.UI.ViewModels;
using Smalldebts.Core.UI.Views.PopUps;
using Smalldebts.ItermediateObjects;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Smalldebts.Core.UI.DataAccess;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace Smalldebts.Core.UI.Views
{
    public partial class HomePage : ContentPage
    {
        private readonly SmalldebtsManager _serviceClient;
        private readonly DebtDetailPage DebtDetailPage;
        private readonly ModifyDebtPage DebtModificationPage;

		private FilterKind Filter = FilterKind.IOweToTheyOweMe;
        private readonly FilterSettingsPage FilterSettingsPage;

        private List<Debt> OriginalDebts;
        private IEnumerable<Debt> ShownDebts;
        private SortingKind Sorting = SortingKind.ByAmount;
        private readonly SortingSettingsPage SortingSettingsPage;

		public bool IsBusy { get; set; }

        public HomePage()
        {
            InitializeComponent();
            NavigationPage.SetBackButtonTitle(this, "Debts");
            _serviceClient = SmalldebtsManager.DefaultManager;

            DebtModificationPage = new ModifyDebtPage(_serviceClient);
            DebtModificationPage.DebtUpdated += DebtModificationPage_DebtUpdated;
            DebtModificationPage.DebtCreated += DebtModificationPage_DebtCreated;

            DebtDetailPage = new DebtDetailPage(_serviceClient);
            DebtDetailPage.DebtUpdated += DebtModificationPage_DebtUpdated;


			FilterSettingsPage = new FilterSettingsPage(Filter);
            FilterSettingsPage.FilterChanged += FilterChanged;

            SortingSettingsPage = new SortingSettingsPage();
            SortingSettingsPage.SortingChanged += SortingChanged;

			FilterImage.Clicked += async (sender, e) => await PopupNavigation.PushAsync(FilterSettingsPage);

			SortImage.Clicked += async (sender, e) => await PopupNavigation.PushAsync(SortingSettingsPage);

			DebtList.RefreshCommand = LoadDebtsCommand; 

            DebtList.ItemSelected += DebtList_ItemSelected;

            var item = new ToolbarItem { Text = "Add", Icon = "add" };
            item.Clicked += async (s, a) => await OpenDebtModificationPage();
            ToolbarItems.Add(item);


            MessagingCenter.Subscribe<DebtCell, DebtManipulationViewModel>(this, "update",
                async (sender, debtManipulation) =>
                    await OpenDebtModificationPage(debtManipulation));

            MessagingCenter.Subscribe<DebtCell, DebtManipulationViewModel>(this, "deleted", Delete);
        }

		private Command _loadDebtsCommand;

		public Command LoadDebtsCommand
		{
			get
			{
				return _loadDebtsCommand ?? (_loadDebtsCommand = new Command(async () =>
				{

			if (IsBusy)
						return;
					IsBusy = true;
					LoadDebtsCommand.ChangeCanExecute();
					await LoadData();
					IsBusy = false;
					DebtList.IsRefreshing = false;
					LoadDebtsCommand.ChangeCanExecute();
				}, 
				                                                             () =>
			   {
				   return !IsBusy;
			   }));
			}
		}


        async Task LoadData()
        {
            var debtList = await _serviceClient.GetDebts();

            OriginalDebts = debtList;
            ShownDebts = OriginalDebts;
            if (OriginalDebts.Count > 0)
            {
                ApplyFilter();
                ApplySorting();
                AddNewDebtOptionPanel.IsVisible = false;
				DebtList.IsVisible = true;
            }
            else
            {
                DebtList.IsVisible = false;
                AddNewDebtOptionPanel.IsVisible = true;
                var tap = new TapGestureRecognizer();
                tap.Tapped += async (s, a) => await OpenDebtModificationPage();
                AddNewDebtOptionPanel.GestureRecognizers.Add(tap);
            }
			SetNewItemSource();
        }


        protected override async void OnAppearing()
        {
			var authed = await App.RealCurrent.AutoAuthenticate();
			if (!authed)
            {
                var loginPage = new LoginPage(_serviceClient);
                loginPage.LoggedIn += async (s, a) => await LoadData();
				await Navigation.PushModalAsync(loginPage);
            }
            else
            {
                await LoadData();
            }
            base.OnAppearing();
        }

        private async Task OpenDebtModificationPage(DebtManipulationViewModel debtModification = null)
        {
            DebtModificationPage.DebtManipulation = debtModification;
            await PopupNavigation.PushAsync(DebtModificationPage);
        }

        private void SortingChanged(object sender, SortingKind e)
        {
            Sorting = e;
            ApplySorting();
            SetNewItemSource();
        }

        private void ApplySorting()
        {
            switch (Sorting)
            {
                case SortingKind.ByDate:
                    ShownDebts = ShownDebts.OrderByDescending(d => d.UpdatedAt ?? d.CreatedAt);
                    break;
                case SortingKind.ByName:
                    ShownDebts = ShownDebts.OrderBy(d => d.Name);
                    break;
                case SortingKind.ByAmount:
                    ShownDebts = ShownDebts.OrderByDescending(d => d.Balance);
                    break;
            }
        }

        private void FilterChanged(object sender, FilterKind filterKind)
        {
            Filter = filterKind;
            ApplyFilter();
            SetNewItemSource();
        }

        private void ApplyFilter()
        {
            switch (Filter)
            {
                case FilterKind.ImEven:
                    ShownDebts = OriginalDebts.Where(d => d.Balance == 0);
                    break;
                case FilterKind.IOweToTheyOweMe:
                    ShownDebts = OriginalDebts.Where(d => d.Balance != 0);
                    break;
                case FilterKind.TheyOweMe:
                    ShownDebts = OriginalDebts.Where(d => d.Balance > 0);
                    break;
                case FilterKind.IOweTo:
                    ShownDebts = OriginalDebts.Where(d => d.Balance < 0);
                    break;
                default:
                    ShownDebts = OriginalDebts;
                    break;
            }
        }


        private async void Delete(DebtCell cell, DebtManipulationViewModel vm)
        {
            var result = await UserDialogs.Instance.ConfirmAsync("Seguro");
        }

        private async void DebtList_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (DebtList.SelectedItem != null)
            {
                var debt = DebtList.SelectedItem as Debt;
                DebtDetailPage.Debt = debt;
                await Navigation.PushAsync(DebtDetailPage);
                DebtList.SelectedItem = null;
            }
        }

        private void SearchTextChanged(object sender, TextChangedEventArgs e)
        {
			ShownDebts = ShownDebts.Where(debt => debt.Name.IndexOf(e.NewTextValue, 0,System.StringComparison.OrdinalIgnoreCase) >= 0);
            SetNewItemSource();
        }

        private void DebtModificationPage_DebtUpdated(object sender, Debt e)
        {
            var updated = ShownDebts.FirstOrDefault(d => d.Id == e.Id);
            updated.Balance = e.Balance;
            ApplyFilter();
            ApplySorting();
            SetNewItemSource();
        }

        private void DebtModificationPage_DebtCreated(object sender, Debt result)
        {
            OriginalDebts.Add(result);
            (DebtList.ItemsSource as ObservableCollection<Debt>).Insert(0, result);
            if (!DebtList.IsVisible)
            {
                AddNewDebtOptionPanel.IsVisible = false;
                DebtList.IsVisible = true;
            }
        }

        private void SetNewItemSource()
        {
            DebtList.ItemsSource = new ObservableCollection<Debt>(ShownDebts);
        }
    }
}