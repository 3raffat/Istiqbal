using Istiqbal.Api.Extension;
using Istiqbal.Application.Featuers.Amenity.Dtos;
using Istiqbal.Application.Featuers.Reservations.Commands.CreateReservation;
using Istiqbal.Application.Featuers.Reservations.Dtos;
using Istiqbal.Application.Featuers.Reservations.Queries;
using Istiqbal.Contracts.Requests.Reservation;
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
    [Route("api/v{version:apiVersion}/reservation")]
    public class ReservationController(ISender _sender) :ControllerBase
    {
        [HttpGet]
        [EndpointName("GetReservations")]
        [EndpointDescription("Retrieves a list of all reservations including guest and room details.")]
        [ProducesResponseType(typeof(StandardSuccessResponse<List<ReservationDto>>), StatusCodes.Status200OK)]
        [Produces("application/json")]
        [OutputCache(Duration = 60)]
        public async Task<IActionResult> Get(CancellationToken cancellationToken)
        {
            var result = await _sender.Send(new GetReservationQuery(), cancellationToken);

            if (result.IsError)
                return result.TopError.ToActionResult();

            return Ok(new StandardSuccessResponse<List<ReservationDto>>(
                result.Value,
                StatusCodes.Status200OK,
                "Reservations retrieved successfully"));
        }

        [HttpPost]
        [Authorize(Roles = $"{nameof(Role.Admin)},{nameof(Role.Receptionist)}")]
        [EndpointName("CreateReservation")]
        [EndpointDescription("Creates a new reservation for a guest in a specific room with check-in and check-out dates.")]
        [ProducesResponseType(typeof(StandardSuccessResponse<ReservationDto>), StatusCodes.Status201Created)]
        [Produces("application/json")]
        [Consumes("application/json")]
        public async Task<IActionResult> Create([FromBody] CreateReservationRequest request, CancellationToken cancellationToken)
        {
            var result = await _sender.Send(
                new CreateReservationCommand(
                    request.GuestId,
                    request.RoomId,
                    request.CheckInDate,
                    request.CheckOutDate),
                cancellationToken);

            if (result.IsError)
                return result.TopError.ToActionResult();

            return StatusCode(StatusCodes.Status201Created,
                              new StandardSuccessResponse<ReservationDto>(
                                  result.Value,
                                  StatusCodes.Status201Created,
                                  "Reservation created successfully"));
        }
    }
}
