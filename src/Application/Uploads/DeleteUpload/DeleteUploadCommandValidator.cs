using FluentValidation;

namespace CarsManager.Application.Uploadds.DeleteUpload
{
    public class DeleteUploadCommandValidator : AbstractValidator<DeleteUploadCommand>
    {
        public DeleteUploadCommandValidator()
        {
            RuleFor(c => c.FileName)
                .NotEmpty();
            RuleFor(c => c.Path)
                .NotEmpty();
        }
    }
}
