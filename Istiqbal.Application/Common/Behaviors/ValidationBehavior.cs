using FluentValidation;
using Istiqbal.Domain.Common.Results;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Istiqbal.Application.Common.Behaviors
{
    public sealed class ValidationBehavior
        <TRequest, TResponse>(IValidator<TRequest>? validator=null)
        :IPipelineBehavior<TRequest, TResponse>
        where TRequest : notnull
    {
        private readonly IValidator<TRequest>? _validator = validator;

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
           if(_validator is null )
                return await next(cancellationToken);

           var ValidationResult = await _validator.ValidateAsync(request,cancellationToken);

            if (ValidationResult.IsValid)
                return await next(cancellationToken);

            var errors = ValidationResult.Errors.ConvertAll(e=>Error.Validation(e.ErrorMessage,e.PropertyName));


            return (dynamic)errors;
        }
    }
}
