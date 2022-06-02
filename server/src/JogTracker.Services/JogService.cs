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
        IEnumerable<JogEntity> GetJogsByQuery(JogQuery query);
        void DeleteJog(int id);
    }

    public class JogService : IJogService
    {
        private readonly IRepository<JogEntity> _repository;
        private readonly IQueryHelper _queryHelper;

        public JogService(IRepository<JogEntity> repository, IQueryHelper queryHelper)
        {
            _repository = repository;
            _queryHelper = queryHelper;
        }

        public JogEntity CreateJog(JogEntity jog)
        {
            jog = _repository.Create(jog);
            return GetJogById(jog.Id);
        }

        public bool IsJogExist(int id)
        {
            return _repository.Exists(id);
        }

        public JogEntity GetJogById(int id)
        {
            return _repository.GetWithInclude(j => j.Id == id, j => j.User).Single();
        }

        public IEnumerable<JogEntity> GetJogsByQuery(JogQuery query)
        {
            var dbQuery = new DbQuery<JogEntity>(
                j => IsMatchQuery(j, query),
                j => _queryHelper.JogSortModel[query.SortBy],
                query.SortByDesc,
                query.PageSize,
                query.PageIndex);

            return _repository.GetByQuery(dbQuery);
        }

        public void DeleteJog(int id)
        {
            _repository.Delete(id);
        }

        public JogEntity UpdateJog(int id, JogEntity jog)
        {
            jog.Id = id;
            _repository.Update(id, jog);

            return GetJogById(id);
        }

        private bool IsMatchQuery(JogEntity jog, JogQuery query)
        {
            var filters = new[]
            {
                jog.User.UserName,
                jog.User.FirstName,
                jog.User.LastName,
                $"{jog.User.FirstName} {jog.User.LastName}",
                $"{jog.User.LastName} {jog.User.FirstName}",
            };

            if (_queryHelper.IsMatch(query.SearchText, filters))
                return false;

            if ((query.DateFrom.HasValue && jog.Date < query.DateFrom) || (query.DateTo.HasValue && jog.Date > query.DateTo))
                return false;

            return true;
        }
    }
}
