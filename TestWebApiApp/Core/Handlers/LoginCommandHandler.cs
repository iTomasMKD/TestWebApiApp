using MediatR;
using TestWebApiApp.Core.Commands;

namespace TestWebApiApp.Core.Handlers
{
    public class LoginCommandHandler : IRequestHandler<LoginCommand, string>
    {
        public Task<string> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            // Implement user login logic here
            // You can use services like UserManager, authentication services, etc.

            // For simplicity, assume a successful login
            return Task.FromResult("User login successful");
        }
    }
}
