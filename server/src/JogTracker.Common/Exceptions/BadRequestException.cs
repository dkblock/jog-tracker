using System.Collections.Generic;
using System.Net;

namespace JogTracker.Common.Exceptions
{
    public class BadRequestException : ApiException
    {
        public IEnumerable<Error> Errors { get; set; }

        public BadRequestException(IEnumerable<Error> errors)
            : base(HttpStatusCode.BadRequest, "The server cannot or will not process the request due to an apparent client error")
        {
            Errors = errors;
        }
    }
}
