using Outlou.Application.Abstractions.Messaging;
using Domain.Repositories;
using Domain.Shared;

namespace Outlou.Application.News.Queries.GetAllUnreadNewsFromSomeDate;

public sealed class GetAllUnreadNewsFromSomeDateQueryHandler : IQueryHandler<GetAllUnreadNewsFromSomeDateQuery, IEnumerable<UnreadNewsFromSomeDateResponse>>
{
    private readonly INewsRepository _newsRepository;
    private readonly IUserNewsRepository _userNewsRepository;

    public GetAllUnreadNewsFromSomeDateQueryHandler(INewsRepository newsRepository, IUserNewsRepository userNewsRepository)
    {
        _newsRepository = newsRepository;
        _userNewsRepository=userNewsRepository;
    }

    public async Task<Result<IEnumerable<UnreadNewsFromSomeDateResponse>>> Handle(GetAllUnreadNewsFromSomeDateQuery request, CancellationToken cancellationToken)
    {

        var userNews = await _userNewsRepository.GetAllUnreadUserNewsFromSomeDate(request.UserId,request.Date,cancellationToken);

        var response = userNews
            .Select(x => new UnreadNewsFromSomeDateResponse(x.NewsId,
                new NewsResponse(x.News.RssFeedId, x.News.Name, x.News.Description), x.PublishedDateTime)).ToList();

        return response;
    }
}