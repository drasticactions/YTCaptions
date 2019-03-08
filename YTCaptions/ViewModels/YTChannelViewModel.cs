using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Linq;

using Xamarin.Forms;

using Google.Apis.Services;
using Google.Apis.YouTube.v3;
using Google.Apis.YouTube.v3.Data;

using Newtonsoft.Json;
using YoutubeExplode;
using YoutubeExplode.Models.ClosedCaptions;

namespace YTCaptions.ViewModels
{
    public class YTChannelViewModel : BaseViewModel
    {
        public YTChannelViewModel(string channelTitle) {
            ChannelTitle = channelTitle;
        }

        public string ChannelTitle { get; set; }

        PlaylistItemListResponse listResponse = new PlaylistItemListResponse();
        Channel channel = new Channel();
        public Channel Channel
        {
            get { return channel; }
            set { SetProperty(ref channel, value); }
        }

        public ObservableRangeCollection<PlaylistItem> Items { get; set; } = new ObservableRangeCollection<PlaylistItem>();

        public async Task ExecuteChannelInfoCommand(string channelId)
        {
            IsBusy = true;
            try
            {
                var test = YouTubeService.Channels.List("snippet,brandingSettings,contentDetails");
                test.Id = channelId;
                var result = await test.ExecuteAsync();
                Channel = result.Items.First();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                IsBusy = false;
            }
        }

        public async Task ExecuteChannelListCommand()
        {
            if (string.IsNullOrEmpty(channel.Id))
                return;

            Items.RemoveAll(n => true);
            var channelListRequest = YouTubeService.PlaylistItems.List("snippet");
            channelListRequest.PlaylistId = Channel.ContentDetails.RelatedPlaylists.Uploads;
            channelListRequest.MaxResults = 25;
            listResponse = await channelListRequest.ExecuteAsync();
            Items.AddRange(listResponse.Items);
        }

        public async Task<ClosedCaptionTrackInfo> TempGetCaptions(PlaylistItem item)
        {
            var captions = await YouTubeWebsite.GetVideoClosedCaptionTrackInfosAsync(item.Snippet.ResourceId.VideoId);
            return captions.FirstOrDefault();
        }
    }
}
