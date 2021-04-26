using FluentValidation;

namespace CarsManager.Application.Users.Commands.DeleteUser
{
    public class DeleteUserCommandValidator : AbstractValidator<DeleteUserCommand>
    {
        public DeleteUserCommandValidator()
        {
            RuleFor(c => c.Id)
                .NotEmpty();
        }
    }
}
