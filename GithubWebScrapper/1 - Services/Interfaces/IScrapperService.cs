using GithubWebScrapper._4___Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GithubWebScrapper._1___Services.Interfaces
{
    public interface IScrapperService
    {
        public IEnumerable<GitHubWebScrapperResponse> GetGithubFilesGroupedByExtension(string url);
    }
}
