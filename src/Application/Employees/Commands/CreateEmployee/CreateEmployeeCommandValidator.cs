using System.Text.RegularExpressions;
using FluentValidation;

namespace CarsManager.Application.Employees.Commands.CreateEmployee
{
    public class CreateEmployeeCommandValidator: AbstractValidator<CreateEmployeeCommand>
    {
        private const string NAME_REGEX = @"^[a-zа-я\s\-]+$";
        private const string PHONE_REGEX = @"^[+]*[(]{0,1}[0-9]{1,4}[)]{0,1}[-\s\./0-9]*$";

        public CreateEmployeeCommandValidator()
        {
            RuleFor(e => e.GivenName)
                .MaximumLength(100)
                .Matches(NAME_REGEX, RegexOptions.IgnoreCase)
                .NotEmpty();
            RuleFor(e => e.Surname)
                .MaximumLength(100)
                .Matches(NAME_REGEX, RegexOptions.IgnoreCase)
                .NotEmpty();
            RuleFor(e => e.Address)
                .Matches(NAME_REGEX, RegexOptions.IgnoreCase)
                .MaximumLength(256);
            RuleFor(e => e.Telephone)
                .Matches(PHONE_REGEX);
        }
    }
}
