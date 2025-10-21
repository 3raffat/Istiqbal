using FluentValidation;

namespace Istiqbal.Application.Featuers.Room.Commands.UpdateRoom
{
    public sealed class UpdateRoomCommandValidator : AbstractValidator<UpdateRoomCommand>
    {
        public UpdateRoomCommandValidator() 
        { 
         
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("Room ID is required.");

            RuleFor(x => x.RoomTypeId)
                .NotEmpty().WithMessage("Room Type ID is required.");

            RuleFor(x => x.RoomStatus)
                .IsInEnum()
                .WithMessage("Room Status must be a valid value .");

        }
    }
}
