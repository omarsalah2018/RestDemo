using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace RestDemo.Controllers.V1
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestPollyController : ControllerBase
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private ILogger<TestPollyController> _logger;

        public TestPollyController(IHttpClientFactory httpClientFactory, ILogger<TestPollyController> logger)
        {
            _httpClientFactory = httpClientFactory;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetPosts()
        {
            var client = _httpClientFactory.CreateClient("MyApiClient");

            var response = await client.GetAsync("posts");
            _logger.LogError("Error occurred while calling external API at {Time}", DateTime.Now);
            if (!response.IsSuccessStatusCode)
            {
                return StatusCode((int)response.StatusCode, "External API failed");
            }

            var data = await response.Content.ReadAsStringAsync();

            return Ok(data);
        }
    }
}