using Istiqbal.Domain.Common.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Istiqbal.Domain.Guests.Reservations
{
    public static class ReservationErrors
    {
        public static Error ReservationIdRequired=> Error.Validation(
            code: "Reservation.ReservationId.Required",
            description: "Reservation ID cannot be empty."
        );
        public static Error ReservationGuestIdRequired=> Error.Validation(
            code: "Reservation.GuestId.Required",
            description: "Guest ID cannot be empty."
        );
        public static Error ReservationRoomIdRequired=> Error.Validation(
            code: "Reservation.RoomId.Required",
            description: "Room ID cannot be empty."
        ); 
        public static Error ReservationCheckOutDateInvalid => Error.Validation(
            code: "Reservation.CheckOutDate.Invalid",
            description: "Check-out date must be after check-in date."
        );
        public static Error ReservationCheckInDateInvalid => Error.Validation(
            code: "Reservation.CheckInDate.Invalid",
            description: "Check-in date must be before check-out date."
        );
        public static Error ReservationAmountInvalid => Error.Validation(
            code: "Reservation.Amount.Invalid",
            description: "Amount must be greater than zero."
        );
        public static Error ReservationNumberOfGuestsInvalid => Error.Validation(
            code: "Reservation.NumberOfGuests.Invalid",
            description: "Number of guests must be greater than zero."
        );
        public static Error ReservationStatusInvalid => Error.Validation(
            code: "Reservation.Status.Invalid",
            description: "Reservation status is invalid."
        );
        public static Error ReservationFeedbackRequired => Error.Validation(
            code: "Reservation.Feedback.Required",
            description: "Feedback cannot be null."
        );
        public static Error ReservationFeedbackNotFound => Error.NotFound(
            code: "Reservation.Feedback.NotFound",
            description: "Feedback not found in this reservation."
        );
        public static Error ReservationFeedbackIsExist => Error.Conflict(
            code: "Reservation.Feedback.IsExist",
            description: "This reservation already has feedback."
        );  
    }
}
