using System;
using Rg.Plugins.Popup.Pages;

namespace Smalldebts.Core.UI.Views.PopUps
{
    public enum FilterKind
    {
        All = 0,
        IOweToTheyOweMe = 1,
        IOweTo = 3,
        TheyOweMe = 4,
        ImEven = 5
    }

    public partial class FilterSettingsPage : PopupPage
    {
        public FilterSettingsPage(FilterKind SelectedFilter = FilterKind.All)
        {
            InitializeComponent();
            FilterPicker.SelectedIndex = (int) SelectedFilter;
        }

        public event EventHandler<FilterKind> FilterChanged;

        private void Handle_SelectedIndexChanged(object sender, EventArgs e)
        {
            var filterKind = (FilterKind) FilterPicker.SelectedIndex;
            FilterChanged?.Invoke(this, filterKind);
        }
    }
}