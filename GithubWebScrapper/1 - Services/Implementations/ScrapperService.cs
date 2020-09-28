using GithubWebScrapper._1___Services.Interfaces;
using GithubWebScrapper._2___Domain.Repositories;
using GithubWebScrapper._4___Shared;
using GitHubWebScrapper.Domain;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace GithubWebScrapper._1___Services.Implementations
{
    public class ScrapperService: IScrapperService
    {
        public readonly IScrapperRepository _scrapperRepository;
        public ScrapperService(IScrapperRepository scrapperRepository)
        {
            _scrapperRepository = scrapperRepository;
        }
        public IEnumerable<GitHubWebScrapperResponse> GetGithubFilesGroupedByExtension(string url)
        {
            var allFiles = _scrapperRepository.ScrapGitHubUrl(url);
            var data = MapData(allFiles);
            return data;
        }
        private IList<GitHubWebScrapperResponse> MapData(IList<GithubFileInfo> gitHubFiles)
        {
            var filesGrouped = gitHubFiles.GroupBy(f => f.Extension).ToList();
            var Grouped = new List<GitHubWebScrapperResponse>();

            int lines = 0;
            float bytes = 0;            

            for (int i = 0; i < filesGrouped.Count; i++)
            {
                lines = 0;
                bytes = 0;
                filesGrouped[i].ToList().ForEach(f => lines += f.Lines);
                filesGrouped[i].ToList().ForEach(f => bytes += f.Bytes);

                Grouped.Add(new GitHubWebScrapperResponse(filesGrouped[i].Key, lines, bytes, filesGrouped[i]));
            }
            return Grouped;
        }
    }
}
