using System.Net;

namespace JogTracker.Common.Exceptions
{
    public class ForbiddenException : ApiException
    {
        public ForbiddenException()
            : base(HttpStatusCode.Forbidden, "You have no the necessary permissions to access this resource")
        {

        }
    }
}
