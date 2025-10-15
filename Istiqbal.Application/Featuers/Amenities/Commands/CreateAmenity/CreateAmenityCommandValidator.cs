using FluentValidation;
namespace Istiqbal.Application.Featuers.Amenity.Commands.CreateAmenity
{
    public sealed class CreateAmenityCommandValidator :AbstractValidator<CreateAmenityCommand>
    {
        public CreateAmenityCommandValidator()
        {
            RuleFor(x => x.name)
                .NotEmpty()
                .MaximumLength(50);
        }
    }
}
