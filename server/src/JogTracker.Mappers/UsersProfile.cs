using AutoMapper;
using JogTracker.Entities;
using JogTracker.Models.DTO.Users;
using JogTracker.Models.Requests.Account;
using JogTracker.Models.Requests.Users;
using System.Linq;

namespace JogTracker.Mappers
{
    public class UsersProfile : Profile
    {
        public UsersProfile()
            : base()
        {
            CreateMap<UserEntity, User>()
                .ForMember(dest => dest.TotalJogs, src => src.MapFrom(src => src.Jogs.Count()));

            CreateMap<RegisterCommand, UserEntity>();
            CreateMap<UpdateUserCommand, UserEntity>();
            CreateMap<CreateRefreshTokenCommand, RefreshTokenEntity>();
        }
    }
}
