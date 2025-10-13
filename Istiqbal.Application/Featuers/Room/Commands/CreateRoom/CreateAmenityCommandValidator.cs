using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Istiqbal.Application.Featuers.Room.Commands.CreateRoom
{
    public sealed class CreateAmenityCommandValidator:AbstractValidator<CreateAmenityCommand>
    {
        public CreateAmenityCommandValidator()
        {
            RuleFor(x => x.id)
             .NotEmpty().WithMessage("Amenity ID is required.")
             .Must(id => id != Guid.Empty).WithMessage("Amenity ID must be a valid GUID.");

            RuleFor(x => x.name)
               .NotEmpty().WithMessage("Amenity  name is required.")
               .MaximumLength(50).WithMessage("Amenitys name must not exceed 50 characters.");

        }
    }
}
