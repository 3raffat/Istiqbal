using FluentValidation;
namespace Istiqbal.Application.Featuers.RoomTypes.Commands.CreateRoomType
{
    public sealed class CreateRoomTypeCommandValidator:AbstractValidator<CreateRoomTypeCommand>
    {
        public CreateRoomTypeCommandValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Room type name is required.")
                .MaximumLength(50).WithMessage("Room type name must not exceed 50 characters.");

            RuleFor(x => x.Description)
                .NotEmpty().WithMessage("Room type description is required.")
                .MaximumLength(200).WithMessage("Room type description must not exceed 200 characters.");

            RuleFor(x => x.PricePerNight)
                .GreaterThan(0).WithMessage("Price per night must be greater than zero.");

            RuleFor(x => x.MaxOccupancy)
                .GreaterThan(0).WithMessage("Max occupancy must be greater than zero.");
        }
    }
}
