using AutoMapper;
using MediatR;
using NewsReader.Domain.DTO.WebsiteCategoryDTO;
using NewsReader.Domain.Helpers;
using NewsReader.Domain.Repository.IRepository;

namespace NewsReader.Features.WebsiteCategories.Queries
{
    public class GetWebsiteCategoryQuery : IRequest<ApiResponse<WebsiteCategoryDTO>>
    {
        public int Id { get; set; }

        public class GetWebsiteCategoryQueryHandler : IRequestHandler<GetWebsiteCategoryQuery, ApiResponse<WebsiteCategoryDTO>>
        {
            private readonly IMapper _mapper;
            private readonly ILogger _logger;
            private readonly IWebsiteCategoryRepository _websiteCategoryRepository;
            public GetWebsiteCategoryQueryHandler(IMapper mapper, ILogger logger, IWebsiteCategoryRepository websiteCategoryRepository)
            {
                _mapper = mapper;
                _logger = logger;
                _websiteCategoryRepository = websiteCategoryRepository;
            }

            public async Task<ApiResponse<WebsiteCategoryDTO>> Handle(GetWebsiteCategoryQuery request, CancellationToken cancellationToken)
            {
                try
                {
                    var category = await _websiteCategoryRepository.GetAsync(request.Id);
                    if (category is null)
                        return new ApiResponse<WebsiteCategoryDTO>(Domain.Enums.HttpStatusCodeEnum.NotFound, null, ApiMessage.NotFound);

                    var mappedCategory = _mapper.Map<WebsiteCategoryDTO>(category);
                    return new ApiResponse<WebsiteCategoryDTO>(Domain.Enums.HttpStatusCodeEnum.Success, mappedCategory, ApiMessage.Successful);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex.Message);
                    return new ApiResponse<WebsiteCategoryDTO>(Domain.Enums.HttpStatusCodeEnum.BadRequest, null, ApiMessage.Error);
                }
            }
        }
    }
}
