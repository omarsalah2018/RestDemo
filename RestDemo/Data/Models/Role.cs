using Azure.Identity;

namespace RestDemo.Data.Models
{
    public class Role
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; }

        public virtual User User { get; set; }
    }
}