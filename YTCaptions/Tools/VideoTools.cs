using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using YoutubeExplode.Models.ClosedCaptions;
using YoutubeExplode.Models.MediaStreams;

namespace YTCaptions.Tools
{
    public static class VideoTools
    {
        public static async Task<Stream> GetVideoClip(MuxedStreamInfo info, TimeSpan duration, ClosedCaption caption)
        {
            var secondLength = info.Size / (duration.TotalSeconds);
            var startRange = (caption.Offset.TotalSeconds * secondLength) + 1;
            var endRange = ((caption.Offset.TotalSeconds + caption.Duration.TotalSeconds) * secondLength) + 1;
            var testCurl = $"curl \"{info.Url}\" -H \"Range: bytes={(long)startRange}-{(long)endRange}\" -o ten_secs.mp4";
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Range = new System.Net.Http.Headers.RangeHeaderValue((long)startRange, (long)endRange);
                return await client.GetStreamAsync(info.Url);
            }
        }
    }
}
