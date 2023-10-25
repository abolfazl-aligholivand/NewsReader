using MediatR;
using NewsReader.Domain.Data;
using NewsReader.Domain.DTO.NewsDTO;
using NewsReader.Domain.Enums;
using NewsReader.Domain.Helpers;
using NewsReader.Domain.Repository.IRepository;

namespace NewsReader.Features.ReadNews.Commands
{
    public class ReadNewsFromWebsiteCommand : IRequest<ApiResponse<IEnumerable<NewsDTO>>>
    {
        public int websiteId { get; set; }
        public class ReadNewsFromWebsiteCommandHandler : IRequestHandler<ReadNewsFromWebsiteCommand, ApiResponse<IEnumerable<NewsDTO>>>
        {
            private readonly IWebsiteRepository _websiteRepository;
            private readonly INewsRepository _newsRepository;
            private readonly SendMessage _sendMessage;
            private readonly NewsReaderContext _context;
            private readonly ILogger _logger;
            public ReadNewsFromWebsiteCommandHandler(INewsRepository newsRepository, IWebsiteRepository websiteRepository, NewsReaderContext context,
                SendMessage sendMessage, ILogger logger)
            {
                _newsRepository = newsRepository;
                _websiteRepository = websiteRepository;
                _context = context;
                _sendMessage = sendMessage;
                _logger = logger;
            }
            public async Task<ApiResponse<IEnumerable<NewsDTO>>> Handle(ReadNewsFromWebsiteCommand request, CancellationToken cancellationToken)
            {
                try
                {
                    var website = await _websiteRepository.GetAsync(request.websiteId);
                    if (website is null)
                        return new ApiResponse<IEnumerable<NewsDTO>>(Domain.Enums.HttpStatusCodeEnum.NotFound, Enumerable.Empty<NewsDTO>(), ApiMessage.NotFound);

                    var todayNewses = FeedReader.ReadRSSFeed(website.FeedLink, out string error);
                    if(!string.IsNullOrEmpty(error))
                        return new ApiResponse<IEnumerable<NewsDTO>>(Domain.Enums.HttpStatusCodeEnum.BadRequest, Enumerable.Empty<NewsDTO>(), error);

                    foreach (var news in todayNewses)
                    {
                        //check news by newsGuid
                        var oldNews = await _newsRepository.GetAsync(news.NewsGuid);
                        if (oldNews is not null)
                        {
                            news.Id = oldNews.Id;
                            continue;
                        }

                        //add news to db
                        news.FKCategoryId = 1;
                        news.FKWebsiteId = website.Id;
                        news.Subject ??= "No Subject";
                        news.Media ??= "No Media";
                        news.Publisher ??= "No Publisher";
                        var addedNews = await _newsRepository.CreateAsynce(news);
                        if (addedNews is not null)
                            await _context.SaveChangesAsync();    
                        news.Id = addedNews.Id;
                    }

                    var result = todayNewses.Select(s => new NewsDTO
                    {
                        Id = s.Id,
                        Website = website.Title,
                        Date = s.Date,
                        Description = s.Description,
                        WebsiteId = website.Id,
                        Title = s.Title,
                        Link = s.Link
                    });
                    return new ApiResponse<IEnumerable<NewsDTO>>(HttpStatusCodeEnum.Success, result, ApiMessage.Successful);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex.Message);
                    return new ApiResponse<IEnumerable<NewsDTO>>(HttpStatusCodeEnum.BadRequest, Enumerable.Empty<NewsDTO>(), ApiMessage.Error);
                }
            }
        }
    }
}
