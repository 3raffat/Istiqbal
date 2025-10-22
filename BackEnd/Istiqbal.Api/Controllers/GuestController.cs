using Azure.Core;
using Istiqbal.Api.Extension;
using Istiqbal.Application.Featuers.Guest.Commands.CreateGuest;
using Istiqbal.Application.Featuers.Guest.Commands.DeleteGuest;
using Istiqbal.Application.Featuers.Guest.Commands.DeleteGuestReservation;
using Istiqbal.Application.Featuers.Guest.Commands.UpdateGuest;
using Istiqbal.Application.Featuers.Guest.Commands.UpdateGuestReservation;
using Istiqbal.Application.Featuers.Guest.Dtos;
using Istiqbal.Application.Featuers.Guest.Queries;
using Istiqbal.Application.Featuers.Reservations.Dtos;
using Istiqbal.Contracts.Requests.Guests;
using Istiqbal.Contracts.Requests.Reservation;
using Istiqbal.Contracts.Responses;
using Istiqbal.Domain.Auth;
using Istiqbal.Domain.Common.Results;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System.Threading;
using static Istiqbal.Application.Common.Responses.StandardResponse;

namespace Istiqbal.Api.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/guests")]
    public sealed class GuestController(ISender _sender) : ControllerBase
    {

        [HttpGet]
        [EndpointName("GetGuests")]
        [EndpointDescription("Retrieves a list of all guests including their contact information.")]
        [ProducesResponseType(typeof(StandardSuccessResponse<List<GuestDto>>), StatusCodes.Status200OK)]
        [Produces("application/json")]
        public async Task<IActionResult> Get(CancellationToken cancellationToken)
        {
            var result = await _sender.Send(new GetGuestQuery(), cancellationToken);

            if (result.IsError)
                return result.TopError.ToActionResult();

            return Ok(new StandardSuccessResponse<List<GuestDto>>(
                result.Value,
                StatusCodes.Status200OK,
                "Guests retrieved successfully"));
        }

        [HttpPost]
        [Authorize(Roles = $"{nameof(Role.Admin)},{nameof(Role.Receptionist)}")]
        [EndpointName("CreateGuest")]
        [EndpointDescription("Creates a new guest with full name, phone, and email.")]
        [ProducesResponseType(typeof(StandardSuccessResponse<GuestDto>), StatusCodes.Status201Created)]
        [Produces("application/json")]
        [Consumes("application/json")]
        public async Task<IActionResult> Create([FromBody] CreateGuestRequest request, CancellationToken cancellationToken)
        {
            var result = await _sender.Send(
                new CreateGuestCommand(
                    request.FullName,
                    request.Phone,
                    request.Email),
                cancellationToken);

            if (result.IsError)
                return result.TopError.ToActionResult();

            return StatusCode(
                StatusCodes.Status201Created,
                new StandardSuccessResponse<GuestDto>(
                    result.Value,
                    StatusCodes.Status201Created,
                    "Guest created successfully"));
        }

        [HttpPut("{guestId:guid}")]
        [Authorize(Roles = nameof(Role.Admin))]
        [EndpointName("UpdateGuest")]
        [EndpointDescription("Updates the information of an existing guest by ID.")]
        [ProducesResponseType(typeof(StandardSuccessResponse<GuestDto>), StatusCodes.Status200OK)]
        [Produces("application/json")]
        [Consumes("application/json")]
        public async Task<IActionResult> Update(Guid guestId, [FromBody] UpdateGuestRequest request, CancellationToken cancellationToken)
        {
            var result = await _sender.Send(
                new UpdateGuestCommand(
                    guestId,
                    request.FullName,
                    request.Phone,
                    request.Email),
                cancellationToken);

            if (result.IsError)
                return result.TopError.ToActionResult();

            return Ok(new StandardSuccessResponse<GuestDto>(
                result.Value,
                StatusCodes.Status200OK,
                "Guest updated successfully"));
        }

        [HttpDelete("{guestId:guid}")]
        [Authorize(Roles = nameof(Role.Admin))]
        [EndpointName("DeleteGuest")]
        [EndpointDescription("Deletes a guest by its unique identifier.")]
        [ProducesResponseType(typeof(StandardSuccessResponse<object>), StatusCodes.Status200OK)]
        [Produces("application/json")]
        public async Task<IActionResult> Delete(Guid guestId, CancellationToken cancellationToken)
        {
            var result = await _sender.Send(
                new DeleteGuestCommand(guestId),
                cancellationToken);

            if (result.IsError)
                return result.TopError.ToActionResult();

            return Ok(new StandardSuccessResponse<object>(
                null,
                StatusCodes.Status200OK,
                "Guest deleted successfully"));
        }

        [HttpDelete("{guestId:guid}/reservations/{reservationId:guid}")]
        [Authorize(Roles = $"{nameof(Role.Admin)},{nameof(Role.Receptionist)}")]
        [EndpointName("CancelReservation")]
        [EndpointDescription("Cancels a specific reservation for a guest.")]
        [ProducesResponseType(typeof(StandardSuccessResponse<ReservationDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Produces("application/json")]
        public async Task<IActionResult> CancelReservation(Guid guestId, Guid reservationId, CancellationToken cancellationToken)
        {
            var result = await _sender.Send(
                new CancelGuestReservationCommand(guestId, reservationId),
                cancellationToken);

            if (result.IsError)
                return result.TopError.ToActionResult();

            return Ok(new StandardSuccessResponse<ReservationDto>(
                result.Value,
                StatusCodes.Status200OK,
                "Reservation cancelled successfully"));
        }

        [HttpPut("{guestId:guid}/reservations/{reservationId:guid}")]
        [Authorize(Roles = $"{nameof(Role.Admin)},{nameof(Role.Receptionist)}")]
        [EndpointName("UpdateReservation")]
        [EndpointDescription("Updates a specific reservation for a guest with new room, dates, and status.")]
        [ProducesResponseType(typeof(StandardSuccessResponse<ReservationDto>), StatusCodes.Status200OK)]
        [Produces("application/json")]
        [Consumes("application/json")]
        public async Task<IActionResult> UpdateReservation(Guid guestId, Guid reservationId, [FromBody] UpdateReservationRequest request, CancellationToken cancellationToken)
        {
            var result = await _sender.Send(
                new UpdateGuestReservationCommand(
                    guestId,
                    reservationId,
                    request.RoomId,
                    request.CheckInDate,
                    request.CheckOutDate,
                    request.Status),
                cancellationToken);

            if (result.IsError)
                return result.TopError.ToActionResult();

            return Ok(new StandardSuccessResponse<ReservationDto>(
                result.Value,
                StatusCodes.Status200OK,
                "Reservation updated successfully"));
        }
    }
}