using JogTracker.Database;
using JogTracker.Entities;
using JogTracker.Models.Jogs;
using System.Collections.Generic;
using System.Linq;

namespace JogTracker.Services
{
    public interface IJogService
    {
        JogEntity CreateJog(JogEntity jog);
        bool IsJogExist(int id);
        JogEntity GetJogById(int id);
        JogEntity UpdateJog(JogEntity jog);
    }

    public class JogService : IJogService
    {
        private readonly IRepository<JogEntity> _repository;

        public JogService(IRepository<JogEntity> repository)
        {
            _repository = repository;
        }

        public JogEntity CreateJog(JogEntity jog)
        {
            return _repository.Create(jog);
        }

        public bool IsJogExist(int id)
        {
            return _repository.Exists(id);
        }

        public JogEntity GetJogById(int id)
        {
            return _repository.Get(id);
        }

        public IEnumerable<JogEntity> GetJogsByQuery(JogQuery query)
        {
            var jogs = _repository.Get(j => j != null).Where(j => IsMatchQuery(j, query));
            var sortedJogs = query.SortByDesc
                ? jogs.OrderByDescending(j => j.Date)
                : jogs.OrderBy(j => j.Date);

            return sortedJogs.Skip(query.PageSize * (query.PageIndex - 1)).ToList();
        }

        public JogEntity UpdateJog(JogEntity jog)
        {
            //var jogEntity = _mapper.ToEntity(jog);
            //var updatedJog = await _repository.Update(jogEntity.Id, jogEntity);

            //return _mapper.ToModel(updatedJog);
            return null;
        }

        private bool IsMatchQuery(JogEntity jog, JogQuery query)
        {
            if ((query.DateFrom.HasValue && jog.Date < query.DateFrom) || (query.DateTo.HasValue && jog.Date > query.DateTo))
                return false;

            return true;
        }
    }    
}
