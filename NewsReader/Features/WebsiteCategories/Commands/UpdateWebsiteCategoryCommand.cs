using AutoMapper;
using MediatR;
using NewsReader.Domain.Data;
using NewsReader.Domain.DTO.WebsiteCategoryDTO;
using NewsReader.Domain.DTO.WebsiteDTO;
using NewsReader.Domain.Helpers;
using NewsReader.Domain.Repository.IRepository;
using NewsReader.Features.Websites.Commands;

namespace NewsReader.Features.WebsiteCategories.Commands
{
    public class UpdateWebsiteCategoryCommand : IRequest<ApiResponse<WebsiteCategoryDTO>>
    {
        public int Id { get; set; }
        public string Category { get; set; }

        public class UpdateWebsiteCategoryCommandHandler : IRequestHandler<UpdateWebsiteCategoryCommand, ApiResponse<WebsiteCategoryDTO>>
        {
            private readonly IMapper _mapper;
            private readonly ILogger _logger;
            private readonly NewsReaderContext _context;
            private readonly IWebsiteCategoryRepository _websiteCategoryRepository;
            public UpdateWebsiteCategoryCommandHandler(NewsReaderContext context, ILogger logger, IMapper mapper, IWebsiteCategoryRepository websiteCategoryRepository)
            {
                _mapper = mapper;
                _logger = logger;
                _context = context;
                _websiteCategoryRepository = websiteCategoryRepository;
            }
            public async Task<ApiResponse<WebsiteCategoryDTO>> Handle(UpdateWebsiteCategoryCommand request, CancellationToken cancellationToken)
            {
                try
                {
                    var websiteCategory = await _websiteCategoryRepository.GetAsync(request.Id);
                    if (websiteCategory is null)
                        return new ApiResponse<WebsiteCategoryDTO>(Domain.Enums.HttpStatusCodeEnum.NotFound, null, ApiMessage.NotFound);

                    websiteCategory.Category = request.Category;

                    var updateCategory = _websiteCategoryRepository.Update(websiteCategory);
                    if (updateCategory is null)
                        return new ApiResponse<WebsiteCategoryDTO>(Domain.Enums.HttpStatusCodeEnum.BadRequest, null, ApiMessage.Error);

                    await _context.SaveChangesAsync();
                    var result = _mapper.Map<WebsiteCategoryDTO>(updateCategory);
                    return new ApiResponse<WebsiteCategoryDTO>(Domain.Enums.HttpStatusCodeEnum.Success, result, ApiMessage.WebsiteCategoryUpdateSuccessful);
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
