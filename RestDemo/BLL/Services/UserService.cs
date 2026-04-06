using Mapster;
using RestDemo.BLL.IServices;
using RestDemo.Data;
using RestDemo.Data.Models;
using RestDemo.Dtos;

namespace RestDemo.BLL.Services
{
    public class UserService : IUserService
    {
        private readonly AppDbContext _appDbContext;
        private readonly ILogger<UserService> _logger;

        public UserService(AppDbContext appDbContext, ILogger<UserService> logger)
        {
            _appDbContext = appDbContext;
            _logger = logger;
        }

        public bool CreateUser(CreateUserDto userDto)
        {
            try
            {
                _logger.LogInformation("Creating user with username: {UserName} at {Time}", userDto.UserName, DateTime.Now);
                var userEntity = userDto.Adapt<User>();
                _appDbContext.Users.Add(userEntity);
                var res = _appDbContext.SaveChanges();
                if (res > 0)
                    return true;

                return false;
            }
            catch (Exception)
            {
                _logger.LogError("Error occurred while creating user with username: {UserName} at {Time}", userDto.UserName, DateTime.Now);
                return false;
            }
            finally
            {
                _logger.LogInformation("Finished creating user with username: {UserName} at {Time}", userDto.UserName, DateTime.Now);
            }
        }
    }
}