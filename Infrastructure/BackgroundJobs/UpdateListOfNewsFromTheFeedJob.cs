using Domain.Entities;
using Domain.Enums;
using Domain.Repositories;
using Microsoft.AspNetCore.Http;
using Quartz;
using System.ServiceModel.Syndication;
using System.Xml;

namespace Infrastructure.BackgroundJobs;

public class UpdateListOfNewsFromTheFeedJob : IJob
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly IUserFeedsRepository _userFeedsRepository;
    private readonly INewsRepository _newsRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IUserNewsRepository _userNewsRepository;
    private readonly IUserRepository _userRepository;


    public UpdateListOfNewsFromTheFeedJob(IHttpClientFactory httpClientFactory,
        IUserRepository userRepository,
        IUserFeedsRepository userFeedsRepository,
        INewsRepository newsRepository,
        IUnitOfWork unitOfWork,
        IUserNewsRepository userNewsRepository)
    {
        _httpClientFactory = httpClientFactory;
        _userFeedsRepository = userFeedsRepository;
        _newsRepository = newsRepository;
        _unitOfWork = unitOfWork;
        _userNewsRepository = userNewsRepository;
        _userRepository = userRepository;
    }

    public async Task Execute(IJobExecutionContext context)
    {
        var users = await _userRepository.GetAll();

        foreach (var user in users)
        {
            var feeds = await _userFeedsRepository.GetAllActiveFeeds(user.Id);
            var client = _httpClientFactory.CreateClient();
            foreach (var feed in feeds)
            {
                try
                {
                    var response = await client.GetAsync(feed.Uri);
                    var stream = await response.Content.ReadAsStreamAsync();
                    var reader = XmlReader.Create(stream);
                    var rssFeed = SyndicationFeed.Load(reader);

                    foreach (var item in rssFeed.Items)
                    {
                        if (!await _newsRepository.IsAlreadyExist(item.Title.Text)) continue;

                        var news = new Domain.Entities.News(Guid.NewGuid(), feed.Id, item.Title.Text, item.Title.Text);

                        _newsRepository.Add(news);
                        await _unitOfWork.SaveChangesAsync(default);

                        var userNews = new UserNews(Guid.NewGuid(), user.Id, news.Id, item.PublishDate.DateTime,
                            NewsStatus.Unread);

                        _userNewsRepository.Add(userNews);
                        await _unitOfWork.SaveChangesAsync(default);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            }
        }
    }
}

