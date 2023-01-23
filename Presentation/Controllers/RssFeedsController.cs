using System.Security.Claims;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Outlou.Application.RssFeeds.Commands.AddRssFeed;
using Outlou.Application.RssFeeds.Queries.GetAllActiveRssFeeds;
using Presentation.Abstractions;

namespace Presentation.Controllers;

[Route("api/RSSFeeds")]
public sealed class RssFeedsController : ApiController
{
    public RssFeedsController(ISender sender)
        : base(sender)
    {
    }

    [Authorize]
    [HttpPost("AddFeed/{uri}")]
    public async Task<IActionResult> AddFeed(string uri, CancellationToken token)
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        var command = new AddRssFeedCommand(uri,Guid.Parse(userId));

        var result = await Sender.Send(command, token);


        return result.IsSuccess ? Ok() : BadRequest(result.Error);
    }

    [Authorize]
    [HttpGet("GetAllActiveFeeds")]
    public async Task<IActionResult> GetAllActiveFeeds(CancellationToken token)
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        var query = new GetAllActiveRssFeedsQuery(Guid.Parse(userId));

        var result = await Sender.Send(query, token);

        return result.IsSuccess ? Ok(result.Value) : BadRequest(result.Error);
    }
}

