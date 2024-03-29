﻿using System;
using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;

namespace Smalldebts.Core.UI.Views.PopUps
{
    public enum SortingKind
    {
        ByDate = 0,
        ByAmount = 1,
        ByName = 3
    }

    public partial class SortingSettingsPage : PopupPage
    {
        public SortingSettingsPage()
        {
            InitializeComponent();
        }

        public event EventHandler<SortingKind> SortingChanged;


        private async void SortingClicked(object sender, EventArgs e)
        {
            SortingKind sortingKind;
            if (sender == SortByDate)
                sortingKind = SortingKind.ByDate;
            else if (sender == SortByName)
                sortingKind = SortingKind.ByName;
            else
                sortingKind = SortingKind.ByAmount;

            SortingChanged?.Invoke(this, sortingKind);
            await PopupNavigation.PopAsync();
        }
    }
}