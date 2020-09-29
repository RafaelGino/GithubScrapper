
using FluentAssertions;
using GitHubWebScrapper.Shared;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json.Linq;
using System;
using Xunit;

namespace GithubWebScrapper.Test.Unit
{
    [TestClass]
    public class ParserControllerSpec
    {
        [Fact]
        public void TestReturn()
        {
            var validJson = isValidJSON(Constants.GITHUB_JSON_EXAMPLE);
            validJson.Should().Be(true);
        }

        private bool isValidJSON(string json)
        {
            try
            {
                JToken token = JObject.Parse(json);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
