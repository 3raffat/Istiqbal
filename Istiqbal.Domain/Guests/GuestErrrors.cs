using Istiqbal.Domain.Common.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Istiqbal.Domain.Guests
{
    public static class GuestErrors
    {
 public static Error GuestNameRequired => Error.Validation(
            code: "Guest.GuestName.Required",
            description: "Guest name cannot be empty."
        );
        public static Error GuestPhoneRequired => Error.Validation(
            code: "Guest.GuestPhone.Required",
            description: "Guest phone cannot be empty."
        );
        public static Error GuestEmailRequired => Error.Validation(
            code: "Guest.GuestEmail.Required",
            description: "Guest email cannot be empty."
        );
        public static Error GuestIdRequired => Error.Validation(
            code: "Guest.GuestId.Required",
            description: "Guest ID cannot be empty."
        );  
        public static Error GuestNotFound => Error.NotFound(
            code: "Guest.Guest.NotFound",
            description: "Guest not found."
        );
        public static Error EmailInvalid => Error.Validation(
            code: "Guest.GuestEmail.Invalid",
            description: "Guest email is invalid."
        );
       public static Error InvalidPhoneNumber => Error.Validation(
            code: "Guest.GuestPhone.Invalid",
            description: "Guest phone number is invalid. It should be in the format +1234567890."
        );

    }
}
