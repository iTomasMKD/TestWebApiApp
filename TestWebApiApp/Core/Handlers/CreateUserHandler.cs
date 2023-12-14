using AutoMapper;
using FluentValidation;
using MediatR;
using Serilog;
using TestWebApiApp.Core.Commands;
using TestWebApiApp.Core.Models;
using TestWebApiApp.Core.Repository;

namespace TestWebApiApp.Core.Handlers
{
    public class CreateUserHandler : IRequest<CreateUserCommand>
    {
        private readonly IUserRepository _userRepository;
        private readonly IValidator<CreateUserCommand> _validator;
        private readonly IMapper _mapper;

        public CreateUserHandler(IUserRepository userRepository, IValidator<CreateUserCommand> validator, IMapper mapper)
        {
            _userRepository = userRepository;
            _validator = validator;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            try
            {
                // Validate the command using FluentValidation
                var validationResult = await _validator.ValidateAsync(request, cancellationToken);
                if (!validationResult.IsValid)
                {
                    throw new ValidationException(validationResult.Errors);
                }

                // Map CreateUserCommand to User
                var newUser = _mapper.Map<User>(request);

                // Business logic and user creation
                await _userRepository.AddAsync(newUser, cancellationToken);

                return Unit.Value;
            }
            catch (Exception ex)
            {
                // Log the exception
                Log.Error(ex, "An error occurred while processing CreateUserCommand.");

                // Rethrow the exception for global handling
                throw;
            }
        }
    }
}
