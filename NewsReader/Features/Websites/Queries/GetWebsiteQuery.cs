using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using NewsReader.Domain.DTO.WebsiteDTO;
using NewsReader.Domain.Helpers;
using NewsReader.Domain.Repository.IRepository;

namespace NewsReader.Features.Websites.Queries
{
    public class GetWebsiteQuery : IRequest<ApiResponse<WebsiteDTO>>
    {
        public int Id { get; set; }

        public class GetWebsiteQueryHandler : IRequestHandler<GetWebsiteQuery, ApiResponse<WebsiteDTO>>
        {
            private readonly IMapper _mapper;
            private readonly ILogger _logger;
            private readonly IWebsiteRepository _websiteRepository;
            public GetWebsiteQueryHandler(IMapper mapper, ILogger logger, IWebsiteRepository websiteRepository)
            {
                _mapper = mapper;
                _logger = logger;
                _websiteRepository = websiteRepository;
            }

            public async Task<ApiResponse<WebsiteDTO>> Handle(GetWebsiteQuery request, CancellationToken cancellationToken)
            {
                try
                {
                    var website = await _websiteRepository.GetAsync(request.Id);
                    if (website is null)
                        return new ApiResponse<WebsiteDTO>(Domain.Enums.HttpStatusCodeEnum.NotFound, null, ApiMessage.NotFound);

                    var result = _mapper.Map<WebsiteDTO>(website);
                    return new ApiResponse<WebsiteDTO>(Domain.Enums.HttpStatusCodeEnum.Success, result, ApiMessage.Successful);
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
