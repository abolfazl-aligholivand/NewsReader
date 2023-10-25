using MediatR;
using NewsReader.Domain.DTO.WebsiteCategoryDTO;
using NewsReader.Domain.Helpers;
using NewsReader.Domain.Repository.IRepository;

namespace NewsReader.Features.WebsiteCategories.Queries
{
    public class GetAllWebsiteCategoriesQuery : IRequest<ApiResponse<IEnumerable<WebsiteCategoryDTO>>>
    {
        public class GetAllWebsiteCategoriesQueryHandler : IRequestHandler<GetAllWebsiteCategoriesQuery, ApiResponse<IEnumerable<WebsiteCategoryDTO>>>
        {
            private readonly IWebsiteCategoryRepository _websiteCategoryRepository;
            private readonly ILogger _logger;
            public GetAllWebsiteCategoriesQueryHandler(ILogger logger, IWebsiteCategoryRepository websiteCategoryRepository)
            {
                _logger = logger;
                _websiteCategoryRepository = websiteCategoryRepository;
            }

            public async Task<ApiResponse<IEnumerable<WebsiteCategoryDTO>>> Handle(GetAllWebsiteCategoriesQuery request, CancellationToken cancellationToken)
            {
                try
                {
                    var categories = await _websiteCategoryRepository.GetAllAsync();
                    var result = categories.Select(s => new WebsiteCategoryDTO
                    {
                        Id = s.Id,
                        Category = s.Category
                    });

                    return new ApiResponse<IEnumerable<WebsiteCategoryDTO>>(Domain.Enums.HttpStatusCodeEnum.Success, result, ApiMessage.Successful);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex.Message);
                    return new ApiResponse<IEnumerable<WebsiteCategoryDTO>>(Domain.Enums.HttpStatusCodeEnum.BadRequest, null, ApiMessage.Error);
                }
            }
        }
    }
}
