using System.Linq;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;
using YoutubeExplode.Models.ClosedCaptions;
using YTCaptions.ViewModels;

namespace YTCaptions
{
    public partial class SearchChannelsPage : ContentPage
    {
        YTSearchChannelsViewModel viewModel;
        public SearchChannelsPage()
        {
            InitializeComponent();
            On<Xamarin.Forms.PlatformConfiguration.iOS>().SetUseSafeArea(true);
            BindingContext = viewModel = new YTSearchChannelsViewModel();
        }

        async void OnSelectedItem(object sender, SelectedItemChangedEventArgs args)
        {
            if (!(args.SelectedItem is YoutubeExplode.Models.Channel vidItem))
                return;
            var channelPage = new ChannelVideoListPage(vidItem);
            await Navigation.PushAsync(channelPage);
            await channelPage.GetPlaylistAsync();
            VideoListView.SelectedItem = null;
        }
    }
}
