using Outlou.Application.Abstractions.Messaging;

namespace Outlou.Application.RssFeeds.Commands.AddRssFeed;

public sealed record AddRssFeedCommand(string Uri, Guid UserId) : ICommand;