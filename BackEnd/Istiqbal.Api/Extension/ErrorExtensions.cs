using Istiqbal.Domain.Common.Results;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using static Istiqbal.Application.Common.Responses.StandardResponse;

namespace Istiqbal.Api.Extension
{
    public static class ErrorExtensions
    {
        private static IHttpContextAccessor? _httpContextAccessor;

        public static void Initialize(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }
        public static IActionResult ToActionResult(this Error error)
        {
            var statusCode = error.Type switch
            {
                ErrorKind.Validation => StatusCodes.Status400BadRequest,
                ErrorKind.NotFound => StatusCodes.Status404NotFound,
                ErrorKind.Conflict => StatusCodes.Status409Conflict,
                ErrorKind.Unauthorized => StatusCodes.Status401Unauthorized,
                ErrorKind.Forbidden => StatusCodes.Status403Forbidden,
                ErrorKind.Failure => StatusCodes.Status500InternalServerError,
                _ => StatusCodes.Status500InternalServerError
            };

            var traceId = _httpContextAccessor?.HttpContext?.TraceIdentifier;

            var response = new StandardErrorResponse(DateTime.UtcNow.ToString("o"),statusCode, error.Description, error.Code, traceId);


            return new ObjectResult(response)
            {
                StatusCode = statusCode
            };
        }
    }
}
