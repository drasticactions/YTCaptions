using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace YTCaptions.ViewModels
{
    public class YTSearchVideosViewModel : BaseViewModel
    {
        public YTSearchVideosViewModel()
        {
            SearchCommand = new Command(async (text) => await ExecuteSearchCommand((string)text));
        }

        public Command SearchCommand { get; set; }
        public ObservableRangeCollection<YoutubeExplode.Models.Video> Items { get; set; } = new ObservableRangeCollection<YoutubeExplode.Models.Video>();
        public string Text { get; set; }

        async Task ExecuteSearchCommand(string searchParameter)
        {
            if (string.IsNullOrEmpty(searchParameter))
                return;

            IsBusy = true;
            
            try
            {
                Items.RemoveAll(n => true);
                var result = await YouTubeWebsite.SearchVideosAsync(searchParameter, 1);
                Items.AddRange(result);
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
