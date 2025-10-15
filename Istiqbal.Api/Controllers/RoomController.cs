using Istiqbal.Api.Extension;
using Istiqbal.Application.Featuers.Room.Commands.CreateRoom;
using Istiqbal.Application.Featuers.Room.Commands.DeleteRoom;
using Istiqbal.Application.Featuers.Room.Commands.UpdateRoom;
using Istiqbal.Application.Featuers.Room.Queries;
using Istiqbal.Contracts.Requests.Rooms;
using Istiqbal.Contracts.Responses;
using Istiqbal.Domain.Common.Results;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Istiqbal.Api.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/rooms")]
    public sealed class RoomController(ISender _sender):ApiController
    {

        [HttpGet]
        public async Task<IActionResult> Get(CancellationToken cancellationToken)
        {
            var result = await _sender.Send(new GetRoomQuery(), cancellationToken);

            return result.ToActionResult(this, "Room retrieved successfully"); 
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateRoomRequest request, CancellationToken cancellationToken)
        {
            var result = await _sender.Send(
                new CreateRoomCommand(
                    request.roomTypeId
                   ,request.AmenitiesIds)
                   ,cancellationToken);

            return result.ToActionResult(
            this,
            "Room created successfully",
            room => CreatedAtAction(
                nameof(Create),
                new { id = room.id },
                result.ToApiResponse("Room created successfully")
            ));
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id,CancellationToken cancellationToken)
        {
            var result = await _sender.Send(new DeleteRoomCommand(id) ,cancellationToken);

            if (result.IsSuccess)
            {
                return Ok(new ApiResponse<object>
                {
                    Success = true,
                    Message = "Room deleted successfully",
                    Timestamp = DateTime.UtcNow
                });
            }

            return result.ToErrorActionResult<Deleted>(this);
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Update(Guid id, updateRoomRequest request, CancellationToken cancellationToken)
        {
            var result = await _sender.Send(
                new UpdateRoomCommand(
                    id
                    ,request.roomTypeId
                    ,request.amenitiesId
                    ,request.roomStatus
                    )
                , cancellationToken);

            return result.ToActionResult(this, "Room updated successfully");
        }
    }
}
