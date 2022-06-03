using AutoMapper;

namespace JogTracker.Mappers
{
    public static class AutoMapperConfig
    {
        public static IMapper Initialize()
        {
            return new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new JogsProfile());
                cfg.AddProfile(new UsersProfile());
            }).CreateMapper();
        }
    }
}
