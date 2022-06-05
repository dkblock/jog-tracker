using JogTracker.Models.DTO.Jogs;
using MediatR;
using System;

namespace JogTracker.Models.Requests.Reports
{
    public class GenerateReportCommand : IRequest<JogReport>
    {
        public GenerateReportCommand(DateTime? dateFrom, DateTime? dateTo, string userId)
        {
            DateFrom = dateFrom;
            DateTo = dateTo;
            UserId = userId;
        }

        public DateTime? DateFrom { get; set; }
        public DateTime? DateTo { get; set; }
        public string UserId { get; set; }
    }
}
