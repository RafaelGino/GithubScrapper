
using FluentAssertions;
using GitHubWebScrapper.Domain;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Xunit;

namespace Test.Unit
{
    [TestClass]
    public class GithubFileInfoSpec
    {
        [Fact]
        public void TestConstructor()
        {
            var fileInfo = new GithubFileInfo("FileInfo.cs", 
                                              "https://github.com/RafaelGino/GithubScrapper/blob/master/GithubWebScrapper/2%20-%20Domain/FileInfo.cs",
                                              ".cs",
                                              100, 
                                              44);
            fileInfo.Should().NotBeNull();
        }
    }
}
