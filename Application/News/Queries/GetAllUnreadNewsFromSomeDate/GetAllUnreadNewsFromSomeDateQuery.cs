using Outlou.Application.Abstractions.Messaging;

namespace Outlou.Application.News.Queries.GetAllUnreadNewsFromSomeDate;

public sealed record GetAllUnreadNewsFromSomeDateQuery(Guid UserId,DateTime Date) : IQuery<IEnumerable<UnreadNewsFromSomeDateResponse>>;