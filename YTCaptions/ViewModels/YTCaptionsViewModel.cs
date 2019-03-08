using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Google.Apis.YouTube.v3.Data;
using Newtonsoft.Json;
using Xamarin.Forms;
using YoutubeExplode.Models.ClosedCaptions;
using YoutubeExplode.Models.MediaStreams;
using YTCaptions.Tools;

namespace YTCaptions.ViewModels
{
    public class YTCaptionsViewModel : BaseViewModel
    {
        ClosedCaptionTrackInfo trackInfo;
        ClosedCaptionTrack track;

        List<ClosedCaption> captions = new List<ClosedCaption>();
        public List<ClosedCaption> Captions {
            get { return captions; }
            set { SetProperty(ref captions, value); }
        }

        public string Text { get; set; }
        public List<ClosedCaption> AllCaptions { get; set; } = new List<ClosedCaption>();
        public string VideoTitle { get; set; }
        string videoId;
        public Command SearchCommand { get; set; }

        public ClosedCaptionTrack Track
        {
            get { return track; }
            set { SetProperty(ref track, value); }
        }

        public YTCaptionsViewModel(string title, string id, ClosedCaptionTrackInfo item)
        {
            SearchCommand = new Command(async (text) => await ExecuteSearchCommand((string)text));
            trackInfo = item;
            VideoTitle = title;
            videoId = id;
        }

        async Task ExecuteSearchCommand(string searchParameter)
        {
            if (string.IsNullOrEmpty(searchParameter))
                Captions = AllCaptions;
            else
                Captions = AllCaptions.Where(n => n.Text.Contains(searchParameter)).ToList();
        }

        public async Task ExecuteGetCaptionAsync()
        {
            IsBusy = true;

            try
            {
                Track = await YouTubeWebsite.GetClosedCaptionTrackAsync(trackInfo);
                AllCaptions = Track.Captions.ToList();
                Captions = AllCaptions;
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

        public async Task TempGetVideoLink(ClosedCaption caption)
        {
            var videoMedia = await YouTubeWebsite.GetVideoMediaStreamInfosAsync(videoId);
            var video = await YouTubeWebsite.GetVideoAsync(videoId);
            var test = JsonConvert.SerializeObject(video);
            var test2 = JsonConvert.SerializeObject(videoMedia);
            var test3 = videoMedia.Muxed.WithHighestVideoQuality();
            var poop = await VideoTools.GetVideoClip(videoMedia.Muxed.WithHighestVideoQuality(), video.Duration, caption);
        }
    }
}

