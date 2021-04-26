using FluentValidation;

namespace CarsManager.Application.Users.Queries.ChangeAdminStatus
{
    public class ChangeAdminStatusCommandValidator : AbstractValidator<ChangeAdminStatusCommand>
    {
        public ChangeAdminStatusCommandValidator()
        {
            RuleFor(c => c.UserId)
                .NotEmpty();
        }
    }
}
