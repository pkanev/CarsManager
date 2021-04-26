using FluentValidation;

namespace CarsManager.Application.Users.Commands.CreateUser
{
    public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
    {
        public CreateUserCommandValidator()
        {
            RuleFor(c => c.Username)
                .NotEmpty();
            RuleFor(c => c.Password)
                .NotEmpty();
            RuleFor(c => c.FirstName)
                .NotEmpty();
            RuleFor(c => c.LastName)
                .NotEmpty();
        }
    }
}
