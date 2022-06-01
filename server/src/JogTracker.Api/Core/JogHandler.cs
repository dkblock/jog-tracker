using JogTracker.Mappers;
using JogTracker.Models.Jogs;
using JogTracker.Services;

namespace JogTracker.Api.Core
{
    public interface IJogHandler
    {
        Jog CreateJog(JogPayload jogPayload);
        bool IsJogExist(int id);
        Jog GetJogById(int id);
    }

    public class JogHandler : IJogHandler
    {
        private readonly IJogService _jogService;
        private readonly IJogMapper _jogMapper;

        public JogHandler(IJogService jogService, IJogMapper jogMapper)
        {
            _jogService = jogService;
            _jogMapper = jogMapper;
        }

        public Jog CreateJog(JogPayload jogPayload)
        {
            var jogEntity = _jogMapper.ToEntity(jogPayload);
            jogEntity = _jogService.CreateJog(jogEntity);

            return _jogMapper.ToModel(jogEntity);
        }

        public bool IsJogExist(int id)
        {
            return _jogService.IsJogExist(id);
        }

        public Jog GetJogById(int id)
        {
            var jogEntity = _jogService.GetJogById(id);
            return _jogMapper.ToModel(jogEntity);
        }        
    }
}
