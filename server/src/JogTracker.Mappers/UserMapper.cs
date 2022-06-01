using JogTracker.Entities;
using JogTracker.Models.Account;
using JogTracker.Models.Users;

namespace JogTracker.Mappers
{
    public interface IUserMapper
    {
        UserEntity ToEntity(RegisterPayload payload);
        User ToModel(UserEntity entity);
    }

    public class UserMapper : IUserMapper
    {
        public UserEntity ToEntity(RegisterPayload payload)
        {
            return new UserEntity
            {
                UserName = payload.Username,
                FirstName = payload.FirstName,
                LastName = payload.LastName,
            };
        }

        public User ToModel(UserEntity entity)
        {
            if (entity == null)
                return null;

            return new User
            {
                Id = entity.Id,
                Username = entity.UserName,
                FirstName = entity.FirstName,
                LastName = entity.LastName,
            };
        }
    }
}
