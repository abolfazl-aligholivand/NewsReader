using AutoMapper;
using MediatR;
using NewsReader.Domain.Data;
using NewsReader.Domain.DTO.WebsiteDTO;
using NewsReader.Domain.Helpers;
using NewsReader.Domain.Models;
using NewsReader.Domain.Repository.IRepository;

#nullable disable

namespace NewsReader.Features.Websites.Commands
{
    public class CreateWebsiteCommand : IRequest<ApiResponse<WebsiteDTO>>
    {
        public string Title { get; set; }
        public string Url { get; set; }
        public string FeedLink { get; set; }
        public int CategoryId { get; set; }

        public class CreateWebsiteCommandHandler : IRequestHandler<CreateWebsiteCommand, ApiResponse<WebsiteDTO>>
        {
            private readonly IMapper _mapper;
            private readonly ILogger _logger;
            private readonly NewsReaderContext _context;
            private readonly IWebsiteRepository _websiteRepository;
            public CreateWebsiteCommandHandler(IMapper mapper, ILogger logger, NewsReaderContext context, IWebsiteRepository websiteRepository)
            {
                _mapper = mapper;
                _logger = logger;
                _context = context;
                _websiteRepository = websiteRepository;
            }
            public async Task<ApiResponse<WebsiteDTO>> Handle(CreateWebsiteCommand request, CancellationToken cancellationToken)
            {
                try
                {
                    var website = new Website
                    {
                        Title = request.Title,
                        Url = request.Url,
                        FeedLink = request.FeedLink,
                        FKCategoryId = request.CategoryId
                    };

                    var addWebsite = await _websiteRepository.CreateAsynce(website);
                    if (addWebsite is null)
                        return new ApiResponse<WebsiteDTO>(Domain.Enums.HttpStatusCodeEnum.BadRequest, null, ApiMessage.Error);

                    await _context.SaveChangesAsync();
                    var result = _mapper.Map<WebsiteDTO>(addWebsite);
                    return new ApiResponse<WebsiteDTO>(Domain.Enums.HttpStatusCodeEnum.Success, result, ApiMessage.WebsiteCreateSuccessful);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex.Message);
                    return new ApiResponse<WebsiteDTO>(Domain.Enums.HttpStatusCodeEnum.BadRequest, null, ApiMessage.Error);
                }
            }
        }
    }
}
