using FluentValidation;

namespace Istiqbal.Application.Featuers.Room.Commands.UpdateRoom
{
    public sealed class UpdateRoomCommandValidator : AbstractValidator<UpdateRoomCommand>
    {
        public UpdateRoomCommandValidator() 
        { 
         
            RuleFor(x => x.id)
                .NotEmpty().WithMessage("Room ID is required.");

            RuleFor(x => x.roomTypeId)
                .NotEmpty().WithMessage("Room Type ID is required.");

            RuleFor(x => x.amenities)
                .NotNull().WithMessage("Amenities IDs list cannot be null.")
                .Must(list => list != null && list.Count > 0)
                .WithMessage("Amenities IDs list must contain at least one ID.");

            RuleFor(x => x.roomStatus)
                .IsInEnum()
                .WithMessage("Room Status must be a valid value .");


        }
    }
}
