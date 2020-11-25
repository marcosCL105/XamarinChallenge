using System;
using System.Collections.Generic;

using Xamarin.Forms;
using XamarinChallenge.ViewModels;

namespace XamarinChallenge.Views
{
    public partial class ResulPage : ContentPage
    {
        private string searchText;
        private ResultPageViewModel instance;
        public ResulPage(string searchText)
        {
            InitializeComponent();
            this.Title = searchText;
            this.searchText = searchText;
            instance = new ResultPageViewModel(); ;
            this.BindingContext = instance;
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            if (instance != null && instance.BookList?.Count == 0)
                await instance.GetBooks(this.searchText);
            else
                instance.SelectedItem = null;
        }
    }
}
