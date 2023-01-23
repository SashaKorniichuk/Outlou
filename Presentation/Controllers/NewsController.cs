using MediatR;
using Microsoft.AspNetCore.Mvc;
using Outlou.Application.News.Commands.SetNewsAsRead;
using Outlou.Application.News.Queries.GetAllUnreadNewsFromSomeDate;
using Presentation.Abstractions;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace Presentation.Controllers;

[Route("api/news")]
public sealed class NewsController : ApiController
{
    public NewsController(ISender sender)
        : base(sender)
    {
    }

    [Authorize]
    [HttpPost("ReedNews/{id}")]
    public async Task<IActionResult> ReedNews(Guid id, CancellationToken cancellationToken)
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        var command = new SetNewsAsReadCommand(id,Guid.Parse(userId));

        var result = await Sender.Send(command, cancellationToken);

        return result.IsSuccess ? Ok() : BadRequest(result.Error);
    }

    [Authorize]
    [HttpGet("GetAllUnreadNewsFromSomeDate/{dateTime}")]
    public async Task<IActionResult> GetAllUnreadNews(DateTime dateTime, CancellationToken cancellationToken)
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        var query = new GetAllUnreadNewsFromSomeDateQuery(Guid.Parse(userId),dateTime);

        var result = await Sender.Send(query, cancellationToken);

        return result.IsSuccess ? Ok(result.Value) : BadRequest(result.Error);
    }
}