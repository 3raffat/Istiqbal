using Azure.Core;
using Istiqbal.Application.Featuers.Guest.Commands.CreateGuest;
using Istiqbal.Application.Featuers.Guest.Commands.DeleteGuest;
using Istiqbal.Application.Featuers.Guest.Commands.UpdateGuest;
using Istiqbal.Application.Featuers.Guest.Queries;
using Istiqbal.Contracts.Requests.Guests;
using Istiqbal.Domain.Identity;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading;

namespace Istiqbal.Api.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/guests")]
    public sealed class GuestController(ISender _sender) :ApiController
    {
        [HttpGet]
      //  [Authorize(Roles = $"{nameof(Role.Admin)},{nameof(Role.Receptionist)}")]
        public async Task<IActionResult> Get(CancellationToken cancellationToken)
        {
            var result = await _sender.Send(new GetGuestQuery(), cancellationToken);

            return result.Match(
            response => Ok(response),
            Problem);
        }

        [HttpPost]
      //  [Authorize(Roles = $"{nameof(Role.Admin)},{nameof(Role.Receptionist)}")]
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

        [HttpPut("{id:guid}")]
      //  [Authorize(Roles = nameof(Role.Admin))]
        public async Task<IActionResult> Update(Guid id,UpdateGuestRequest request, CancellationToken cancellationToken)
        {
            var result = await _sender.Send(
                new UpdateGuestCommand(
                    id
                    ,request.fullName
                    , request.phone
                    , request.email)
                , cancellationToken);

            return result.Match(
            response => Ok(response),
            Problem);
        }

        [HttpDelete("{id:guid}")]
      //  [Authorize(Roles = nameof(Role.Admin))]
        public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
        {
            var result = await _sender.Send(
                new DeleteGuestCommand(id)
                , cancellationToken);

            return result.Match(
            response => Ok(response),
            Problem);
        }

    }
}
