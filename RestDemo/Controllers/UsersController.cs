using Mapster;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RestDemo.BLL.IServices;
using RestDemo.BLL.Services;
using RestDemo.Data;
using RestDemo.Data.Models;
using RestDemo.Dtos;

namespace RestDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly AppDbContext _appDbContext;

        public UsersController(AppDbContext appDbContext, IUserService userService)
        {
            _appDbContext = appDbContext;
            _userService = userService;
        }

        [HttpGet("user-list")]
        public IActionResult GetUsers()
        {
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
    }
}