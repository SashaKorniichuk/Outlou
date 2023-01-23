using Outlou.Application.Abstractions.Messaging;
using Domain.Entities;
using Domain.Enums;
using Domain.Repositories;
using Domain.Shared;
using Domain.Errors;
using System.Web;

namespace Outlou.Application.RssFeeds.Commands.AddRssFeed;

internal sealed class AddRssFeedCommandHandler : ICommandHandler<AddRssFeedCommand>
{
    private readonly IRssFeedRepository _rssFeedRepository;
    private readonly IUserFeedsRepository _userFeedsRepository;
    private readonly IUnitOfWork _unitOfWork;

    public AddRssFeedCommandHandler(IRssFeedRepository rssFeedRepository, IUnitOfWork unitOfWork,IUserFeedsRepository feedsRepository)
    {
        _rssFeedRepository = rssFeedRepository;
        _userFeedsRepository = feedsRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result> Handle(AddRssFeedCommand request, CancellationToken cancellationToken)
    {
        var uri = new Uri(request.Uri.Replace("%2F", "/"));
        var rssFeed = await _rssFeedRepository.GetFeedByUrl(uri, cancellationToken);

        if (rssFeed is null)
        {
            rssFeed = new RssFeed(Guid.NewGuid(), uri);
        
            _rssFeedRepository.Add(rssFeed);
            
            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }

        if (!await _userFeedsRepository.IsAlreadyExist(rssFeed.Id, cancellationToken))
        {
            return Result.Failure<string>(DomainErrors.UserFeed.FeedAlreadyExist);
        }

        var userFeed = new UserFeed(Guid.NewGuid(), request.UserId, rssFeed.Id,RssFeedStatus.Active);

        _userFeedsRepository.Add(userFeed);

        await _unitOfWork.SaveChangesAsync(cancellationToken);


        return Result.Success();
    }
}