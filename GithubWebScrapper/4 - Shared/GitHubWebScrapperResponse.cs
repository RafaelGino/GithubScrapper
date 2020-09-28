using GitHubWebScrapper.Domain;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GithubWebScrapper._4___Shared
{
    public class GitHubWebScrapperResponse
    {
        public string GroupedExtension;
        public int TotalLines;
        public float TotalBytes;
        public IGrouping<string, GithubFileInfo> Files;



        public GitHubWebScrapperResponse(string extension, int lines, float bytes, IGrouping<string, GithubFileInfo> files) 
        {
            this.GroupedExtension = extension;
            this.TotalLines = lines;
            this.TotalBytes = bytes;
            this.Files = files;
        }
    }
}
