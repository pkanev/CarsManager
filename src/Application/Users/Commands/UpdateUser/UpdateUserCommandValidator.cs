using FluentValidation;

namespace CarsManager.Application.Users.Commands.UpdateUser
{
    public class UpdateUserCommandValidator : AbstractValidator<UpdateUserCommand>
    {
        public UpdateUserCommandValidator()
        {
            RuleFor(c => c.Id)
                .NotEmpty();
            RuleFor(c => c.FirstName)
                .NotEmpty();
            RuleFor(c => c.LastName)
                .NotEmpty();
        }
    }
}
