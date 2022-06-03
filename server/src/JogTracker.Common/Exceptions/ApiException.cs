using System;
using System.Net;

namespace JogTracker.Common.Exceptions
{
    public class ApiException : Exception
    {
        public HttpStatusCode StatusCode { get; set; }

        public ApiException(HttpStatusCode statusCode)
            : this(statusCode, string.Empty) { }

        public ApiException(HttpStatusCode statusCode, string message)
            : base(message)
        {
            StatusCode = statusCode;
        }
    }
}
