using System;
using System.Collections.Generic;

using Xamarin.Forms;
using XamarinChallenge.ViewModels;

namespace XamarinChallenge.Views
{
    public partial class DetailPage : ContentPage
    {
        public DetailPage(Models.Response.Item selectedItem)
        {
            InitializeComponent();
            this.Title = selectedItem.VolumeInfo.Title;
            this.BindingContext = new DetailPageViewModel(selectedItem);
        }
    }
}
