using System;

namespace CopyPasta.Api.Exceptions
{
    public abstract class BaseException : Exception
    {
        public int StatusCode => _statusCode;
        private readonly int _statusCode;

        public BaseException(int statusCode, string? message, Exception? innerException) : base(message)
        {
            _statusCode = statusCode;
        }

        public BaseException(int statusCode, string? message) : this(statusCode, message, null)
        {
        }
    }
}