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
    public partial class ChannelPage : ContentPage
    {
        YTChannelViewModel viewModel;
        public ChannelPage(string title)
        {
            InitializeComponent();
            On<Xamarin.Forms.PlatformConfiguration.iOS>().SetUseSafeArea(true);
            BindingContext = viewModel = new YTChannelViewModel(title);
        }

        public async Task SetChannelInfoAsync(string channelId)
        {
            await viewModel.ExecuteChannelInfoCommand(channelId);
            await viewModel.ExecuteChannelListCommand();
        }

        async void OnSelectionChanged(object sender, SelectionChangedEventArgs args)
        {
            // TODO: Temp workaround
            if (args.CurrentSelection.Count != 1)
                return;
            var item = args.CurrentSelection.First() as PlaylistItem;
            if (item == null) return;
            var caption = await viewModel.TempGetCaptions(item);

        }
    }
}
