using MediatR;
using NewsReader.Domain.Data;
using NewsReader.Domain.Helpers;
using NewsReader.Domain.Repository.IRepository;

namespace NewsReader.Features.WebsiteCategories.Commands
{
    public class DeleteWebsiteCategoryCommand : IRequest<ApiResponse<bool>>
    {
        public int Id { get; set; }

        public class DeleteWebsiteCategoryCommandHandler : IRequestHandler<DeleteWebsiteCategoryCommand, ApiResponse<bool>>
        {
            private readonly ILogger _logger;
            private readonly NewsReaderContext _context;
            private readonly IWebsiteCategoryRepository _websiteCategoryRepository;
            public DeleteWebsiteCategoryCommandHandler(ILogger logger, NewsReaderContext context, IWebsiteCategoryRepository websiteCategoryRepository)
            {
                _logger = logger;
                _context = context;
                _websiteCategoryRepository = websiteCategoryRepository;
            }

            public async Task<ApiResponse<bool>> Handle(DeleteWebsiteCategoryCommand request, CancellationToken cancellationToken)
            {
                try
                {
                    var delete = await _websiteCategoryRepository.DeleteAsynce(request.Id);
                    if (!delete)
                        return new ApiResponse<bool>(Domain.Enums.HttpStatusCodeEnum.BadRequest, delete, ApiMessage.Error);

                    await _context.SaveChangesAsync();
                    return new ApiResponse<bool>(Domain.Enums.HttpStatusCodeEnum.Success, delete, ApiMessage.WebsiteCategoryDeleteSuccessful);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex.Message);
                    return new ApiResponse<bool>(Domain.Enums.HttpStatusCodeEnum.BadRequest, false, ApiMessage.Error);
                }
            }
        }
    }
}
