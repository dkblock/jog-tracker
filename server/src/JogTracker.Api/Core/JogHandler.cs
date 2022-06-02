using JogTracker.Entities;
using JogTracker.Mappers;
using JogTracker.Models.Jogs;
using JogTracker.Services;
using System.Collections.Generic;
using System.Linq;

namespace JogTracker.Api.Core
{
    public interface IJogHandler
    {
        Jog CreateJog(JogPayload jogPayload);
        bool IsJogExist(int id);
        Jog GetJogById(int id);
        PageResponse<Jog> GetJogsByQuery(JogQuery query);
        void DeleteJog(int id);
    }

    public class JogHandler : IJogHandler
    {
        private readonly IJogService _jogService;
        private readonly IJogMapper _jogMapper;
        private readonly IUserMapper _userMapper;

        public JogHandler(IJogService jogService, IJogMapper jogMapper, IUserMapper userMapper)
        {
            _jogService = jogService;
            _jogMapper = jogMapper;
            _userMapper = userMapper;
        }

        public Jog CreateJog(JogPayload jogPayload)
        {
            var jogEntity = _jogMapper.ToEntity(jogPayload);
            jogEntity = _jogService.CreateJog(jogEntity);

            return BuildJog(jogEntity);
        }

        public bool IsJogExist(int id)
        {
            return _jogService.IsJogExist(id);
        }

        public Jog GetJogById(int id)
        {
            var jogEntity = _jogService.GetJogById(id);
            return BuildJog(jogEntity);
        }

        public PageResponse<Jog> GetJogsByQuery(JogQuery query)
        {
            var jogsPage = _jogService.GetJogsByQuery(query);

            return new PageResponse<Jog>
            {
                Page = jogsPage.Page.Select(j => BuildJog(j)),
                TotalCount = jogsPage.TotalCount
            };
        }
        
        public void DeleteJog(int id)
        {
            _jogService.DeleteJog(id);
        }

        private Jog BuildJog(JogEntity jogEntity)
        {
            var jog = _jogMapper.ToModel(jogEntity);
            jog.User = _userMapper.ToModel(jogEntity.User);

            return jog;
        }
    }
}
