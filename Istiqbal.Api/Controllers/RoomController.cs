using Istiqbal.Application.Featuers.Room.Commands.CreateRoom;
using Istiqbal.Contracts.Requests.Rooms;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Istiqbal.Api.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/rooms")]
    public sealed class RoomController(ISender _sender):ApiController
    {

        //[HttpPost]
        //public async Task<IActionResult> Create(CreateRoomRequest request, CancellationToken cancellationToken)
        //{
        //    var Amenities = request
        //        .Amenities.ConvertAll(a=> new CreateAmenityCommand(a.id,a.name));

        //    var result = await _sender.Send(
        //        new CreateRoomCommand(
        //            request.roomTypeId
        //            , Amenities
        //        )
        //        ,cancellationToken);

        //    return result.Match(
        //       response => Ok(response),
        //       Problem);
        //}
    }
}
