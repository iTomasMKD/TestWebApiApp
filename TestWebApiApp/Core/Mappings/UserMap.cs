using TestWebApiApp.Core.Commands;
using TestWebApiApp.Core.DTO;
using TestWebApiApp.Core.Models;

namespace TestWebApiApp.Core.Mappings
{
    public class UserMap : User
    {
        public UserProfile()
        {
            CreateMap<User, UserDto>();
            CreateMap<CreateUserCommand, User>();
            CreateMap<UpdateUserCommand, User>();
        }
    }
}
