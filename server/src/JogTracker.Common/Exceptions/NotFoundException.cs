using System.Net;

namespace JogTracker.Common.Exceptions
{
    public class NotFoundException : ApiException
    {
        public NotFoundException()
            : base(HttpStatusCode.NotFound, "The requested resource could not be found")
        {

        }
    }
}
