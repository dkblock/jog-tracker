﻿using JogTracker.Common.Extensions;
using JogTracker.Models.Requests.Jogs;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace JogTracker.Api.Controllers
{
    [ApiController]
    [Route("api/jogs")]
    public class JogsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public JogsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [Route("")]
        [Authorize]
        public async Task<IActionResult> CreateJog([FromBody] CreateJogCommand payload)
        {
            payload.UserId = User.GetUserId();
            var createdJog = await _mediator.Send(payload);

            return CreatedAtAction(nameof(GetJog), new { jogId = createdJog.Id }, createdJog);
        }

        [HttpGet]
        [Route("{jogId}")]
        public IActionResult GetJog()
        {
            return Ok();
        }

        [HttpGet]
        [Route("")]
        public async Task<IActionResult> GetJogs(
            [FromQuery] string searchText = "",
            [FromQuery] DateTime? dateFrom = null,
            [FromQuery] DateTime? dateTo = null,
            [FromQuery] bool onlyOwn = false,
            [FromQuery] int pageIndex = 1,
            [FromQuery] int pageSize = 20,
            [FromQuery] JogsSortBy sortBy = JogsSortBy.Date,
            [FromQuery] bool desc = true)
        {
            var query = new GetJogsQuery(searchText, dateFrom, dateTo, onlyOwn, pageSize, pageIndex, sortBy, desc, User.GetUserId());
            var jogsPage = await _mediator.Send(query);
            
            return Ok(jogsPage);
        }

        [HttpDelete]
        [Route("{jogId}")]
        [Authorize]
        public async Task<IActionResult> DeleteJog([FromRoute] string jogId)
        {
            var payload = new DeleteJogCommand(jogId, User.GetUserId());
            await _mediator.Send(payload);

            return NoContent();
        }

        [HttpPut]
        [Route("{jogId}")]
        [Authorize]
        public async Task<IActionResult> UpdateJog([FromBody] UpdateJogCommand payload)
        {
            payload.UserId = User.GetUserId();
            var updatedJog = await _mediator.Send(payload);

            return Ok(updatedJog);
        }
    }
}
