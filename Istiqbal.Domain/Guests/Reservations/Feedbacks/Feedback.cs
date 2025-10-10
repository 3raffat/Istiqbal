using Istiqbal.Domain.Common;
using Istiqbal.Domain.Common.Results;
using Istiqbal.Domain.Guests.Reservations.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Istiqbal.Domain.Guests.Reservations.Feedbacks
{
    public sealed class Feedback : AuditableEntity
    {
        public Guid ReservationId { get; set; }
        public Reservation Reservation { get; set; } = null!;
        public short Rating { get; set; }
        public FeedbackType Type { get; set; }
        public string? Comments { get; set; } 

        private Feedback() { }
        private Feedback(Guid id,Guid reservationId, short rating, FeedbackType type, string? comments):base(id)
        {
            ReservationId = reservationId;
            Rating = rating;
            Type = type;
            Comments = comments;
        }
        public static Result<Feedback> Create(Guid id, Guid reservationId, short rating, FeedbackType type, string comments)
        {
            if (id == Guid.Empty)
                return FeedbackErrors.FeedbackIdRequired;

            if (reservationId == Guid.Empty)
                return FeedbackErrors.ReservationIdRequired;

            if (rating < 1 || rating > 5)
                return FeedbackErrors.RatingInvalid;

            if(!Enum.IsDefined(type))
                return FeedbackErrors.FeedbackTypeInvalid;
            
            return new Feedback(id, reservationId, rating, type, comments);
        }
        public Result<Updated> Update(short rating, FeedbackType type, string comments)
        {
            if (rating < 1 || rating > 5)
                return FeedbackErrors.RatingInvalid;

            if (!Enum.IsDefined(type))
                return FeedbackErrors.FeedbackTypeInvalid;

            Rating = rating;
            Type = type;
            Comments = comments;
            return Result.Updated;
        }
    }
}
