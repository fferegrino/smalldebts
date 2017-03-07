using System;
using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using Smalldebts.Core.UI.Resources;

namespace Smalldebts.Core.UI.Views.PopUps
{
    public enum FilterKind
    {
        All = 0,
        IOweToTheyOweMe = 1,
        IOweTo = 2,
        TheyOweMe = 3,
        ImEven = 4
    }

    public partial class FilterSettingsPage : PopupPage
    {
        public FilterSettingsPage(FilterKind SelectedFilter = FilterKind.All)
        {
            InitializeComponent();
            FilterPicker.Items.Add(AppStrings.FilterAll);
            FilterPicker.Items.Add(AppStrings.PeopleIOweOrTheyOweMe);
            FilterPicker.Items.Add(AppStrings.FilterByPeopleIOweTo);
            FilterPicker.Items.Add(AppStrings.FilterByPeopleWhoOweMe);
            FilterPicker.Items.Add(AppStrings.FilterByPeopleImEvenWith);
            FilterPicker.SelectedIndex = (int) SelectedFilter;
			FilterPicker.SelectedIndexChanged += Handle_SelectedIndexChanged;
        }

        public event EventHandler<FilterKind> FilterChanged;

        private async void Handle_SelectedIndexChanged(object sender, EventArgs e)
        {
            var filterKind = (FilterKind) FilterPicker.SelectedIndex;
            FilterChanged?.Invoke(this, filterKind);
            await PopupNavigation.PopAsync();
        }
    }
}