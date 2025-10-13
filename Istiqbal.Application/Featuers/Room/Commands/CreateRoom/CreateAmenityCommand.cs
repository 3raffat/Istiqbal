using Istiqbal.Application.Featuers.RoomTypes.Dtos;
using MediatR;
namespace Istiqbal.Application.Featuers.Room.Commands.CreateRoom
{
    public sealed record CreateAmenityCommand(Guid id, string name):IRequest<AmenityDto>;
    
}
