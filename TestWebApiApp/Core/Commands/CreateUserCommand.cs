using MediatR;

namespace TestWebApiApp.Core.Commands
{
    //public class CreateUserCommand(string Username, string Password);  c#12 

    //IRequest interface is often used to define request messages that represent commands or queries

    public class CreateUserCommand : IRequest
    {
        public string Username { get; }
    public string Password { get; }

    public CreateUserCommand(string username, string password)
    {
        Username = username;
        Password = password;
    }
}
}
