using FluentValidation;
namespace Istiqbal.Application.Featuers.Amenity.Commands.CreateAmenity
{
    public sealed class CreateAmenityCommandValidator :AbstractValidator<CreateAmenityCommand>
    {
        public CreateAmenityCommandValidator()
        {
            RuleFor(x => x.Name)
           .NotEmpty().WithMessage("The Name field cannot be empty.")
           .MaximumLength(50).WithMessage("The Name field must not exceed 50 characters.");
        }
    }
}
