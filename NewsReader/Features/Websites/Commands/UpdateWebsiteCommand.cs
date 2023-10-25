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
    public class UpdateWebsiteCommand : IRequest<ApiResponse<WebsiteDTO>>
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Url { get; set; }
        public string FeedLink { get; set; }
        public int CategoryId { get; set; }

        public class UpdateWebsiteCommandHandler : IRequestHandler<UpdateWebsiteCommand, ApiResponse<WebsiteDTO>>
        {
            private readonly IMapper _mapper;
            private readonly ILogger _logger;
            private readonly NewsReaderContext _context;
            private readonly IWebsiteRepository _websiteRepository;
            public UpdateWebsiteCommandHandler(NewsReaderContext context, IMapper mapper, ILogger logger, IWebsiteRepository websiteRepository)
            {
                _mapper = mapper;
                _logger = logger;
                _context = context;
                _websiteRepository = websiteRepository;
            }
            public async Task<ApiResponse<WebsiteDTO>> Handle(UpdateWebsiteCommand request, CancellationToken cancellationToken)
            {
                try
                {
                    var website = await _websiteRepository.GetAsync(request.Id);
                    if (website is null)
                        return new ApiResponse<WebsiteDTO>(Domain.Enums.HttpStatusCodeEnum.NotFound, null, ApiMessage.NotFound);

                    website.Title = request.Title;
                    website.Url = request.Url;
                    website.FeedLink = request.FeedLink;
                    website.FKCategoryId = request.CategoryId;

                    var updatedWebsite = _websiteRepository.Update(website);
                    if (updatedWebsite is null)
                        return new ApiResponse<WebsiteDTO>(Domain.Enums.HttpStatusCodeEnum.BadRequest, null, ApiMessage.Error);

                    await _context.SaveChangesAsync();
                    var result = _mapper.Map<WebsiteDTO>(updatedWebsite);
                    return new ApiResponse<WebsiteDTO>(Domain.Enums.HttpStatusCodeEnum.Success, result, ApiMessage.WebsiteUpdateSuccessful);
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
