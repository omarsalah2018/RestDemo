using System.Text.Json.Serialization;

namespace RestDemo.Data.Models
{
    public class Room
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Capastiy { get; set; }

        [JsonIgnore]
        public virtual ICollection<UserRooms> UserRooms { get; set; } = new List<UserRooms>();
    }
}