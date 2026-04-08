using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RestDemo.Data;
using RestDemo.Data.Models;

namespace RestDemo.Controllers.V1
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoomsController : ControllerBase
    {
        private readonly AppDbContext _appDbContext;

        public RoomsController(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        [HttpGet]
        public IActionResult GetRoomsByUserId([FromQuery] int userId)
        {
            //1-- Egger Loading
            //  var roomsList = _appDbContext.UserRooms.Include(x => x.Room).Where(x => x.UserId == userId).ToList();

            //2- Lazy Loading
            var userRooms = _appDbContext.UserRooms.Where(x => x.UserId == userId).ToList();
            var roomsList = userRooms.Select(ur => ur.Room).OfType<Room>().ToList();

            //3- Explicit Loading
            // List<Room> roomsList = new List<Room>();
            // var userRooms = _appDbContext.UserRooms.Where(x => x.UserId == userId).ToList();
            // foreach (var item in userRooms)
            // {
            //     var room = _appDbContext.Rooms.Where(x => x.Id == item.RoomId).FirstOrDefault();
            //     if (room != null)
            //         roomsList.Add(room);
            // }

            return Ok(roomsList);
        }

        [HttpGet("get-all-rooms")]
        public IActionResult GetAllRooms([FromQuery] int capasity = 1, [FromQuery] int page = 1, [FromQuery] int pageSize = 3)
        {
            //filtering
            //var roomsList = _appDbContext.Rooms.Where(x => x.Capastiy > capasity).ToList();

            //sorting
            //var roomsList = _appDbContext.Rooms.OrderByDescending(x => x.Name).ToList();

            //pagination
            var roomsList = _appDbContext.Rooms.Skip((page - 1) * pageSize).Take(pageSize).ToList();

            return Ok(roomsList);
        }
    }
}