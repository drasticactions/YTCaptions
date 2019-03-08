﻿using System;
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

        public CaptionsPage(string title, string id, ClosedCaptionTrackInfo info)
        {
            InitializeComponent();
            On<Xamarin.Forms.PlatformConfiguration.iOS>().SetUseSafeArea(true);
            BindingContext = viewModel = new YTCaptionsViewModel(title, id, info);
        }

        public async Task GetCaptionsAsync()
        {
            await viewModel.ExecuteGetCaptionAsync();
        }

        async void OnSelectedItem(object sender, SelectedItemChangedEventArgs args)
        {
            await viewModel.TempGetVideoLink();
            CaptionsListView.SelectedItem = null;
        }
    }
}
