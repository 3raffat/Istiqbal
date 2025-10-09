using Istiqbal.Domain.Common;
using Istiqbal.Domain.Common.Results;
using Istiqbal.Domain.Guests.Reservations.Enums;


namespace Istiqbal.Domain.Guests.Reservations.Payments
{
    public sealed class Payment:Entity
    {
        public Guid ReservationId { get; set; }
        public Reservation Reservation { get; set; } = null!;
        public decimal Amount { get; set; }
        public DateTimeOffset DateOfPayment { get; set; }
        public string PaymentMethod { get; set; } = string.Empty;
        public PaymentStatus Status { get; set; } = PaymentStatus.Unpaid;

        private Payment() { }
        public Payment(Guid id,Guid reservationId, decimal amount, DateTimeOffset dateOfPayment, string paymentMethod, PaymentStatus status): base(id)
        {
            ReservationId = reservationId;
            Amount = amount;
            DateOfPayment = dateOfPayment;
            PaymentMethod = paymentMethod;
            Status = status;
        }
        public static Result<Payment> Create(Guid id,Guid reservationId, decimal amount, DateTimeOffset dateOfPayment, string paymentMethod, PaymentStatus status)
        {
            
           
            return new Payment(id, reservationId, amount, dateOfPayment, paymentMethod, status);
        }

    }
   
}
