using MediatR;

namespace TestWebApiApp.Core.Commands
{
    public class DeleteUserCommand : IRequest
    {
        public int UserId { get; set; }
    } 
}
