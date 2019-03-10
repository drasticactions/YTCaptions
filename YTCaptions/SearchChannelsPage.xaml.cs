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
            if (!(args.SelectedItem is YoutubeExplode.Models.Video vidItem))
                return;
            VideoListView.SelectedItem = null;
            var captions = await viewModel.GetCaptions(vidItem.Id);
            if (captions.Count <= 0)
            {
                await DisplayAlert("Captions", $"No Captions Available for \"{vidItem.Title}\"!", "Dang!");
                return;
            }
            ClosedCaptionTrackInfo info;
            if (captions.Count == 1)
                info = captions.First();
            else
            {
                var capList = captions.Select(n => n.Language.Name).ToArray();
                var action = await DisplayActionSheet("Pick Caption Language: ", "Cancel", null, capList);
                info = captions.FirstOrDefault(n => n.Language.Name == action);
                if (info == null)
                    return;
            }
            var captionPage = new CaptionsPage(vidItem.Title, vidItem.Id, info);
            await Navigation.PushAsync(captionPage);
            await captionPage.GetCaptionsAsync();
        }
    }
}
