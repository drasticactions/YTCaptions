using System;
using System.Threading.Tasks;
using LibVLCSharp.Forms.Shared;
using LibVLCSharp.Shared;
using Xamarin.Forms;
using YoutubeExplode.Models.ClosedCaptions;
using YoutubeExplode.Models.MediaStreams;

namespace YTCaptions.ViewModels
{
    public class YTVideoViewModel : BaseViewModel
    {
        LibVLC _libVLC;
        MediaPlayer _mediaPlayer;
        VideoView _videoView;
        float _position;
        ClosedCaption closedCaption;

        public ClosedCaption Caption
        {
            get { return closedCaption; }
            set { SetProperty(ref closedCaption, value); }
        }

        public YTVideoViewModel(VideoView videoView)
        {
            _libVLC = new LibVLC();
            _videoView = videoView;
        }


        public async Task PlayVideo(string videoId, ClosedCaption caption)
        {
            Caption = caption;
            var videoMedia = await YouTubeWebsite.GetVideoMediaStreamInfosAsync(videoId);

            var media = new Media(_libVLC,
                videoMedia.Muxed.WithHighestVideoQuality().Url,
                FromType.FromLocation);

            media.AddOption($":start-time={string.Format("{0:D2}.{1:D2}", (int)caption.Offset.TotalSeconds, caption.Offset.Milliseconds)}");
            media.AddOption($":run-time={string.Format("{0:D2}.{1:D2}", (int)caption.Duration.TotalSeconds, caption.Duration.Milliseconds)}");

            _mediaPlayer = new MediaPlayer(_libVLC);
            _videoView.MediaPlayer = _mediaPlayer;
            _mediaPlayer.Play(media);
        }
    }
}

