using System.Security.Claims;

namespace JogTracker.Common.Extensions
{
    public static class IdentityExtensions
    {
        public static string GetUserId(this ClaimsPrincipal principal)
        {
            if (principal == null)
                return null;

            var claim = principal.FindFirst(ClaimTypes.PrimarySid);
            return claim?.Value;
        }
    }
}
