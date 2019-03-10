using System;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

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
        Playlist channelVideosPlaylist;
        Channel channel;

        #region Playlists

        public Playlist ChannelVideosPlaylist
        {
            get { return channelVideosPlaylist; }
            set { SetProperty(ref channelVideosPlaylist, value); }
        }

        #endregion

        public Channel Channel
        {
            get { return channel; }
            set { SetProperty(ref channel, value); }
        }

        async Task ExecuteSearchCommand(string searchParameter)
        {
            if (string.IsNullOrEmpty(searchParameter))
                return;

            IsBusy = true;

            try
            {
                var result = await YouTubeWebsite.GetChannelIdAsync(searchParameter);
                if (string.IsNullOrEmpty(result))
                    return;
                Channel = await YouTubeWebsite.GetChannelAsync(result);
                ChannelVideosPlaylist = await YouTubeWebsite.GetPlaylistAsync(Channel.GetChannelVideosPlaylistId(), 1);
                //ChannelVideosPlaylist.Videos.First().Tit
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
