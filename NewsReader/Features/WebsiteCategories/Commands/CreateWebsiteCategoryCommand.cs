using AutoMapper;
using MediatR;
using NewsReader.Domain.Data;
using NewsReader.Domain.DTO.WebsiteCategoryDTO;
using NewsReader.Domain.Helpers;
using NewsReader.Domain.Models;
using NewsReader.Domain.Repository.IRepository;

namespace NewsReader.Features.WebsiteCategories.Commands
{
    public class CreateWebsiteCategoryCommand : IRequest<ApiResponse<WebsiteCategoryDTO>>
    {
        public string Category { get; set; }

        public class CreateWebsiteCategoryCommandHandler : IRequestHandler<CreateWebsiteCategoryCommand, ApiResponse<WebsiteCategoryDTO>>
        {
            private readonly IMapper _mapper;
            private readonly ILogger _logger;
            private readonly NewsReaderContext _context;
            private readonly IWebsiteCategoryRepository _websiteCategoryRepository;
            public CreateWebsiteCategoryCommandHandler(IMapper mapper, ILogger logger, NewsReaderContext context, IWebsiteCategoryRepository websiteCategoryRepository)
            {
                _mapper = mapper;
                _logger = logger;
                _context = context;
                _websiteCategoryRepository = websiteCategoryRepository;
            }

            public async Task<ApiResponse<WebsiteCategoryDTO>> Handle(CreateWebsiteCategoryCommand request, CancellationToken cancellationToken)
            {
                try
                {
                    var oldCategory = await _websiteCategoryRepository.GetAsync(request.Category);
                    if (oldCategory is not null)
                        return new ApiResponse<WebsiteCategoryDTO>(Domain.Enums.HttpStatusCodeEnum.BadRequest, null, ApiMessage.WebsiteCategoryDuplicateError);

                    var createdCategory = await _websiteCategoryRepository.CreateAsynce(new WebsiteCategory
                    {
                        Category = request.Category
                    });

                    if (createdCategory is null)
                        return new ApiResponse<WebsiteCategoryDTO>(Domain.Enums.HttpStatusCodeEnum.BadRequest, null, ApiMessage.Error);

                    await _context.SaveChangesAsync();
                    var result = _mapper.Map<WebsiteCategoryDTO>(createdCategory);
                    return new ApiResponse<WebsiteCategoryDTO>(Domain.Enums.HttpStatusCodeEnum.Success, result, ApiMessage.WebsiteCategoryCreateSuccessful);
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
