using AutoMapper;
using JogTracker.Entities;
using JogTracker.Models.Commands.Account;
using JogTracker.Models.DTO.Users;

namespace JogTracker.Mappers
{
    public class UsersProfile : Profile
    {
        public UsersProfile()
            : base()
        {
            CreateMap<UserEntity, User>();
            CreateMap<RegisterCommand, UserEntity>();
        }
    }
}
