using Istiqbal.Api.Extension;
using Istiqbal.Application.Featuers.Amenities.Commands.DeleteAmenity;
using Istiqbal.Application.Featuers.Amenities.Queries;
using Istiqbal.Application.Featuers.Amenity.Commands.CreateAmenity;
using Istiqbal.Application.Featuers.Amenity.Commands.UpdateAmenity;
using Istiqbal.Application.Featuers.Guest.Commands.DeleteGuest;
using Istiqbal.Application.Featuers.Guest.Commands.UpdateGuest;
using Istiqbal.Application.Featuers.Guest.Queries;
using Istiqbal.Contracts.Requests.Amenity;
using Istiqbal.Contracts.Requests.Guests;
using Istiqbal.Contracts.Responses;
using Istiqbal.Domain.Common.Results;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Istiqbal.Api.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/amenitis")]
    public class AmenityController(ISender _sender) : ApiController
    {

        [HttpGet]
        public async Task<IActionResult> Get(CancellationToken cancellationToken)
        {
            var result = await _sender.Send(new GetAmenityQuery(), cancellationToken);

            return result.ToActionResult(this, "Amenitits retrieved successfully");
        }


        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateAmenityRequest request, CancellationToken cancellationToken)
        {
            var result = await _sender.Send(
                 new CreateAmenityCommand(
                     request.name)
                 , cancellationToken);

            return result.Match(
              response => Ok(response),
              Problem);
        }
        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Update(Guid id, UpdateAmenityRequest request, CancellationToken cancellationToken)
        {
            var result = await _sender.Send(
                new UpdateAmenityCommand(
                    id
                    , request.name)
                , cancellationToken);

            return result.ToActionResult(this, "Amenity updated successfully");

        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id ,CancellationToken cancellationToken)
        {
            var result = await _sender.Send(
                new DeleteAmenityCommand(id)
                , cancellationToken);

            if (result.IsSuccess)
            {
                return Ok(new ApiResponse<object>
                {
                    Success = true,
                    Message = "Amenity deleted successfully",
                    Timestamp = DateTime.UtcNow
                });
            }

            return result.ToErrorActionResult<Deleted>(this);
        }
    }
}
