using FluentValidation;
using TestWebApiApp.Core.Commands;

namespace TestWebApiApp.Core.Validators
{
    public class UpdateUserValidator : AbstractValidator<UpdateUserCommand>
    {
        public UpdateUserValidator()
        {
            RuleFor(x => x.NewUsername).NotEmpty().MaximumLength(50);
        }
    }
}
