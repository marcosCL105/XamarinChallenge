using System;
using System.Windows.Input;
using Xamarin.Forms;
using XamarinChallenge.ViewModels.Base;
using XamarinChallenge.Views;

namespace XamarinChallenge.ViewModels
{
    public class MainPageViewModel : BaseViewModel
    {
        #region Commands
        /// <summary>
        /// Command Search
        /// </summary>
        public ICommand SearchCommand { get; set; }
        #endregion

        #region Binding Properties
        private string _textToSearch;
        public string TextToSearch
        {
            get => _textToSearch;
            set
            {
                _textToSearch = value;
                NotifyPropertyChanged();
            }
        }
        #endregion

        /// <summary>
        /// Main constructor
        /// </summary>
        public MainPageViewModel()
        {
            this.SearchCommand = new Command(Search);
        }

        /// <summary>
        /// Method that makes the search into the Google Books API 
        /// </summary>
        /// <param name="searchText">The world that you want to search Example:"cat", "dog"</param>
        private void Search()
        {
            try
            {
                if (string.IsNullOrEmpty(TextToSearch))
                    return;

                Application.Current.MainPage.Navigation.PushAsync(new ResulPage(TextToSearch), true);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception: {ex.Message}, Stacktrace: {ex.StackTrace}");
            }
        }
    }
}
