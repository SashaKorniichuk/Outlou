using Domain.Errors;
using Domain.Repositories;
using Domain.Shared;
using Outlou.Application.Abstractions.Messaging;

namespace Outlou.Application.News.Commands.SetNewsAsRead;

public class SetNewsAsReadCommandHandler : ICommandHandler<SetNewsAsReadCommand>
{
    private readonly INewsRepository _newsRepository;
    private readonly  IUserNewsRepository _userNewsRepository;
    private readonly IUnitOfWork _unitOfWork;

    public SetNewsAsReadCommandHandler(INewsRepository newsRepository, IUnitOfWork unitOfWork,IUserNewsRepository userNewsRepository)
    {
        _newsRepository = newsRepository;
        _unitOfWork = unitOfWork;
        _userNewsRepository=userNewsRepository;
    }

    public async Task<Result> Handle(SetNewsAsReadCommand request, CancellationToken cancellationToken)
    {
        var news = await _userNewsRepository.GetNewsByIdAsync(request.Id, cancellationToken);

        if (news is null)
        {
            return Result.Failure(DomainErrors.News.NotFound(request.Id));
        }

        news.MarkAsRead();

        _userNewsRepository.Update(news);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}