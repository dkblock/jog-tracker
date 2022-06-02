using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace JogTracker.Identity
{
    public class IdentityMiddleware
    {
        private readonly RequestDelegate _next;

        public IdentityMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context, IAuthenticationService authService, IUserContext userContext)
        {
            var currentUser = authService.GetUserFromPrincipal(context.User);
            userContext.AttachUser(currentUser);

            await _next(context);
        }
    }
}
