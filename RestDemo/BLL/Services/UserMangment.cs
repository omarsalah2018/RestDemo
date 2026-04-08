using RestDemo.BLL.IServices;
using System.Security.Claims;

namespace RestDemo.BLL.Services
{
    public class UserMangment : IUserMangment
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserMangment(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public ClaimsPrincipal User => _httpContextAccessor.HttpContext.User;

        public string Role { get => User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value; set => throw new NotImplementedException(); }
        public string Name { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string Email { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        ClaimsPrincipal IUserMangment.User { get => User; set => throw new NotImplementedException(); }
    }
}