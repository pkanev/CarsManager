using System.Text.RegularExpressions;
using CarsManager.Application.Common.Constants;
using FluentValidation;

namespace CarsManager.Application.Employees.Commands.CreateEmployee
{
    public class CreateEmployeeCommandValidator: AbstractValidator<CreateEmployeeCommand>
    {public CreateEmployeeCommandValidator()
        {
            RuleFor(e => e.GivenName)
                .MaximumLength(EmployeeConstants.NAME_MAX_LENGTH)
                .Matches(EmployeeConstants.NAME_REGEX, RegexOptions.IgnoreCase)
                .NotEmpty();
            RuleFor(e => e.MiddleName)
                .Matches($"^$|{EmployeeConstants.NAME_REGEX}", RegexOptions.IgnoreCase)
                .MaximumLength(EmployeeConstants.NAME_MAX_LENGTH);
            RuleFor(e => e.Surname)
                .MaximumLength(EmployeeConstants.NAME_MAX_LENGTH)
                .Matches(EmployeeConstants.NAME_REGEX, RegexOptions.IgnoreCase)
                .NotEmpty();
            RuleFor(e => e.Address)
                .MaximumLength(EmployeeConstants.ADDRESS_MAX_LENGTH);
            RuleFor(e => e.Telephone)
                .Matches($"^$|{EmployeeConstants.PHONE_REGEX}");
        }
    }
}
