using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using XamarinChallenge.Models.Response;
using XamarinChallenge.Services;
using XamarinChallenge.Services.Interfaces;
using XamarinChallenge.ViewModels.Base;
using XamarinChallenge.Views;

namespace XamarinChallenge.ViewModels
{
    public class ResultPageViewModel : BaseViewModel
    {
        #region Variables
        private int startIndex = 0;
        private string searchedText = string.Empty;
        #endregion
        #region Commands
        public ICommand ItemTresholdReachedCommand { get; set; }
        public ICommand GoHomeCommand { get; set; }
        public ICommand SelectionChangedCommand { get; set; }
        #endregion
        #region Binding properties
        private bool _isLoading;
        public bool IsLoading
        {
            get => _isLoading;
            set
            {
                _isLoading = value;
                NotifyPropertyChanged();
            }
        }
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
        private ObservableCollection<Item> _bookList;
        public ObservableCollection<Item> BookList
        {
            get => _bookList;
            set
            {
                _bookList = value;
                NotifyPropertyChanged();
            }
        }

        private int _remainingItemsThreshold = 1;
        public int RemainingItemsThreshold
        {
            get => _remainingItemsThreshold;
            set
            {
                _remainingItemsThreshold = value;
                NotifyPropertyChanged();
            }
        }
        private bool _isAddingMoreItems;
        public bool IsAddingMoreItems
        {
            get => _isAddingMoreItems;
            set
            {
                _isAddingMoreItems = value;
                NotifyPropertyChanged();
            }
        }
        #endregion

        /// <summary>
        /// Main constructor
        /// </summary>
        public ResultPageViewModel()
        {
            this.IsLoading = true;
            BookList = new ObservableCollection<Item>();
            this.ItemTresholdReachedCommand = new Command<string>(LoadMore);
            this.GoHomeCommand = new Command(GoHome);
            this.SelectionChangedCommand = new Command(SelectionChanged);
        }

        /// <summary>
        /// Method that is fired when the selection has changed
        /// </summary>
        private async void SelectionChanged()
        {
            try
            {
                if(this.SelectedItem != null)
                {
                    await Application.Current.MainPage.Navigation.PushAsync(new DetailPage(this.SelectedItem));
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception: {ex.Message}, Stacktrace: {ex.StackTrace}");
            }
        }

        /// <summary>
        /// Method that pops the page
        /// </summary>
        /// <param name="obj"></param>
        private async void GoHome(object obj)
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

        /// <summary>
        /// Method that is fired when the RemainingItemsThreshold is equal than the configured when the list scroll is at the end
        /// </summary>
        /// <param name="obj"></param>
        private async void LoadMore(Object obj)
        {
            try
            {
                if (this.IsLoading == false && startIndex < BookList.Count)
                {
                    IsAddingMoreItems = true;
                    startIndex = BookList.Count + 1;
                    await GetBooks(searchedText);
                    IsAddingMoreItems = false;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception: {ex.Message}, Stacktrace: {ex.StackTrace}");
            }
        }

        /// <summary>
        /// Method that makes the get information and the pagination 
        /// </summary>
        /// <param name="searchText">the word that is goin to be searched</param>
        /// <returns></returns>
        public async Task GetBooks(string searchText)
        {
            try
            {
                this.searchedText = searchText;
                IClientFactory client = new ClientFactory(null, null);
                var bookResponse = await client.GetAsync<BookResponse>($"https://www.googleapis.com/books/v1/volumes?q={searchedText}&startIndex={startIndex}&maxResults=20");
                if (bookResponse != null && bookResponse.Items != null && bookResponse.Items.Count > 0) 
                {
                    if(BookList != null && BookList.Count == 0)
                        BookList = new ObservableCollection<Item>(bookResponse.Items);
                    else
                    {
                        foreach(var item in bookResponse.Items)
                        {
                            if(!BookList.Contains(item))
                                BookList.Add(item);
                        }
                    }
                    this.IsLoading = false;
                }
                else
                {
                    this.IsLoading = false;
                    RemainingItemsThreshold = -1;
                    return;
                }
            }
            catch (Exception ex)
            {
                this.IsLoading = false;
                Console.WriteLine($"Exception: {ex.Message}, Stacktrace: {ex.StackTrace}");
            }
        }
    }
}
