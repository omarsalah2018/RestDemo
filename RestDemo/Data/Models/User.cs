using Azure.Identity;

namespace RestDemo.Data.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public int Age { get; set; } = 18;
        public bool IsActive { get; set; } = true;
        public int? RoleId { get; set; }
        public virtual Role Role { get; set; }

        public virtual ICollection<UserRooms> UserRooms { get; set; } = new List<UserRooms>();
    }
}