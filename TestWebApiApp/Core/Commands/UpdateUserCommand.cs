using MediatR;

namespace TestWebApiApp.Core.Commands
{
    //public record UpdateUserCommand(int UserId, string NewUsername);
     public class UpdateUserCommand : IRequest
     {
            public int UserId { get; }
            public string NewUsername { get; }

            public UpdateUserCommand(int userId, string newUsername)
            {
                UserId = userId;
                NewUsername = newUsername;
            }
     }
 }
