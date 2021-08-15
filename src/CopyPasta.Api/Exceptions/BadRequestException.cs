using System;
using Microsoft.AspNetCore.Http;

namespace CopyPasta.Api.Exceptions
{
    public class BadRequestException : BaseException
    {
        public BadRequestException(string message) : base(StatusCodes.Status400BadRequest, message)
        {
        }

        public BadRequestException(string message, Exception innerException) : base(StatusCodes.Status400BadRequest, message, innerException)
        {
        }
    }
}