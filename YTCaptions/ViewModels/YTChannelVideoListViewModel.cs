using System;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

using Xamarin.Forms;
using YoutubeExplode.Models;

namespace YTCaptions.ViewModels
{
    public class YTChannelVideoListViewModel : BaseViewModel
    {
        public YTChannelVideoListViewModel(Channel channel)
        {
            _channel = channel;
        }

        Playlist _channelVideosPlaylist;
        Channel _channel;

        public Channel Channel
        {
            get { return _channel; }
            set { SetProperty(ref _channel, value); }
        }

        public Playlist ChannelVideosPlaylist
        {
            get { return _channelVideosPlaylist; }
            set { SetProperty(ref _channelVideosPlaylist, value); }
        }

        public async Task ExecuteLoadVideoList(bool refresh = false)
        {
            IsBusy = true;

            try
            {
                ChannelVideosPlaylist = await YouTubeWebsite.GetPlaylistAsync(Channel.GetChannelVideosPlaylistId(), 1);
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
