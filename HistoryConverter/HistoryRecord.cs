using System;

namespace HistoryConverter
{
    public class HistoryRecord
    {
        public string Header { get; set; }

        public string Title { get; set; }

        public Uri TitleUrl { get; set; }

        public Subtitle[] Subtitles { get; set; }

        public DateTime? Time { get; set; }

        public string[] Products { get; set; }
    }

    public class Subtitle
    {
        public string Name { get; set; }

        public Uri Url { get; set; }
    }
}
