using Istiqbal.Domain.Common;
using Istiqbal.Domain.Common.Results;
using Istiqbal.Domain.Common.Results.Abstraction;
using Istiqbal.Domain.Guestes.Reservations;
using System.Net.Mail;
using System.Text.RegularExpressions;

namespace Istiqbal.Domain.Guestes
{
    public sealed class Guest : AuditableEntity
    {
        public string FullName { get; private set; } = string.Empty;
        public string Phone { get; private set; } = string.Empty;
        public string Email { get; private set; } = string.Empty;
        private readonly List<Reservation> _reservation = new();
        public IEnumerable<Reservation> Reservation => _reservation.AsReadOnly();
        private Guest() { }
        private Guest(Guid id,string fullName, string phone, string email) :base(id)
        {
            Phone = phone;
            Email = email;
            FullName = fullName;
        }
        public static Result<Guest> Create(Guid id,string fullName, string phone, string email)
        {
            if (id == Guid.Empty)
                return GuestErrors.GuestIdRequired;

            if (string.IsNullOrWhiteSpace(fullName))         
                return GuestErrors.GuestNameRequired;

            if (string.IsNullOrWhiteSpace(phone) || !Regex.IsMatch(phone, @"^\+[1-9]\d{1,3}\d{6,12}$"))
                return GuestErrors.InvalidPhoneNumber;
            
            if (string.IsNullOrWhiteSpace(email))
                return GuestErrors.GuestEmailRequired;

            try
            {
                _ = new MailAddress(email);
            }
            catch
            {
                return GuestErrors.EmailInvalid;
            }


            return new Guest(id, fullName, phone, email);
        }
        public Result<Updated> Update(string fullName, string phone, string email)
        {
            if (string.IsNullOrWhiteSpace(fullName))
                return GuestErrors.GuestNameRequired;

            if (string.IsNullOrWhiteSpace(phone) || !Regex.IsMatch(phone, @"^\+[1-9]\d{1,3}\d{6,12}$"))
                return GuestErrors.InvalidPhoneNumber;

            if (string.IsNullOrWhiteSpace(email))
                return GuestErrors.GuestEmailRequired;
            try
            {
                _ = new MailAddress(email);
            }
            catch
            {
                return GuestErrors.EmailInvalid;
            }

            FullName = fullName;
            Phone = phone;
            Email = email;
            return Result.Updated;
        }   

    }
}
