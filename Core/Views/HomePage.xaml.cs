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

            MessagingCenter.Subscribe<DebtCell, DebtManipulationViewModel>(this, "update", (sender, arg) =>
            {
                System.Diagnostics.Debug.WriteLine(arg.Id);
            });

            MessagingCenter.Subscribe<DebtCell, DebtManipulationViewModel>(this, "deleted", (sender, arg) =>
            {
                System.Diagnostics.Debug.WriteLine(arg.Id);
            });

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
