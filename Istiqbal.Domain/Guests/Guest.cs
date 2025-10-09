using Istiqbal.Domain.Common;
using Istiqbal.Domain.Common.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Istiqbal.Domain.Guests
{
    public sealed class Guest : AuditableEntity
    {
        public string Name { get; private set; } = string.Empty;
        public string Phone { get; private set; } = string.Empty;
        public string Email { get; private set; } = string.Empty;

        private Guest() { }
        private Guest(Guid id,string name, string phone, string email) :base(id)
        {
            Name = name;
            Phone = phone;
            Email = email;
        }
        public static Result<Guest> Create(Guid id,string name, string phone, string email)
        {
            if (id == Guid.Empty)
                return GuestErrors.GuestIdRequired;

            if (string.IsNullOrWhiteSpace(name))         
                return GuestErrors.GuestNameRequired;

            if (string.IsNullOrWhiteSpace(phone) || !Regex.IsMatch(phone, @"^\+[1-9]\d{4,14}$"))
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


            return new Guest(id, name, phone, email);
        }
        public Result<Updated> Update(string name, string phone, string email)
        {
            if (string.IsNullOrWhiteSpace(name))
                return GuestErrors.GuestNameRequired;

            if (string.IsNullOrWhiteSpace(phone) || !Regex.IsMatch(phone, @"^\+[1-9]\d{4,14}$"))
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

            Name = name;
            Phone = phone;
            Email = email;
            return Result.Updated;
        }   

    }
}
