using Istiqbal.Application.Featuers.Amenity.Commands.CreateAmenity;
using Istiqbal.Contracts.Requests.Amenity;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Istiqbal.Api.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/amenitis")]
    public class AmenityController(ISender _sender):ApiController
    {
        [HttpPost]
        public async Task<IActionResult> Create([FromBody]CreateAmenityRequest request,CancellationToken cancellationToken)
        {
           var result =  await _sender.Send(
                new CreateAmenityCommand(
                    request.name)
                ,cancellationToken);

            return result.Match(
              response => Ok(response),
              Problem);
        }
    }
}
