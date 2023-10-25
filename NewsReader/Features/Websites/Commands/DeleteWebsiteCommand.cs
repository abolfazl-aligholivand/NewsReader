using MediatR;
using NewsReader.Domain.Data;
using NewsReader.Domain.Helpers;
using NewsReader.Domain.Repository.IRepository;

namespace NewsReader.Features.Websites.Commands
{
    public class DeleteWebsiteCommand : IRequest<ApiResponse<bool>>
    {
        public int id { get; set; }

        public class DeleteWebsiteCommandHandler : IRequestHandler<DeleteWebsiteCommand, ApiResponse<bool>>
        {
            private readonly NewsReaderContext _context;
            private readonly ILogger _logger;
            private readonly IWebsiteRepository _websiteRepository;
            public DeleteWebsiteCommandHandler(NewsReaderContext context, ILogger logger, IWebsiteRepository websiteRepository)
            {
                _context = context;
                _logger = logger;
                _websiteRepository = websiteRepository;
            }

            public async Task<ApiResponse<bool>> Handle(DeleteWebsiteCommand request, CancellationToken cancellationToken)
            {
                try
                {
                    var delete = await _websiteRepository.DeleteAsynce(request.id);
                    if (!delete)
                        return new ApiResponse<bool>(Domain.Enums.HttpStatusCodeEnum.BadRequest, delete, ApiMessage.Error);

                    await _context.SaveChangesAsync();
                    return new ApiResponse<bool>(Domain.Enums.HttpStatusCodeEnum.Success, delete, ApiMessage.WebsiteDeleteSuccessful);
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
