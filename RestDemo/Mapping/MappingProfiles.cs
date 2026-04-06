using Mapster;
using RestDemo.Data.Models;
using RestDemo.Dtos;

namespace RestDemo.Mapping
{
    public class MappingProfiles
    {
        public static void RegisterMappings()
        {
            // MapsterConfig.RegisterMappings();
            TypeAdapterConfig<CreateUserDto, User>.NewConfig()
                .Map(dest => dest.Name, src => src.UserName);
        }
    }
}