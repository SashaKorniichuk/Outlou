using Outlou.Application.Abstractions.Messaging;
using Domain.Repositories;
using Domain.Shared;
using System.Linq;

namespace Outlou.Application.RssFeeds.Queries.GetAllActiveRssFeeds;

public class GetAllActiveRssFeedsQueryHandler : IQueryHandler<GetAllActiveRssFeedsQuery, List<ActiveRssFeedResponse>>
{
    private readonly IRssFeedRepository _rssFeedRepository;
    private readonly IUserFeedsRepository _userFeedsRepository;

    public GetAllActiveRssFeedsQueryHandler(IRssFeedRepository rssFeedRepository, IUserFeedsRepository userFeedsRepository)
    {
        _rssFeedRepository=rssFeedRepository;
        _userFeedsRepository=userFeedsRepository;
    }

    public async Task<Result<List<ActiveRssFeedResponse>>> Handle(GetAllActiveRssFeedsQuery request, CancellationToken cancellationToken)
    {

        var rssFeeds = await _userFeedsRepository.GetAllActiveFeeds(request.UserId,cancellationToken);

        var response = rssFeeds.Select(x => new ActiveRssFeedResponse(x.Id, x.Uri)).ToList();

        return response;
    }
}