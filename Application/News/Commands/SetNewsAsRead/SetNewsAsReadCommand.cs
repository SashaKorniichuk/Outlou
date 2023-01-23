using Outlou.Application.Abstractions.Messaging;

namespace Outlou.Application.News.Commands.SetNewsAsRead;

public sealed record SetNewsAsReadCommand(Guid Id,Guid UserId) : ICommand;