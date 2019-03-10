using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using MoreLinq;
using Xamarin.Forms;
using YoutubeExplode.Models;

namespace YTCaptions.ViewModels
{
    public class YTSearchChannelsViewModel : BaseViewModel
    {
        public YTSearchChannelsViewModel()
        {
            SearchCommand = new Command(async (text) => await ExecuteSearchCommand((string)text));
        }

        public Command SearchCommand { get; set; }
        public string Text { get; set; }
        public ObservableRangeCollection<Channel> Items { get; set; } = new ObservableRangeCollection<Channel>();

        async Task ExecuteSearchCommand(string searchParameter)
        {
            if (string.IsNullOrEmpty(searchParameter))
                return;

            IsBusy = true;

            try
            {
                Items.RemoveAll(n => true);
                var results = await YouTubeWebsite.SearchVideosAsync(searchParameter, 1);
                var distinctResults = results.DistinctBy(n => n.Author);
                foreach(var vid in distinctResults)
                {
                    var channel = await YouTubeWebsite.GetVideoAuthorChannelAsync(vid.Id);
                    Items.Add(channel);
                }
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
