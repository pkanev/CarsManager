using FluentValidation;

namespace CarsManager.Application.Employees.Commands.UpdateEmployee
{
    public class UpdateEmployeeCommandValidator : AbstractValidator<UpdateEmployeeCommand>
    {
        public UpdateEmployeeCommandValidator()
        {
            RuleFor(e => e.GivenName)
                .MaximumLength(100)
                .NotEmpty();
            RuleFor(e => e.Surname)
                .MaximumLength(100)
                .NotEmpty();
            RuleFor(e => e.Address)
                .MaximumLength(256);
            RuleFor(e => e.Telephone)
                .Matches(@"^[+]*[(]{0,1}[0-9]{1,4}[)]{0,1}[-\s\./0-9]*$");
        }
    }
}
