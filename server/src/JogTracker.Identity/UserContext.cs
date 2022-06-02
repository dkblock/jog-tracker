using JogTracker.Models.Users;

namespace JogTracker.Identity
{
    public interface IUserContext
    {
        User CurrentUser { get; }
        void AttachUser(User user);
    }

    public class UserContext : IUserContext
    {
        public User CurrentUser { get; private set; }

        public void AttachUser(User user)
        {
            CurrentUser = user;
        }
    }
}
