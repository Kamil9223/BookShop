using System;
using System.Net;

namespace Ksiegarnia.Exceptions
{
    public abstract class AppException : Exception
    {
        public int ErrorCode { get; set; }
        public HttpStatusCode StatusCode { get; set; }

        public AppException(string message) : base(message) { }

        public AppException(int errorCode, string message) : base (message)
        {
            this.ErrorCode = errorCode;
        }

        public AppException(HttpStatusCode statusCode, int errorCode, string message) : base(message)
        {
            this.ErrorCode = errorCode;
            this.StatusCode = statusCode;
        }
    }
}
