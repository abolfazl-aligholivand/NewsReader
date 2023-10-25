using MediatR;
using Microsoft.IdentityModel.Tokens;
using NewsReader.Domain.GoogleTranslate;
using NewsReader.Domain.Helpers;
using NewsReader.Domain.Repository.IRepository;

namespace NewsReader.Features.Newses.Commands
{
    public class TranslateNewsCommand : IRequest<ApiResponse<string>>
    {
        public Guid newsId { get; set; }

        public class TranslateNewsCommandHandler : IRequestHandler<TranslateNewsCommand, ApiResponse<string>>
        {
            private readonly INewsRepository _newsRepository;
            private readonly ITranslateProvider _provider;
            private readonly ILogger _logger;
            public TranslateNewsCommandHandler(ILogger logger, INewsRepository newsRepository, ITranslateProvider provider)
            {
                _newsRepository = newsRepository;
                _provider = provider;
                _logger = logger;
            }
            public async Task<ApiResponse<string>> Handle(TranslateNewsCommand request, CancellationToken cancellationToken)
            {
                try
                {
                    var news = await _newsRepository.GetAsync(request.newsId);
                    if (news is null)
                        return new ApiResponse<string>(Domain.Enums.HttpStatusCodeEnum.NotFound, string.Empty, ApiMessage.NotFound);

                    var translatedMessage = await _provider.ExecuteAsync(news.Description, "fa", cancellationToken);
                    if (string.IsNullOrEmpty(translatedMessage))
                        return new ApiResponse<string>(Domain.Enums.HttpStatusCodeEnum.BadRequest, string.Empty, ApiMessage.Error);

                    //https://www.craftedpod.com/tech/google-cloud-translate-v3-with-csharp/ 
                    return new ApiResponse<string>(Domain.Enums.HttpStatusCodeEnum.Success, translatedMessage, ApiMessage.Successful);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex.Message);
                    return new ApiResponse<string>(Domain.Enums.HttpStatusCodeEnum.BadRequest, string.Empty, ApiMessage.Error);
                }
            }
        }
    }
}
