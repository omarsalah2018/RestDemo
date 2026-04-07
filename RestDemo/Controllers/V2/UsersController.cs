using Asp.Versioning;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace RestDemo.Controllers.V2
{
    [ApiVersion("2.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        [HttpGet]
        public IActionResult TestV2GetUserData()
        {
            return Ok("Test V2 Running>>>>");
        }
    }
}