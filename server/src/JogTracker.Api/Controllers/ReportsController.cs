using JogTracker.Common.Extensions;
using JogTracker.Models.Requests.Reports;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace JogTracker.Api.Controllers
{
    [ApiController]
    [Route("api/reports")]
    public class ReportsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ReportsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [Route("generate")]
        public async Task<IActionResult> GenerateReport(
            [FromQuery] DateTime? dateFrom = null,
            [FromQuery] DateTime? dateTo = null)
        {
            var payload = new GenerateReportCommand(dateFrom, dateTo, User.GetUserId());
            var report = await _mediator.Send(payload);

            return Ok(report);
        }
    }
}
