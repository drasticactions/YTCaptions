using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Google.Apis.YouTube.v3.Data;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;
using YoutubeExplode.Models.ClosedCaptions;
using YTCaptions.ViewModels;

namespace YTCaptions
{
    public partial class VideoPage : ContentPage
    {
        YTVideoViewModel viewModel;

        public VideoPage()
        {
            InitializeComponent();
            On<Xamarin.Forms.PlatformConfiguration.iOS>().SetUseSafeArea(true);
            BindingContext = viewModel = new YTVideoViewModel(VideoView);
        }

        public async Task PlayVideo(string videoId, ClosedCaption caption)
        {
            await viewModel.PlayVideo(videoId, caption);
        }

        async void Cancel_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }
    }
}
