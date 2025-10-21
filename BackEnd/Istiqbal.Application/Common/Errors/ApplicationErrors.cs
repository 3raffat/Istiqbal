using Istiqbal.Domain.Common.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Istiqbal.Application.Common.Errors
{
    public static class ApplicationErrors
    {
        public static Error RoomTypeNotFound =>
           Error.NotFound(
        "ApplicationErrors.RoomType.NotFound",
        "RoomType does not exist.");

        public static Error RoomNotFound =>
           Error.NotFound(
          "ApplicationErrors.Room.NotFound",
          "Room does not exist.");

        public static Error GuestNotFound =>
           Error.NotFound("ApplicationErrors.Guest.NotFound", "Guest does not exist.");

        public static  Error ExpiredAccessTokenInvalid = Error.Conflict(
             "Auth.ExpiredAccessToken.Invalid","Expired access token is not valid.");

        public static  Error UserIdClaimInvalid = Error.Conflict(
             "Auth.UserIdClaim.Invalid","Invalid userId claim.");

        public static  Error RefreshTokenExpired = Error.Conflict(
             "Auth.RefreshToken.Expired", "Refresh token is invalid or has expired.");

        public static  Error UserNotFound = Error.NotFound(
             "Auth.User.NotFound", "User not found.");

        public static readonly Error TokenGenerationFailed = Error.Failure(
             "Auth.TokenGeneration.Failed","Failed to generate new JWT token.");


    }
}
