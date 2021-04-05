using FluentValidation;

namespace CarsManager.Application.Uploadds.CreateUpload
{
    public class CreateUploadCommandValidator : AbstractValidator<CreateUploadCommand>
    {
        public CreateUploadCommandValidator()
        {
            RuleFor(c => c.File)
                .NotNull();
            RuleFor(c => c.Path)
                .NotEmpty();
        }
    }
}
