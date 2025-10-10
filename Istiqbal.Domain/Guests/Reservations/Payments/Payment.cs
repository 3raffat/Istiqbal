using Istiqbal.Domain.Common;
using Istiqbal.Domain.Common.Results;
using Istiqbal.Domain.Guests.Reservations.Enums;


namespace Istiqbal.Domain.Guests.Reservations.Payments
{
    public sealed class Payment:Entity
    {
        public Guid ReservationId { get; private set; }
        public Reservation Reservation { get; set; } = null!;
        public decimal Amount { get;}
        public DateTimeOffset DateOfPayment { get; private set; }
        public PaymentMethod PaymentMethod { get; private set; } = PaymentMethod.Cash;
        public PaymentStatus Status { get; private set; } = PaymentStatus.Unpaid;

        private Payment() { }
        public Payment(Guid id,Guid reservationId, DateTimeOffset dateOfPayment, PaymentMethod paymentMethod, PaymentStatus status): base(id)
        {
            ReservationId = reservationId;
            DateOfPayment = dateOfPayment;
            PaymentMethod = paymentMethod;
            Status = status;
        }
       public static Result<Payment> Create(Guid id,Guid reservationId , PaymentMethod paymentMethod, PaymentStatus status)
        {
            if (id == Guid.Empty)
                return PaymentErrors.PaymentIdRequired;

            if (reservationId == Guid.Empty)
                return PaymentErrors.PaymentReservationIdRequired;

           

            if (!Enum.IsDefined(paymentMethod))
                return PaymentErrors.PaymentMethodInvalid;

            if (!Enum.IsDefined(status))
                return PaymentErrors.PaymentStatusInvalid;

            return new Payment(id,reservationId, DateTimeOffset.UtcNow, paymentMethod, status);
        }
        public Result<Updated> Update( PaymentMethod paymentMethod, PaymentStatus status)
        {
            

            if (!Enum.IsDefined(paymentMethod))
                return PaymentErrors.PaymentMethodInvalid;

            if (!Enum.IsDefined(status))
                return PaymentErrors.PaymentStatusInvalid;

            PaymentMethod = paymentMethod;
            Status = status;
            return Result.Updated;
        }

    }
   
}
