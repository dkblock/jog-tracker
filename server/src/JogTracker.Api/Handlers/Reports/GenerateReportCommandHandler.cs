using AutoMapper;
using JogTracker.Common.Exceptions;
using JogTracker.Mappers;
using JogTracker.Models.DTO.Jogs;
using JogTracker.Models.DTO.Users;
using JogTracker.Models.Requests.Reports;
using JogTracker.Repository;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace JogTracker.Api.Handlers.Reports
{
    public class GenerateReportCommandHandler : IRequestHandler<GenerateReportCommand, JogReport>
    {
        private readonly IJogsRepository _jogsRepository;
        private readonly IUsersRepository _usersRepository;
        private readonly IMapper _mapper;

        public GenerateReportCommandHandler(IJogsRepository jogsRepository, IUsersRepository usersRepository, IMapper mapper)
        {
            _jogsRepository = jogsRepository;
            _usersRepository = usersRepository;
            _mapper = mapper;
        }

        public async Task<JogReport> Handle(GenerateReportCommand payload, CancellationToken cancellationToken)
        {
            var jogs = _jogsRepository
                .GetQueryable()
                .Include(j => j.User)
                .AsQueryable();

            if (payload.DateFrom.HasValue)
                jogs = jogs.Where(j => j.Date.Date >= payload.DateFrom.Value.Date);

            if (payload.DateTo.HasValue)
                jogs = jogs.Where(j => j.Date.Date <= payload.DateTo.Value.Date);

            if (!jogs.Any())
                throw new NotFoundException();

            var groupedJogs = jogs.GroupBy(j => j.User.Id);
            var jogStats = groupedJogs.Select(kvp => new JogStats
            {
                TotalDistanceInMeters = kvp.Sum(kvp => kvp.DistanceInMeters),
                TotalElapsedTimeInSeconds = kvp.Sum(kvp => kvp.ElapsedTimeInSeconds),
                AverageSpeedInMetersPerSecond = kvp.Average(kvp => kvp.AverageSpeedInMetersPerSecond),
                UserId = kvp.Key,
            });

            var maxTotalDistanceState = jogStats.OrderByDescending(s => s.TotalDistanceInMeters).FirstOrDefault();
            var maxTotalTimeState = jogStats.OrderByDescending(s => s.TotalElapsedTimeInSeconds).FirstOrDefault();
            var maxAverageSpeedState = jogStats.OrderByDescending(s => s.AverageSpeedInMetersPerSecond).FirstOrDefault();

            var report = new JogReport
            {
                TotalJogs = jogs.Count(),

                MaxTotalDistanceInMeters = maxTotalDistanceState.TotalDistanceInMeters,
                MaxTotalDistanceInKilometers = JogValuesMapper.ToKilometers(maxTotalDistanceState.TotalDistanceInMeters),
                MaxTotalDistanceUser = _mapper.Map<User>(await _usersRepository.GetById(maxTotalDistanceState.UserId)),

                MaxTotalElapsedTime = JogValuesMapper.ToTime(maxTotalTimeState.TotalElapsedTimeInSeconds),
                MaxTotalElapsedTimeUser = _mapper.Map<User>(await _usersRepository.GetById(maxTotalTimeState.UserId)),

                MaxAverageSpeedInMetersPerSecond = JogValuesMapper.ToMetersPerSecond(maxAverageSpeedState.AverageSpeedInMetersPerSecond),
                MaxAverageSpeedInKilometersPerHour = JogValuesMapper.ToKilometersPerHour(maxAverageSpeedState.AverageSpeedInMetersPerSecond),
                MaxAverageSpeedUser = _mapper.Map<User>(await _usersRepository.GetById(maxAverageSpeedState.UserId)),
            };

            if (string.IsNullOrEmpty(payload.UserId))
                return report;

            var ownStat = jogStats.SingleOrDefault(s => s.UserId == payload.UserId);

            if (ownStat == null)
                return report;

            report.TotalOwnJogs = jogs.Count(j => j.UserId == payload.UserId);
            report.OwnTotalDistanceInMeters = ownStat.TotalDistanceInMeters;
            report.OwnTotalDistanceInKilometers = JogValuesMapper.ToKilometers(ownStat.TotalDistanceInMeters);
            report.OwnTotalElapsedTime = JogValuesMapper.ToTime(ownStat.TotalElapsedTimeInSeconds);
            report.OwnAverageSpeedInMetersPerSecond = JogValuesMapper.ToMetersPerSecond(ownStat.AverageSpeedInMetersPerSecond);
            report.OwnAverageSpeedInKilometersPerHour = JogValuesMapper.ToKilometersPerHour(ownStat.AverageSpeedInMetersPerSecond);
            report.User = _mapper.Map<User>(await _usersRepository.GetById(ownStat.UserId));

            return report;
        }
    }
}
