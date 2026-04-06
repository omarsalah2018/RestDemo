using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RestDemo.Data;
using RestDemo.Data.Models;
using RestDemo.Dtos;

namespace RestDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private static readonly List<User> _users = new List<User>
        {
            new User { Id = 1, Name = "Alice", Email = "Test1@test.com" },
            new User { Id = 2, Name = "Bob", Email = "Test2@test.com" }
        };

        private readonly AppDbContext _appDbContext;

        public UsersController(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
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
            //_users.Add(user);
            var userEntity = new User
            {
                Name = userDto.Name,
                Email = userDto.Email
            };
            _appDbContext.Users.Add(userEntity);
            var res = _appDbContext.SaveChanges();

            return Ok();
        }
    }
}