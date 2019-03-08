using System;
using System.Threading.Tasks;
using YoutubeExplode.Models.ClosedCaptions;

namespace YTCaptions.ViewModels
{
    public class YTCaptionsViewModel : BaseViewModel
    {
        ClosedCaptionTrackInfo trackInfo;
        ClosedCaptionTrack track;
        public string VideoTitle { get; set; }
        public ClosedCaptionTrack Track
        {
            get { return track; }
            set { SetProperty(ref track, value); }
        }
        public YTCaptionsViewModel(string title, ClosedCaptionTrackInfo item)
        {
            trackInfo = item;
            VideoTitle = title;
        }

        public async Task ExecuteGetCaptionAsync()
        {
            IsBusy = true;

            try
            {
                Track = await YouTubeWebsite.GetClosedCaptionTrackAsync(trackInfo);
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

