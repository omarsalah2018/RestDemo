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
            //  var roomsList = _appDbContext.UserRooms.Include(x => x.Room).Where(x => x.UserId == userId).ToList();

            List<Room> roomsList = new List<Room>();
            var userRooms = _appDbContext.UserRooms.Where(x => x.UserId == userId).ToList();
            foreach (var item in userRooms)
            {
                var room = _appDbContext.Rooms.Where(x => x.Id == item.RoomId).FirstOrDefault();
                if (room != null)
                    roomsList.Add(room);
            }

            return Ok(roomsList);
        }
    }
}