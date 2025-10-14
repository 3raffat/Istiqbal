using Istiqbal.Application.Featuers.Guest.Commands.CreateGuest;
using Istiqbal.Contracts.Requests.Guests;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Istiqbal.Api.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/guests")]
    public sealed class GuestController(ISender _sender) :ApiController
    {

        [HttpPost]
        public async Task<IActionResult> Create(CreateGuestRequest request , CancellationToken cancellationToken)
        {
            var result = await _sender.Send(
                new CreateGuestCommand(
                     request.fullName
                    ,request.phone
                    , request.email)
                , cancellationToken);

            return result.Match(
            response => Ok(response),
            Problem);
        }
    }
}
