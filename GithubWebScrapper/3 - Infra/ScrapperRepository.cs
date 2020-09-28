using GithubWebScrapper._2___Domain.Repositories;
using GithubWebScrapper._4___Shared;
using GitHubWebScrapper.Domain;
using GitHubWebScrapper.Shared;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace GithubWebScrapper._3___Infra
{
    public class ScrapperRepository : IScrapperRepository
    {
        public IList<string> foldersUrl;
        List<GithubFileInfo> githubFileInfos;
        public ScrapperRepository()
        {
            this.githubFileInfos = new List<GithubFileInfo>();
        }

        public List<GithubFileInfo> ScrapGitHubUrl(string url)
        {
            try
            {
                var adresses = LoadPageAdresses(url);
                foldersUrl = new List<string>();

                for (int i = 0; i < adresses.Count; i++)
                {
                    if (IsFolder(adresses[i]))
                    {
                        foldersUrl.Add($"{Constants.GITHUB_URL_BASE}{adresses[i]}");
                    }
                    else
                    {
                        githubFileInfos.Add(LoadFileData($"{Constants.GITHUB_URL_BASE}{adresses[i]}"));
                    }
                }

                if (foldersUrl.Count > 0)
                {
                    foreach (var folderUrl in foldersUrl)
                    {
                        ScrapGitHubUrl(folderUrl);
                    }
                }

                return githubFileInfos;
            }
            catch (Exception e)
            {

                throw;
            }

        }

        private string LoadStringUrl(string url)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.AutomaticDecompression = DecompressionMethods.GZip;
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            Stream responseStream = response.GetResponseStream();
            StreamReader streamReader = new StreamReader(responseStream);
            return streamReader.ReadToEnd();
        }

        private string LoadPageFromUrl(string url)
        {
            using (var client = new HttpClient())
            {
                var response = client.GetAsync(url).Result;
                if (response.IsSuccessStatusCode)
                {
                    return response.Content.ReadAsStringAsync().Result;
                }
                return "";
            }
           
        }
        private List<string> LoadPageAdresses(string url)
        {
            string page = LoadPageFromUrl(url);
            Regex regexSpan = new Regex(Constants.GITHUB_FILES_SCRAP_REGEX);
            var matchedLines = regexSpan.Matches(page).Cast<Match>().Select(m => m.Value).Distinct().ToList();

            Regex regexScrapStartUrl = new Regex(Constants.GITHUB_FILE_SCRAP_START);
            return matchedLines.Select(x => regexScrapStartUrl.Match(x).Value.Replace("href=\"", "")).ToList();
        }

        public GithubFileInfo LoadFileData(string url)
        {
            try
            {
                var html = LoadStringUrl(url);
                var fileStats = ReturnFileStats(html);
                var size = ReturnSizeFileInBytes(ReturnSizeStats(fileStats));
                var lines = ReturnNumberLines(fileStats);
                var archiveName = ReturnExtension(html);
                var extension = Path.GetExtension(archiveName);

                return new GithubFileInfo(archiveName, url, extension, size, lines);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        private bool IsFolder(string url)
        {
            return url.Contains("tree");
        }

        private int ReturnNumberLines(string fileStats)
        {
            try
            {
                var numberOfLines = this.ValueBetweenString(fileStats, Constants.GITHUB_LINE_INFO_START, Constants.GITHUB_LINE_INFO_END).Trim();
                return !string.IsNullOrEmpty(numberOfLines) ? int.Parse(numberOfLines) : 0;
            }
            catch (Exception e)
            {
                return 0;
            }
        }
        private string ReturnSizeStats(string fileStats)
        {
            try
            {
                return ValueBetweenString(fileStats, Constants.GITHUB_SPAN_ENDING_TAG, Constants.GITHUB_DIV_ENDING_TAG).Trim();
            }
            catch (Exception e)
            {
                return string.Empty;
            }
        }
        private string ReturnFileStats(string htmlString)
        {
            return Regex.Match(htmlString, Constants.GITHUB_FILE_STATS).Value;
        }

        private string ReturnExtension(string fileStats)
        {
            return this.ValueBetweenString(fileStats, Constants.GITHUB_FILE_NAME_START, Constants.GITHUB_FILE_NAME_END).Trim();
        }

        public string ValueBetweenString(string text, string start, string end)
        {
            if (text.IndexOf(start) > 0)
            {
                int indexStart = text.IndexOf(start) + start.Length;
                int indexEnd = text.IndexOf(end, indexStart);
                return text.Substring(indexStart, indexEnd - indexStart);
            }

            return string.Empty;
        }

        private float ReturnSizeFileInBytes(string bytes)
        {
            try
            {
                float bytesFloat = 0;
                if (!string.IsNullOrEmpty(bytes))
                {

                    if (bytes.Contains("KB"))
                    {
                        bytes = Regex.Match(bytes, @"(\d+)?.?(\d+) KB").Value;
                        bytes = Regex.Match(bytes, @"(\d+)?.?(\d+)").Value;
                        bytesFloat = float.Parse(bytes) * 1000;

                    }
                    else if (bytes.Contains("MB"))
                    {
                        bytes = Regex.Match(bytes, @"(\d+).(\d+) MB").Value;
                        bytes = Regex.Match(bytes, @"(\d+).(\d+)").Value;
                        bytesFloat = float.Parse(bytes) * 1000000;
                    }
                    else
                    {
                        bytes = Regex.Match(bytes, @"(\d+) Bytes").Value;
                        bytes = Regex.Match(bytes, @"(\d+)").Value;
                        bytesFloat = float.Parse(bytes);
                    }
                }

                return bytesFloat;
            }
            catch (Exception e)
            {
                return 0;
            }

        }
    }
}
