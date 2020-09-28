using GithubWebScrapper._1___Services.Interfaces;
using GithubWebScrapper._4___Shared;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace GithubWebScrapper.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ParserController : ControllerBase
    {
        private readonly IScrapperService _scrapperService;
        public ParserController(IScrapperService scrapperService)
        {
            _scrapperService = scrapperService;
        }

        /// <summary>
        /// Return all files of a github repository
        /// </summary>
        /// <remarks>
        /// Return all files of a given repository, grouped by extension with total number lines and size.
        /// </remarks>
        /// <param name="baseUrl">Github Repository Url</param>
        /// <returns></returns>
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        [HttpPost("read-data-repository")]
        public async Task<IActionResult> PaserUrl([FromBody] string baseUrl)
        {
            if (string.IsNullOrEmpty(baseUrl))
                return BadRequest(ResponseApi.Empty);

            var response = await Task.FromResult(_scrapperService.GetGithubFilesGroupedByExtension(baseUrl));
            return Ok(JsonConvert.SerializeObject(response));
        }
    }
}
