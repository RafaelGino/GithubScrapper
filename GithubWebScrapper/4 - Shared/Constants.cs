using System;

namespace GitHubWebScrapper.Shared
{
    public class Constants
    {
        public const string GITHUB_FILES_SCRAP_REGEX = "(?s)(?i)<div role=\"rowheader\" class=\"flex-auto min-width-0 col-md-2 mr-3\">(.*?)<\\/div>";
        public const string GITHUB_FILE_SCRAP_START = "href=\"([^\"]+)";
        public const string GITHUB_URL_BASE = "https://github.com";
        public const string GITHUB_FILE_STATS_START = "<div class=\"text-mono f6 flex-auto pr-3 flex-order-2 flex-md-order-1 mt-2 mt-md-0\">"; 
        public const string GITHUB_FILE_STATS = "(?s)(?i)<div class=\"text-mono f6 flex-auto pr-3 flex-order-2 flex-md-order-1 mt-2 mt-md-0\">(.*?)<\\/div>";
        public const string GITHUB_BYTES_STATS = "(\\d+)";
        public const string GITHUB_LINES_STATS = "(?s)(?i)\\>(.*?) lines";        
        public const string GITHUB_LINE_INFO_START = "mt-md-0\">";
        public const string GITHUB_LINE_INFO_END = " lines";
        public const string GITHUB_SPAN_ENDING_TAG = "</span>";
        public const string GITHUB_DIV_ENDING_TAG = "</div>";
        public const string GITHUB_FILE_NAME_START = "<strong class=\"final-path\">";
        public const string GITHUB_FILE_NAME_END = "</strong>";
    }
}


