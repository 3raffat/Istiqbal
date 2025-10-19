using Azure.Core;
using Istiqbal.Api.Extension;
using Istiqbal.Application.Featuers.Guest.Commands.CreateGuest;
using Istiqbal.Application.Featuers.Guest.Commands.DeleteGuest;
using Istiqbal.Application.Featuers.Guest.Commands.DeleteGuestReservation;
using Istiqbal.Application.Featuers.Guest.Commands.UpdateGuest;
using Istiqbal.Application.Featuers.Guest.Commands.UpdateGuestReservation;
using Istiqbal.Application.Featuers.Guest.Queries;
using Istiqbal.Contracts.Requests.Guests;
using Istiqbal.Contracts.Requests.Reservation;
using Istiqbal.Contracts.Responses;
using Istiqbal.Domain.Auth;
using Istiqbal.Domain.Common.Results;
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
        //[Authorize(Roles = $"{nameof(Role.Admin)},{nameof(Role.Receptionist)}")]
        public async Task<IActionResult> Get(CancellationToken cancellationToken)
        {
            var result = await _sender.Send(new GetGuestQuery(), cancellationToken);

            return result.ToActionResult(this, "Guestes retrieved successfully");
        }

       [HttpPost]
      // [Authorize(Roles = $"{nameof(Role.Admin)},{nameof(Role.Receptionist)}")]
        public async Task<IActionResult> Create(CreateGuestRequest request , CancellationToken cancellationToken)
        {
            var result = await _sender.Send(
                new CreateGuestCommand(
                     request.fullName
                    ,request.phone
                    , request.email)
                , cancellationToken);

            return result.ToActionResult(
            this,
            "Guest created successfully",
            Guest => CreatedAtAction(
                nameof(Create),
                new { id = Guest.Id },
                result.ToApiResponse("Guest created successfully")
            ));
        }

        [HttpPut("{id:guid}")]
        //[Authorize(Roles = nameof(Role.Admin))]
        public async Task<IActionResult> Update(Guid id,UpdateGuestRequest request, CancellationToken cancellationToken)
        {
            var result = await _sender.Send(
                new UpdateGuestCommand(
                    id
                    ,request.fullName
                    , request.phone
                    , request.email)
                , cancellationToken);

            return result.ToActionResult(this, "Guest updated successfully");

        }

        [HttpDelete("{id:guid}")]
        //[Authorize(Roles = nameof(Role.Admin))]
        public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
        {
            var result = await _sender.Send(
                new DeleteGuestCommand(id)
                , cancellationToken);


            if (result.IsSuccess)
            {
                return Ok(new ApiResponse<object>
                {
                    Success = true,
                    Message = "Guest deleted successfully",
                    Timestamp = DateTime.UtcNow
                });
            }

            return result.ToErrorActionResult<Deleted>(this);
        }
        [HttpDelete("{guestId:guid}/reservations/{reservationId:guid}")]
        public async Task<IActionResult> Cancle(Guid guestId,Guid reservationId, CancellationToken cancellationToken)
        {
            var result = await _sender.Send(
                new CancelGuestReservationCommand(guestId,reservationId)
                , cancellationToken);

            return result.ToActionResult(this, "Reservation Cancelled successfully");
        }

        [HttpPut("{guestId:guid}/reservations/{reservationId:guid}")]
        public async Task<IActionResult> Update(Guid guestId, Guid reservationId, UpdateReservationRequest request, CancellationToken cancellationToken)
        {
            var result = await _sender.Send(
                new UpdateGuestReservationCommand(
                    guestId,
                    reservationId,
                    request.roomId,
                    request.CheckInDate,
                    request.CheckOutDate,
                    request.Status)
                , cancellationToken);

            return result.ToActionResult(this, "Reservation guest updated successfully");
        }
    }
}
