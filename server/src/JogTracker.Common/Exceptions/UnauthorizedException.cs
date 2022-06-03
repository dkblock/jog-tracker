using System.Net;

namespace JogTracker.Common.Exceptions
{
    public class UnauthorizedException : ApiException
    {
        public UnauthorizedException(string message) 
            : base(HttpStatusCode.Unauthorized, message)
        {
        }
    }
}
