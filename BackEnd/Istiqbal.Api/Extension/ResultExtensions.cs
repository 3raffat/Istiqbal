using Istiqbal.Contracts.Responses;
using Istiqbal.Domain.Common.Results;
using Microsoft.AspNetCore.Mvc;

namespace Istiqbal.Api.Extension
{
    public static class ResultExtensions
    {
        public static ApiResponse<T> ToApiResponse<T>(this Result<T> result, string successMessage = null)
        {
            return new ApiResponse<T>
            {
                Data = result.IsSuccess ? result.Value : default,
                Success = result.IsSuccess,
                Message = result.IsSuccess
                    ? (successMessage ?? "Operation successful")
                    : result.TopError.Description,                   
                Timestamp = DateTime.UtcNow
            };
        }

        public static IActionResult ToActionResult<T>(this Result<T> result,
            ControllerBase controller,
            string successMessage = null,
            Func<T, IActionResult> onSuccess = null)
        {
            if (result.IsSuccess)
            {
                var response = result.ToApiResponse(successMessage);
                return onSuccess?.Invoke(result.Value) ?? controller.Ok(response);
            }

            return result.ToErrorActionResult<T>(controller);
        }

        public static IActionResult ToErrorActionResult<T>(this Result<T> result, ControllerBase controller)
        {
            var response = result.ToApiResponse<T>();
            var errorType = result.TopError.Type;

            return errorType switch
            {
                ErrorKind.Validation => controller.BadRequest(response),
                ErrorKind.NotFound => controller.NotFound(response),
                ErrorKind.Conflict => controller.Conflict(response),
                ErrorKind.Failure => controller.StatusCode(500, response),
                ErrorKind.Unexpected => controller.StatusCode(500, response),
                _ => controller.StatusCode(500, response)
            };
        }
    }
}
