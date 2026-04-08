using System.Security.Claims;

namespace RestDemo.BLL.IServices
{
    public interface IUserMangment
    {
        public string Role { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }

        public ClaimsPrincipal User { get; set; }
    }
}