using FluentValidation;
using TestWebApiApp.Core.Commands;

namespace TestWebApiApp.Core.Validators
{
    public class DeleteUserValidator : AbstractValidator<DeleteUserCommand>
    {
        public DeleteUserValidator()
        {
            RuleFor(x => x.UserId).GreaterThan(0);
        }
    }
}
