namespace RestDemo.Data.Models
{
    public class UserRooms
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int RoomId { get; set; }

        public virtual User User { get; set; } = null!;
        public virtual Room Room { get; set; } = null!;
    }
}