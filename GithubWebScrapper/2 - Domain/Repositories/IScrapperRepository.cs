using GitHubWebScrapper.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GithubWebScrapper._2___Domain.Repositories
{
    public interface IScrapperRepository
    {
        public List<GithubFileInfo> ScrapGitHubUrl(string url);
    }
}
