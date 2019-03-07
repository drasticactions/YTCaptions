using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Linq;

using Xamarin.Forms;

using Google.Apis.Services;
using Google.Apis.YouTube.v3;
using Google.Apis.YouTube.v3.Data;

using Newtonsoft.Json;

namespace YTCaptions.ViewModels
{
    public class YTSearchViewModel : BaseViewModel
    {
        public YTSearchViewModel()
        {
            SearchCommand = new Command(async (text) => await ExecuteSearchCommand((string)text));
        }

        public Command SearchCommand { get; set; }
        public ObservableRangeCollection<SearchResult> Items { get; set; } = new ObservableRangeCollection<SearchResult>();
        public string Text { get; set; }

        YouTubeService youtubeService = new YouTubeService(new BaseClientService.Initializer()
        {
            ApiKey = "AIzaSyAxPNzkE__p8jSJN9QzV5BJUYVqad18YzU",
            ApplicationName = "Caption"
        });

        async Task ExecuteSearchCommand(string searchParameter)
        {
            if (string.IsNullOrEmpty(searchParameter))
                return;

            IsBusy = true;

            try
            {
                Items.RemoveAll(n => true);
                var searchListRequest = youtubeService.Search.List("snippet");
                searchListRequest.Q = searchParameter;
                searchListRequest.MaxResults = 50;
                var result = await searchListRequest.ExecuteAsync();
                Items.AddRange(result.Items);
                //Items.First().Thumbnails.Default__.Width
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}
