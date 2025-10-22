using Istiqbal.Domain.Common;
using Istiqbal.Domain.Common.Results;
using Istiqbal.Domain.Guestes.Enums;
using Istiqbal.Domain.Guestes.Reservations.Payments;
using Istiqbal.Domain.RoomTypes.Rooms;
using static Istiqbal.Contracts.Requests.Reservation.CreateReservationRequest;


namespace Istiqbal.Domain.Guestes.Reservations
{
    public sealed class Reservation : AuditableEntity
    {
        public Guid GuestId { get; set; }
        public Guest Guest { get; set; } = null!;
        public Guid RoomId { get; set; }
        public Room Room { get; set; } = null!;
        public DateOnly CheckInDate { get; set; }
        public DateOnly CheckOutDate { get; set; }
        public decimal Amount { get; private set; }
        public ReservationStatus Status { get; set; } = ReservationStatus.Confirmed;
        public Payment Payment { get; set; }
        private Reservation() { }
        private Reservation(Guid id, Guid guestId, Guid roomId, DateOnly checkInDate, DateOnly checkOutDate, int numberOfDays, decimal pricePerNight) : base(id)
        {
            GuestId = guestId;
            RoomId = roomId;
            CheckInDate = checkInDate;
            CheckOutDate = checkOutDate;
            Amount = numberOfDays * pricePerNight ;

        }
        public static Result<Reservation> Create(Guid id, Guid guestId, Guid roomId, DateOnly checkInDate, DateOnly checkOutDate, int numberOfDays,  decimal pricePerNight)
        {
            var today = DateOnly.FromDateTime(DateTime.Now);

            if (id == Guid.Empty)
                return ReservationErrors.ReservationIdRequired;

            if (guestId == Guid.Empty)
                return ReservationErrors.ReservationGuestIdRequired;

            if (roomId == Guid.Empty)
                return ReservationErrors.ReservationRoomIdRequired;

            if (checkInDate < today)
                return ReservationErrors.ReservationCheckInDateInvalid;

            if (checkOutDate <= checkInDate)
                return ReservationErrors.ReservationCheckOutDateInvalid;

            return new Reservation(id, guestId, roomId, checkInDate, checkOutDate, numberOfDays,pricePerNight);
        }
        public Result<Updated> Update(DateOnly checkInDate, DateOnly checkOutDate, Guid roomId, ReservationStatus status)
        {
            var today = DateOnly.FromDateTime(DateTime.Now);

            if (checkInDate < today)
                return ReservationErrors.ReservationCheckInDateInvalid;

            if (checkOutDate <= checkInDate)
                return ReservationErrors.ReservationCheckOutDateInvalid;

            if (!Enum.IsDefined(status))
                return ReservationErrors.ReservationStatusInvalid;

            CheckInDate = checkInDate;
            CheckOutDate = checkOutDate;
            Status = status;
            RoomId = roomId;
            return Result.Updated;
        }
        public Result<Deleted> Cancel()
        {
            if (Status == ReservationStatus.Cancelled)
                return ReservationErrors.AlreadyCancelled;

            Status = ReservationStatus.Cancelled;
            return Result.Deleted;
        }
    }
}