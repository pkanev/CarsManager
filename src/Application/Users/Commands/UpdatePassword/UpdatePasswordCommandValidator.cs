using FluentValidation;

namespace CarsManager.Application.Users.Commands.UpdatePassword
{
    public class UpdatePasswordCommandValidator : AbstractValidator<UpdatePasswordCommand>
    {
        public UpdatePasswordCommandValidator()
        {
            RuleFor(c => c.Id)
                .NotEmpty();
            RuleFor(c => c.OldPassword)
                .NotEmpty();
            RuleFor(c => c.NewPassword)
                .NotEmpty();
        }
    }
}
