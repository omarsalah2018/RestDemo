using Asp.Versioning;
using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using RestDemo.BLL.IServices;
using RestDemo.BLL.Services;
using RestDemo.Data;
using RestDemo.Data.Models;
using RestDemo.Dtos;

namespace RestDemo.Controllers.V1
{
    [ApiVersion("1.0", Deprecated = true)]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [Authorize]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly AppDbContext _appDbContext;
        private readonly IUserMangment _userMangment;

        public UsersController(AppDbContext appDbContext, IUserService userService, IUserMangment userMangment)
        {
            _appDbContext = appDbContext;
            _userService = userService;
            _userMangment = userMangment;
        }

        [HttpGet("user-list")]
        public IActionResult GetUsers()
        {
            var role = _userMangment.Role;
            var user = _userMangment.User;
            // var users = _users;
            var users = _appDbContext.Users.ToList();
            return Ok(users);
        }

        [HttpGet("get-user-by-id")]
        public IActionResult GetUserById([FromQuery] int id)
        {
            var user = _appDbContext.Users.FirstOrDefault(u => u.Id == id);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }

        [HttpPost]
        public IActionResult CreateUser([FromBody] CreateUserDto userDto)
        {
            return Ok(_userService.CreateUser(userDto));
        }

        [HttpGet("test-async")]
        public async Task<IActionResult> TestAsync()
        {
            Task.Delay(9000);

            return Ok("Done");
        }
    }
}