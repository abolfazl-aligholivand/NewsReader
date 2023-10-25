using MediatR;
using NewsReader.Domain.DTO.WebsiteDTO;
using NewsReader.Domain.Helpers;
using NewsReader.Domain.Repository.IRepository;

namespace NewsReader.Features.Websites.Queries
{
    public class GetAllWebsitesQuery : IRequest<ApiResponse<IEnumerable<WebsiteDTO>>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public class GetAllWebsitesQueryHandler : IRequestHandler<GetAllWebsitesQuery, ApiResponse<IEnumerable<WebsiteDTO>>>
        {
            private readonly ILogger _logger;
            private readonly IWebsiteRepository _websiteRepository;
            public GetAllWebsitesQueryHandler(ILogger logger, IWebsiteRepository websiteRepository)
            {
                _logger = logger;
                _websiteRepository = websiteRepository;
            }
            public async Task<ApiResponse<IEnumerable<WebsiteDTO>>> Handle(GetAllWebsitesQuery request, CancellationToken cancellationToken)
            {
                try
                {
                    var websites = await _websiteRepository.GetAllAsync();
                    var result = websites
                        .Select(s => new WebsiteDTO
                        {
                            Id = s.Id,
                            Title = s.Title,
                            Url = s.Url,
                            FeedLink = s.FeedLink,
                            CategoryId = s.FKCategoryId
                        });

                    return new ApiResponse<IEnumerable<WebsiteDTO>>(Domain.Enums.HttpStatusCodeEnum.Success, result, ApiMessage.Successful);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex.Message);
                    return new ApiResponse<IEnumerable<WebsiteDTO>>(Domain.Enums.HttpStatusCodeEnum.BadRequest, Enumerable.Empty<WebsiteDTO>(), ApiMessage.Error);
                }
            }
        }
    }
}
