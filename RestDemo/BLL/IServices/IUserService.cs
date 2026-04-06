using RestDemo.Dtos;

namespace RestDemo.BLL.IServices
{
    public interface IUserService
    {
        bool CreateUser(CreateUserDto userDto);
    }
}