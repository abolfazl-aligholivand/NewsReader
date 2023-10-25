using MediatR;
using NewsReader.Domain.Data;
using NewsReader.Domain.Enums;
using NewsReader.Domain.Helpers;
using NewsReader.Domain.Models;
using NewsReader.Domain.Repository.IRepository;

namespace ReadNews.Features.NewsReader.Commands
{
    public class ReadNewsCommand : IRequest<ApiResponse<bool>>
    {
        public class ReadNewsCommandHandler : IRequestHandler<ReadNewsCommand, ApiResponse<bool>>
        {
            private readonly NewsReaderContext _context;
            private readonly INewsRepository _newsRepository;
            private readonly IWebsiteRepository _websiteRepository;
            private readonly SendMessage _sendMessage;
            private readonly ILogger _logger;
            public ReadNewsCommandHandler(ILogger logger, NewsReaderContext context, INewsRepository newsRepository, IWebsiteRepository websiteRepository, SendMessage sendMessage)
            {
                _logger = logger;
                _context = context;
                _sendMessage = sendMessage;
                _newsRepository = newsRepository;
                _websiteRepository = websiteRepository;
            }

            public async Task<ApiResponse<bool>> Handle(ReadNewsCommand request, CancellationToken cancellationToken)
            {
                try
                {
                    //get all websites       
                    var websites = await _websiteRepository.GetAllAsync();

                    //read news from each websites
                    foreach (var site in websites)
                    {
                        var todayNewses = FeedReader.ReadRSSFeed(site.FeedLink, out string error);
                        if(!string.IsNullOrEmpty(error))
                        {
                            _logger.LogError(error);
                            continue;
                        }

                        foreach (var news in todayNewses)
                        {
                            //check news by newsGuid
                            var oldNews = await _newsRepository.GetAsync(news.NewsGuid);
                            if (oldNews is not null)
                                continue;

                            try
                            {
                                //add news to db
                                news.FKCategoryId = 1;
                                news.FKWebsiteId = site.Id;
                                news.Subject ??= "No Subject";
                                news.Media ??= "No Media";
                                news.Publisher ??= "No Publisher";
                                var addedNews = await _newsRepository.CreateAsynce(news);
                                if (addedNews is not null)
                                    await _context.SaveChangesAsync();

                                //send message via messenger (send to: @my_news_channel)
                                long chatId = 0;
                                await _sendMessage.SendTextMessage(news.Title, news.Link, news.Date.ToString("yyyy/MM/dd HH:mm"), chatId);
                            }
                            catch (Exception ex)
                            {
                                _logger.LogError($"Error on insert or send news: {ex.Message}");
                            }
                        }
                    }

                    return new ApiResponse<bool>(HttpStatusCodeEnum.Success, true, ApiMessage.Successful);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex.Message);
                    return new ApiResponse<bool>(HttpStatusCodeEnum.BadRequest, false, ApiMessage.Error);
                }
            }
        }
    }
}
