using Istiqbal.Api.Extension;
using Istiqbal.Application.Featuers.Room.Dtos;
using Istiqbal.Application.Featuers.RoomType.Dtos;
using Istiqbal.Application.Featuers.RoomTypes.Commands.CreateRoomType;
using Istiqbal.Application.Featuers.RoomTypes.Commands.DeleteRoomType;
using Istiqbal.Application.Featuers.RoomTypes.Commands.UpdateRoomType;
using Istiqbal.Application.Featuers.RoomTypes.Queries;
using Istiqbal.Contracts.Requests.RoomTypes;
using Istiqbal.Domain.Auth;
using Istiqbal.Domain.RoomTypes;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.AspNetCore.OutputCaching;
using System.Net.Mime;
using static Istiqbal.Application.Common.Responses.StandardResponse;

namespace Istiqbal.Api.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/room-types")]
    public class RoomTypeController(ISender _sender) : ControllerBase
    {
        [HttpGet]
        [EndpointName("GetRoomTypes")]
        [EndpointDescription("Retrieves a list of all room types with their details.")]
        [ProducesResponseType(typeof(StandardSuccessResponse<List<RoomTypeDto>>), StatusCodes.Status200OK)]
        [Produces("application/json")]
        [OutputCache(Duration = 60)]
        public async Task<IActionResult> Get(CancellationToken cancellationToken)
        {
            var result = await _sender.Send(new GetRoomTypeQuery(), cancellationToken);

            if (result.IsError)
                return result.TopError.ToActionResult();
            Console.WriteLine("viset controller ");
            return Ok(new StandardSuccessResponse<List<RoomTypeDto>>(result.Value,
                                                                     StatusCodes.Status200OK,
                                                                     "Room types retrieved successfully"));
        }

        [HttpPost]
        [Authorize(Roles = nameof(Role.Admin))]
        [EndpointName("CreateRoomType")]
        [EndpointDescription("Creates a new room type with name, description, price, occupancy, and amenities.")]
        [ProducesResponseType(typeof(StandardSuccessResponse<RoomTypeDto>), StatusCodes.Status201Created)]
        [Produces("application/json")]
        [Consumes("application/json")]
        public async Task<IActionResult> Create([FromBody] CreateRoomTypeRequest request, CancellationToken cancellationToken)
        {
            var result = await _sender.Send(
                new CreateRoomTypeCommand(
                    request.Name,
                    request.Description,
                    request.PricePerNight,
                    request.MaxOccupancy,
                    request.AmenitieIds),
                cancellationToken);

            if (result.IsError)
                return result.TopError.ToActionResult();

            return StatusCode(StatusCodes.Status201Created,
                new StandardSuccessResponse<RoomTypeDto>(result.Value,
                                                          StatusCodes.Status201Created,
                                                          "Room type created successfully"));
        }

        [HttpPut("{roomTypeId:guid}")]
        [Authorize(Roles = nameof(Role.Admin))]
        [EndpointName("UpdateRoomType")]
        [EndpointDescription("Updates an existing room type by ID including name, description, price, occupancy, and amenities.")]
        [ProducesResponseType(typeof(StandardSuccessResponse<RoomTypeDto>), StatusCodes.Status200OK)]
        [Produces("application/json")]
        [Consumes("application/json")]
        public async Task<IActionResult> Update(Guid roomTypeId, [FromBody] UpdateRoomTypeRequest request, CancellationToken cancellationToken)
        {
            var result = await _sender.Send(
                new UpdateRoomTypeCommand(
                    roomTypeId,
                    request.Name,
                    request.Description,
                    request.PricePerNight,
                    request.MaxOccupancy,
                    request.AmenitieIds),
                cancellationToken);

            if (result.IsError)
                return result.TopError.ToActionResult();

            return Ok(new StandardSuccessResponse<RoomTypeDto>(result.Value,
                                                               StatusCodes.Status200OK,
                                                               "Room type updated successfully"));
        }

        [HttpDelete("{roomTypeId:guid}")]
        [Authorize(Roles = nameof(Role.Admin))]
        [EndpointName("DeleteRoomType")]
        [EndpointDescription("Deletes an existing room type by its unique identifier.")]
        [ProducesResponseType(typeof(StandardSuccessResponse<object>), StatusCodes.Status200OK)]
        [Produces("application/json")]
        public async Task<IActionResult> Delete(Guid roomTypeId, CancellationToken cancellationToken)
        {
            var result = await _sender.Send(
                new DeleteRoomTypeCommand(roomTypeId),
                cancellationToken);

            if (result.IsError)
                return result.TopError.ToActionResult();

            return Ok(new StandardSuccessResponse<object>(null,
                                                          StatusCodes.Status200OK,
                                                          "Room type deleted successfully"));
        }
    }
}