using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Google.Apis.YouTube.v3.Data;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;
using YTCaptions.ViewModels;
using YoutubeExplode.Models.ClosedCaptions;

namespace YTCaptions
{
    public partial class CaptionsPage : ContentPage
    {
        YTCaptionsViewModel viewModel;

        public CaptionsPage(string title, ClosedCaptionTrackInfo info)
        {
            InitializeComponent();
            On<Xamarin.Forms.PlatformConfiguration.iOS>().SetUseSafeArea(true);
            BindingContext = viewModel = new YTCaptionsViewModel(title, info);
        }
    }
}
