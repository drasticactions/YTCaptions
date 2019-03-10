using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Google.Apis.YouTube.v3.Data;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;
using YTCaptions.ViewModels;

namespace YTCaptions
{
    public partial class SearchVideosPage : ContentPage
    {
        YTSearchVideosViewModel viewModel;
        public SearchVideosPage()
        {
            InitializeComponent();
            On<Xamarin.Forms.PlatformConfiguration.iOS>().SetUseSafeArea(true);
            BindingContext = viewModel = new YTSearchVideosViewModel();
        }

        async void OnSelectionChanged(object sender, SelectionChangedEventArgs args)
        {
            if (args.CurrentSelection.Count != 1)
                return;
            if (!(args.CurrentSelection[0] is YoutubeExplode.Models.Video searchItem))
                return;
            //switch (searchItem.Id.Kind)
            //{
            //    case "youtube#channel":
            //        var channelPage = new ChannelPage(searchItem.Snippet.ChannelTitle);
            //        await Navigation.PushAsync(channelPage);
            //        await channelPage.SetChannelInfoAsync(searchItem.Id.ChannelId);
            //        break;
            //}
            SearchCollectionView.SelectedItem = null;
        }
    }
}
