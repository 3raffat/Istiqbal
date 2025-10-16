using Istiqbal.Api.Extension;
using Istiqbal.Application.Featuers.RoomTypes.Commands.CreateRoomType;
using Istiqbal.Application.Featuers.RoomTypes.Commands.DeleteRoomType;
using Istiqbal.Application.Featuers.RoomTypes.Commands.UpdateRoomType;
using Istiqbal.Application.Featuers.RoomTypes.Queries;
using Istiqbal.Contracts.Requests.RoomTypes;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
using System.Net.Mime;

namespace Istiqbal.Api.Controllers
{

    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/room-types")]
    public class RoomTypeController(ISender _sender): ApiController
    {

        [HttpGet]
     //   [Authorize(Roles =nameof(Role.Admin))]
        [Consumes(MediaTypeNames.Application.Json)]
        [EndpointName("GetAllRoomTypes")]
        [EndpointDescription("Returns all existing room types in the system.")]
        public async Task<IActionResult> Get(CancellationToken cancellationToken)
        {
            var result = await _sender.Send(new GetRoomTypeQuery(), cancellationToken);

            return result.ToActionResult(this, "Room type retrieved successfully");

        }

        [HttpPost]
      //  [Authorize(Roles = nameof(Role.Admin))]
        [Consumes(MediaTypeNames.Application.Json)]
        [EndpointName("create room type")]
        [EndpointDescription("create new room type.")]
        public async Task<IActionResult> Create([FromBody] CreateRoomTypeRequest request, CancellationToken cancellationToken) { 
        
         var result = await _sender.Send(
             new CreateRoomTypeCommand
             (request.Name,
             request.Description,
             request.PricePerNight,
             request.MaxOccupancy)
             , cancellationToken);

            return result.Match(
             response => Ok(response),
             Problem);
        }

        [HttpDelete("{id:guid}")]
    //    [Authorize(Roles = nameof(Role.Admin))]
        [Consumes(MediaTypeNames.Application.Json)]
        [EndpointName("delete room type")]
        [EndpointDescription("delete exist room type.")]
        public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
        {
            var result = await _sender.Send(new DeleteRoomTypeCommand(id), cancellationToken);

            return result.Match(
            response => Ok(response),
            Problem);
        }

        [HttpPut("{id:guid}")]
      //  [Authorize(Roles = nameof(Role.Admin))]
        public async Task<IActionResult> Update(Guid id, UpdateRoomTypeRequest request, CancellationToken cancellationToken)
        {
            var result = await _sender.Send(
                new UpdateRoomTypeCommand(
                    id,
                    request.Name,
                    request.Description,
                    request.PricePerNight,
                    request.MaxOccupancy)
                , cancellationToken);

            return result.Match(
            response => Ok(response),
            Problem);
        }
    }
}
