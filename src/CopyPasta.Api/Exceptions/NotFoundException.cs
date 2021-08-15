using System;
using Microsoft.AspNetCore.Http;

namespace CopyPasta.Api.Exceptions
{
    [Serializable]
    public class NotFoundException : BaseException
    {
        public NotFoundException(string message) : base(StatusCodes.Status404NotFound, message)
        {
        }
        public NotFoundException(string message, Exception exception) : base(StatusCodes.Status404NotFound, message, exception)
        {
        }
    }
}