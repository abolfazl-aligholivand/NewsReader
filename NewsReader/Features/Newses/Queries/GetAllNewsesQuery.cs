using MediatR;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using NewsReader.Domain.DTO.NewsDTO;
using NewsReader.Domain.Helpers;
using NewsReader.Domain.Repository.IRepository;
using System.Runtime.CompilerServices;

namespace NewsReader.Features.Newses.Queries
{
    public class GetAllNewsesQuery : IRequest<ApiResponse<IEnumerable<NewsDTO>>>
    {
        public DateTime? Date { get; set; }
        public int PageSize { get; set; }
        public int PageNumber { get; set; }

        public class GetAllNewsesQueryHandler : IRequestHandler<GetAllNewsesQuery, ApiResponse<IEnumerable<NewsDTO>>>
        {
            private readonly INewsRepository _newsRepository;
            private readonly ILogger _logger;
            public GetAllNewsesQueryHandler(ILogger logger, INewsRepository newsRepository)
            {
                _logger = logger;
                _newsRepository = newsRepository;
            }
            public async Task<ApiResponse<IEnumerable<NewsDTO>>> Handle(GetAllNewsesQuery request, CancellationToken cancellationToken)
            {
                try
                {
                    var newses = await _newsRepository.GetAllAsync(new GetNewsFilterDTO
                    {
                        Date = request.Date,
                        PageNumber = request.PageNumber,
                        PageSize = request.PageSize
                    });

                    var result = newses.Select(s => new NewsDTO
                    {
                        Id = s.Id,
                        CategoryId = s.FKCategoryId,
                        Category = s.FKCategory.Category,
                        Creator = s.Creator,
                        Date = s.Date,
                        Link = s.Link,
                        Media = s.Media,
                        Publisher = s.Publisher,
                        Subject = s.Subject,
                        Description = s.Description,
                        Website = s.FKWebsite.Title,
                        WebsiteId = s.FKWebsite.Id,
                    });

                    return new ApiResponse<IEnumerable<NewsDTO>>(Domain.Enums.HttpStatusCodeEnum.Success, result, ApiMessage.Successful);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex.Message);
                    return new ApiResponse<IEnumerable<NewsDTO>>(Domain.Enums.HttpStatusCodeEnum.BadRequest, Enumerable.Empty<NewsDTO>(), ApiMessage.Error);
                }
            }
        }
    }
}
