using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Istiqbal.Application.Featuers.Amenities.Commands.DeleteAmenity
{
    public sealed class DeleteAmenityCommandValidator :AbstractValidator<DeleteAmenityCommand>
    {
        public DeleteAmenityCommandValidator()
        {
            RuleFor(x => x.Id)
                      .NotEmpty().WithMessage("The Id cannot be empty.");
        }
    }
}
