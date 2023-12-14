using AutoMapper;
using TestWebApiApp.Core.Commands;
using TestWebApiApp.Core.DTO;
using TestWebApiApp.Core.Models;

namespace TestWebApiApp.Core.Mappings
{
    public class UserMap : Profile
    {
        public UserMap()
        {
            CreateMap<User, UserDto>();
            CreateMap<CreateUserCommand, User>();
            CreateMap<UpdateUserCommand, User>();
        }
    }
}
