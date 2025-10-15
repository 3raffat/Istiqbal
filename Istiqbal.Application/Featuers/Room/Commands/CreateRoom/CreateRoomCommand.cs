using Istiqbal.Application.Featuers.Room.Dtos;
using Istiqbal.Domain.Amenities;
using Istiqbal.Domain.Common.Results;
using MediatR;


namespace Istiqbal.Application.Featuers.Room.Commands.CreateRoom
{
    public sealed record CreateRoomCommand(Guid roomTypeId,List<Amenity> Amenities):IRequest<Result<RoomDto>>;

}
