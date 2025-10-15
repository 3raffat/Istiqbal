using Istiqbal.Domain.Common.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Istiqbal.Domain.Guestes.Reservations.Payments
{
    public static class PaymentErrors
    {
         
        public static Error PaymentIdRequired=> 
            Error.Validation(
                code: "Payment.Id.Required",
                description: "Payment Id is required.");

        public static Error PaymentReservationIdRequired =>
            Error.Validation(
                code: "Payment.ReservationId.Required",
                description: "Payment Reservation Id is required.");

        public static Error PaymentStatusInvalid =>
            Error.Validation(
                code: "Payment.Status.Invalid",
                description: "Payment Status is invalid.");

        public static Error PaymentMethodInvalid =>
            Error.Validation(
                code: "Payment.Method.Invalid",
                description: "Payment Method is invalid."); 
    }
}
