using System;
using System.Net;
using System.Windows.Input;
using Xamarin.Forms;
using XamarinChallenge.Models.Response;
using XamarinChallenge.ViewModels.Base;

namespace XamarinChallenge.ViewModels
{
    public class DetailPageViewModel : BaseViewModel
    {
        #region Commands
        public ICommand GoBackCommand { get; set; }
        public ICommand WebLinkReaderCommand { get; set; }
        #endregion
        #region Binding properties
        private Item _selectedItem;
        public Item SelectedItem
        {
            get => _selectedItem;
            set
            {
                _selectedItem = value;
                NotifyPropertyChanged();
            }
        }
        private bool _webLinkVisibility;
        public bool WebLinkVisibility
        {
            get => _webLinkVisibility;
            set
            {
                _webLinkVisibility = value;
                NotifyPropertyChanged();
            }
        }
        private string _webLink;
        public string WebLink
        {
            get => _webLink;
            set
            {
                _webLink = value;
                NotifyPropertyChanged();
            }
        }
        public int ScreenWidth { get; set; }
        public CookieContainer CookieContainer { get; set; }
        public UrlWebViewSource UrlWebViewSource { get; set; }
        #endregion
        /// <summary>
        /// Main constructor that is expecting the selected item from the list
        /// </summary>
        /// <param name="selectedItem"></param>
        public DetailPageViewModel(Item selectedItem)
        {
            this.SelectedItem = selectedItem;
            this.GoBackCommand = new Command(GoBack);
            this.WebLinkReaderCommand = new Command(WebLinkReader);
            ScreenWidth = App.ScreenWidth - 20;
        }

        /// <summary>
        /// This method is fired when the link is tapped fromthe detail page and it gives the source and visibility true to the web view
        /// </summary>
        /// <param name="obj"></param>
        private void WebLinkReader(object obj)
        {
            try
            {
                WebLink = SelectedItem.VolumeInfo.PreviewLink;
                WebLinkVisibility = true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception: {ex.Message}, Stacktrace: {ex.StackTrace}");
            }
        }

        /// <summary>
        /// Pops the view
        /// </summary>
        /// <param name="obj"></param>
        private async void GoBack(object obj)
        {
            try
            {
                await Application.Current.MainPage.Navigation.PopAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception: {ex.Message}, Stacktrace: {ex.StackTrace}");
            }
        }
    }
}
