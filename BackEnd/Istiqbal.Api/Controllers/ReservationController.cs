using Istiqbal.Api.Extension;
using Istiqbal.Application.Featuers.Reservations.Commands.CreateReservation;
using Istiqbal.Application.Featuers.Reservations.Queries;
using Istiqbal.Contracts.Requests.Reservation;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Istiqbal.Api.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/reservation")]
    public class ReservationController(ISender _sender) :ApiController
    {
        [HttpGet]
        public async Task<IActionResult> Get(CancellationToken cancellationToken)
        {
            var result = await _sender.Send(new GetReservationQuery(), cancellationToken);

            return result.ToActionResult(this, "Reservation retrieved successfully");
        }
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateReservationRequest request ,CancellationToken cancellationToken)
        {
            var result = await _sender.Send(new 
                CreateReservationCommand(
                    request.GuestId,
                    request.roomId,
                    request.CheckInDate,
                    request.CheckOutDate),
                    cancellationToken);

            return result.ToActionResult(this, "Reservation created successfully");

        }

    }
}
