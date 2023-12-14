using AutoMapper;
using FluentValidation;
using MediatR;
using Serilog;
using TestWebApiApp.Core.Commands;
using TestWebApiApp.Core.Models;
using TestWebApiApp.Core.Repository;

namespace TestWebApiApp.Core.Handlers
{
    public class UpdateUserHandler : IRequest<UpdateUserCommand>
    {
        private readonly IUserRepository _userRepository;
        private readonly IValidator<UpdateUserCommand> _validator;
        private readonly IMapper _mapper;

        public UpdateUserHandler(IUserRepository userRepository, IValidator<UpdateUserCommand> validator, IMapper mapper)
        {
            _userRepository = userRepository;
            _validator = validator;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            try
            {
                // Validate the command using FluentValidation
                var validationResult = await _validator.ValidateAsync(request, cancellationToken);
                if (!validationResult.IsValid)
                {
                    throw new ValidationException(validationResult.Errors);
                }

                // Map UpdateUserCommand to User
                var user = await _userRepository.GetByIdAsync(request.UserId, cancellationToken);
                if (user != null)
                {
                    _mapper.Map(request, user);

                    // Business logic and user update
                    await _userRepository.UpdateAsync(user, cancellationToken);
                }

                return Unit.Value;
            }
            catch (Exception ex)
            {
                // Log the exception
                Log.Error(ex, "An error occurred while processing UpdateUserCommand.");

                // Rethrow the exception for global handling
                throw;
            }
        }
    }
}
