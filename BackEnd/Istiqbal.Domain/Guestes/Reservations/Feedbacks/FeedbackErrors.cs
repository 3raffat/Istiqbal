using Istiqbal.Domain.Common.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Istiqbal.Domain.Guestes.Reservations.Feedbacks
{
    public static class FeedbackErrors
    {
        public static Error ReservationIdRequired =>
            Error.Validation(
                code: "Feedback.ReservationId.Required",
                description: "Reservation ID cannot be empty."
            );
        public static Error RatingInvalid =>
            Error.Validation(
                code: "Feedback.Rating.Invalid",
                description: "Rating must be between 1 and 5."
            );
        public static Error CommentsRequired =>
            Error.Validation(
                code: "Feedback.Comments.Required",
                description: "Comments cannot be empty."
            );
        public static Error FeedbackIdRequired =>
            Error.Validation(
                code: "Feedback.FeedbackId.Required",
                description: "Feedback ID cannot be empty."
            );  
        public static Error FeedbackTypeInvalid =>
            Error.Validation(
                code: "Feedback.Type.Invalid",
                description: "Feedback type is invalid."
            );



    }
}
