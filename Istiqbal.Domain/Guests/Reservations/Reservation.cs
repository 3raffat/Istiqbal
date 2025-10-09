using Istiqbal.Domain.Common;
using Istiqbal.Domain.Common.Results;
using Istiqbal.Domain.Guests.Reservations.Enums;
using Istiqbal.Domain.Guests.Reservations.Feedbacks;
using Istiqbal.Domain.Rooms;


namespace Istiqbal.Domain.Guests.Reservations
{
    public sealed class Reservation : AuditableEntity
    {
        public Guid GuestId { get; set; }
        public Guest Guest { get; set; } = null!;
        public Guid RoomId { get; set; }
        public Room Room { get; set; } = null!;
        public DateTimeOffset CheckInDate { get; set; }
        public DateTimeOffset CheckOutDate { get; set; }
        public decimal Amount => Room.Type.PricePerNight * (decimal)Math.Max(1, Math.Ceiling((CheckOutDate - CheckInDate).TotalDays));
        public short NumberOfGuests { get; set; }
        public ReservationStatus Status { get; set; } = ReservationStatus.Pending;

        private readonly List<Feedback> _feedbacks = new();
        public IReadOnlyCollection<Feedback> Feedbacks => _feedbacks.AsReadOnly();
        private decimal MaxOccupancy => Room?.Type?.MaxOccupancy ?? 1;
        private Reservation() { }
        private Reservation(Guid id, Guid guestId, Guid roomId, DateTimeOffset checkInDate, DateTimeOffset checkOutDate, short numberOfGuests, ReservationStatus status) : base(id)
        {
            GuestId = guestId;
            RoomId = roomId;
            CheckInDate = checkInDate;
            CheckOutDate = checkOutDate;
            NumberOfGuests = numberOfGuests;
            Status = status;
        }
        public static Result<Reservation> Create(Guid id, Guid guestId, Guid roomId, DateTimeOffset checkInDate, DateTimeOffset checkOutDate, short numberOfGuests, ReservationStatus status)
        {
            if (id == Guid.Empty)
                return ReservationErrors.ReservationIdRequired;

            if (guestId == Guid.Empty)
                return ReservationErrors.ReservationGuestIdRequired;

            if (roomId == Guid.Empty)
                return ReservationErrors.ReservationRoomIdRequired;

            if (checkInDate < DateTimeOffset.UtcNow)
                return ReservationErrors.ReservationCheckInDateInvalid;

            if (checkOutDate < checkInDate)
                return ReservationErrors.ReservationCheckOutDateInvalid;

            if (!Enum.IsDefined(status))
                return ReservationErrors.ReservationStatusInvalid;

            return new Reservation(id, guestId, roomId, checkInDate, checkOutDate, numberOfGuests, status);
        }
        public Result<Updated> Update(DateTimeOffset checkInDate, DateTimeOffset checkOutDate, short numberOfGuests, ReservationStatus status)
        {
            if (checkInDate.Date < DateTimeOffset.UtcNow.Date)
                return ReservationErrors.ReservationCheckInDateInvalid;

            if (checkOutDate < checkInDate)
                return ReservationErrors.ReservationCheckOutDateInvalid;

            if (numberOfGuests > MaxOccupancy)
                return ReservationErrors.ReservationNumberOfGuestsInvalid;

            if (!Enum.IsDefined(status))
                return ReservationErrors.ReservationStatusInvalid;

            CheckInDate = checkInDate;
            CheckOutDate = checkOutDate;
            NumberOfGuests = numberOfGuests;
            Status = status;
            return Result.Updated;
        }
        public Result<Updated> AddFeedback(Feedback feedback)
        {
            if (feedback == null)
                return ReservationErrors.ReservationFeedbackRequired;

            if(_feedbacks.Contains(feedback))
                return ReservationErrors.ReservationFeedbackIsExist;

            _feedbacks.Add(feedback);
            return Result.Updated;
        }
        public Result<Deleted> RemoveFeedback(Feedback feedback)
        {
            if (feedback == null)
                return ReservationErrors.ReservationFeedbackRequired;

            if (!_feedbacks.Contains(feedback))
                return ReservationErrors.ReservationFeedbackNotFound;

            _feedbacks.Remove(feedback);
            return Result.Deleted;
        }
    }
}