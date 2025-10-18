using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Istiqbal.Application.Featuers.RoomTypes.Commands.UpdateRoomType
{
    public sealed class UpdateRoomTypeCommandValidator : AbstractValidator<UpdateRoomTypeCommand>
    {
        public UpdateRoomTypeCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("Room type ID is required.")
                .Must(id => id != Guid.Empty).WithMessage("Room type ID must be a valid GUID.");

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

            RuleFor(x => x.amenitieIds)
                .NotNull().WithMessage("Amenities IDs list cannot be null.")
                .Must(list => list != null && list.Count > 0)
                .WithMessage("Amenities IDs list must contain at least one ID.");
        }
    }
}
