using Istiqbal.Api.Extension;
using Istiqbal.Application.Featuers.Amenity.Dtos;
using Istiqbal.Application.Featuers.Room.Commands.CreateRoom;
using Istiqbal.Application.Featuers.Room.Commands.DeleteRoom;
using Istiqbal.Application.Featuers.Room.Commands.UpdateRoom;
using Istiqbal.Application.Featuers.Room.Dtos;
using Istiqbal.Application.Featuers.Room.Queries;
using Istiqbal.Contracts.Requests.Rooms;
using Istiqbal.Contracts.Responses;
using Istiqbal.Domain.Auth;
using Istiqbal.Domain.Common.Results;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using static Istiqbal.Application.Common.Responses.StandardResponse;

namespace Istiqbal.Api.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/rooms")]
    public sealed class RoomController(ISender _sender) : ControllerBase
    {

        [HttpGet]
        [Authorize(Roles = $"{nameof(Role.Admin)},{nameof(Role.Receptionist)}")]
        [EndpointName("GetRooms")]
        [EndpointDescription("Retrieves a list of all rooms with their type and status.")]
        [ProducesResponseType(typeof(StandardSuccessResponse<List<RoomDto>>), StatusCodes.Status200OK)]
        [Produces("application/json")]
        public async Task<IActionResult> Get(CancellationToken cancellationToken)
        {
            var result = await _sender.Send(new GetRoomQuery(), cancellationToken);

            if (result.IsError)
                return result.TopError.ToActionResult();

            return Ok(new StandardSuccessResponse<List<RoomDto>>(result.Value,
                                                                 StatusCodes.Status200OK,
                                                                 "Rooms retrieved successfully"));
        }

        [HttpPost]
        [Authorize(Roles = nameof(Role.Admin))]
        [EndpointName("CreateRoom")]
        [EndpointDescription("Creates a new room with the specified room type and status.")]
        [ProducesResponseType(typeof(StandardSuccessResponse<RoomDto>), StatusCodes.Status201Created)]
        [Produces("application/json")]
        [Consumes("application/json")]
        public async Task<IActionResult> Create([FromBody] CreateRoomRequest request, CancellationToken cancellationToken)
        {
            var result = await _sender.Send(
                new CreateRoomCommand(
                    request.RoomTypeId,
                    request.RoomStatus),
                cancellationToken);

            if (result.IsError)
                return result.TopError.ToActionResult();

            return StatusCode(StatusCodes.Status201Created,
                              new StandardSuccessResponse<RoomDto>(result.Value,
                                                                   StatusCodes.Status201Created,
                                                                   "Room created successfully"));
        }

        [HttpPut("{roomId:guid}")]
        [Authorize(Roles = nameof(Role.Admin))]
        [EndpointName("UpdateRoom")]
        [EndpointDescription("Updates an existing room's type and status by its ID.")]
        [ProducesResponseType(typeof(StandardSuccessResponse<RoomDto>), StatusCodes.Status200OK)]
        [Produces("application/json")]
        [Consumes("application/json")]
        public async Task<IActionResult> Update(Guid roomId, [FromBody] UpdateRoomRequest request, CancellationToken cancellationToken)
        {
            var result = await _sender.Send(
                new UpdateRoomCommand(
                    roomId,
                    request.RoomTypeId,
                    request.RoomStatus),
                cancellationToken);

            if (result.IsError)
                return result.TopError.ToActionResult();

            return Ok(new StandardSuccessResponse<RoomDto>(result.Value,
                                                           StatusCodes.Status200OK,
                                                           "Room updated successfully"));
        }

        [HttpDelete("{roomId:guid}")]
        [Authorize(Roles = nameof(Role.Admin))]
        [EndpointName("DeleteRoom")]
        [EndpointDescription("Deletes a room by its unique identifier.")]
        [ProducesResponseType(typeof(StandardSuccessResponse<object>), StatusCodes.Status200OK)]
        [Produces("application/json")]
        public async Task<IActionResult> Delete(Guid roomId, CancellationToken cancellationToken)
        {
            var result = await _sender.Send(
                new DeleteRoomCommand(roomId),
                cancellationToken);

            if (result.IsError)
                return result.TopError.ToActionResult();

            return Ok(new StandardSuccessResponse<object>(null,
                                                          StatusCodes.Status200OK,
                                                          "Room deleted successfully"));
        }
    }
}