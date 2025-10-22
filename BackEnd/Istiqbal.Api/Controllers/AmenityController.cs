using Istiqbal.Api.Extension;
using Istiqbal.Application.Featuers.Amenities.Commands.DeleteAmenity;
using Istiqbal.Application.Featuers.Amenities.Queries;
using Istiqbal.Application.Featuers.Amenity.Commands.CreateAmenity;
using Istiqbal.Application.Featuers.Amenity.Commands.UpdateAmenity;
using Istiqbal.Application.Featuers.Amenity.Dtos;
using Istiqbal.Contracts.Requests.Amenity;
using Istiqbal.Domain.Auth;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OutputCaching;
using static Istiqbal.Application.Common.Responses.StandardResponse;

namespace Istiqbal.Api.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/amenitis")]
    public class AmenityController(ISender _sender) : ControllerBase
    {

        [HttpGet]
        [EndpointName("GetAmenities")]
        [EndpointDescription("Retrieves a list of all amenities available.")]
        [ProducesResponseType(typeof(StandardSuccessResponse<List<AmenityDto>>), StatusCodes.Status200OK)]
        [Produces("application/json")]
        [OutputCache(Duration = 60)]
        public async Task<IActionResult> Get(CancellationToken cancellationToken)
        {
            var result = await _sender.Send(new GetAmenityQuery(), cancellationToken);

            if (result.IsError)
                return result.TopError.ToActionResult();

            return Ok(new StandardSuccessResponse<List<AmenityDto>>(
                result.Value,
                StatusCodes.Status200OK,
                "Amenities retrieved successfully"));
        }

        [HttpPost]
        [Authorize(Roles = $"{nameof(Role.Admin)},{nameof(Role.Receptionist)}")]
        [EndpointName("CreateAmenity")]
        [EndpointDescription("Creates a new amenity with the specified name.")]
        [ProducesResponseType(typeof(StandardSuccessResponse<AmenityDto>), StatusCodes.Status201Created)]
        [Produces("application/json")]
        [Consumes("application/json")]
        public async Task<IActionResult> Create([FromBody] CreateAmenityRequest request, CancellationToken cancellationToken)
        {
            var result = await _sender.Send(
                new CreateAmenityCommand(request.Name),
                cancellationToken);

            if (result.IsError)
                return result.TopError.ToActionResult();

            return StatusCode(StatusCodes.Status201Created,
                              new StandardSuccessResponse<AmenityDto>(
                                  result.Value,
                                  StatusCodes.Status201Created,
                                  "Amenity created successfully"));
        }

        [HttpPut("{amenityId:guid}")]
        [Authorize(Roles = nameof(Role.Admin))]
        [EndpointName("UpdateAmenity")]
        [EndpointDescription("Updates the name of an existing amenity by its ID.")]
        [ProducesResponseType(typeof(StandardSuccessResponse<AmenityDto>), StatusCodes.Status200OK)]
        [Produces("application/json")]
        [Consumes("application/json")]
        public async Task<IActionResult> Update(Guid amenityId, [FromBody] UpdateAmenityRequest request, CancellationToken cancellationToken)
        {
            var result = await _sender.Send(
                new UpdateAmenityCommand(amenityId, request.Name),
                cancellationToken);

            if (result.IsError)
                return result.TopError.ToActionResult();

            return Ok(new StandardSuccessResponse<AmenityDto>(
                result.Value,
                StatusCodes.Status200OK,
                "Amenity updated successfully"));
        }

        [HttpDelete("{amenityId:guid}")]
        [Authorize(Roles = nameof(Role.Admin))]
        [EndpointName("DeleteAmenity")]
        [EndpointDescription("Deletes an amenity by its unique identifier.")]
        [ProducesResponseType(typeof(StandardSuccessResponse<object>), StatusCodes.Status200OK)]
        [Produces("application/json")]
        public async Task<IActionResult> Delete(Guid amenityId, CancellationToken cancellationToken)
        {
            var result = await _sender.Send(
                new DeleteAmenityCommand(amenityId),
                cancellationToken);

            if (result.IsError)
                return result.TopError.ToActionResult();

            return Ok(new StandardSuccessResponse<object>(
                null,
                StatusCodes.Status200OK,
                "Amenity deleted successfully"));
        }
    }
}
