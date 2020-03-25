using System;
using System.Linq;
using System.Net;
using System.Web;

namespace HistoryConverter
{
    public class View
    {
        public string Id { get; private set; }

        public string Title { get; private set; }

        public Uri Url { get; private set; }

        public DateTime? Date { get; private set; }

        public int? Year { get; private set; }

        public int? Month { get; private set; }

        public string ChannelName { get; private set; }

        public Uri ChannelUrl { get; private set; }

        public View(HistoryRecord record)
        {
            Id = record.TitleUrl != null ? HttpUtility.ParseQueryString(record.TitleUrl.Query).Get("v") : null;
            Title = WebUtility.UrlDecode(record.Title);
            Url = record.TitleUrl;

            Date = record.Time;
            if (Date.HasValue)
            {
                Year = record.Time.Value.Year;
                Month = record.Time.Value.Month;
            }

            var channel = record.Subtitles?.FirstOrDefault();
            if (channel != null)
            {
                ChannelName = WebUtility.UrlDecode(channel.Name);
                ChannelUrl = channel.Url;
            }
        }
    }
}
