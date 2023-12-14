using MediatR;
using TestWebApiApp.Core.Models;
using TestWebApiApp.Core.Queries;
using TestWebApiApp.Core.Repository;

namespace TestWebApiApp.Core.Handlers
{
    public class GetUserHandler : IRequest<GetUserQuery>
    {
        private readonly IUserRepository _userRepository;

        public GetUserHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<User> Handle(GetUserQuery request, CancellationToken cancellationToken)
        {
            // Query user by Id
            return await _userRepository.GetByIdAsync(request.UserId, cancellationToken);
        }
    }
}
