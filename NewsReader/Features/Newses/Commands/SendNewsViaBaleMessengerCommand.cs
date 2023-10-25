using MediatR;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using NewsReader.Domain.Helpers;
using NewsReader.Domain.Repository.IRepository;
using Telegram.Bot;

namespace NewsReader.Features.Newses.Commands
{
    public class SendNewsViaBaleMessengerCommand : IRequest<ApiResponse<bool>>
    {
        public Guid newsId { get; set; }
        public long chatId { get; set; }

        public class SendNewsViaBaleMessengerCommandHandler : IRequestHandler<SendNewsViaBaleMessengerCommand, ApiResponse<bool>>
        {
            private readonly INewsRepository _newsRepository;
            private readonly SendMessage _sendMessage;
            private readonly ILogger _logger;
            public SendNewsViaBaleMessengerCommandHandler(ILogger logger, INewsRepository newsRepository, SendMessage sendMessage)
            {
                _newsRepository = newsRepository;
                _sendMessage = sendMessage;
                _logger = logger;
            }

            public async Task<Domain.Helpers.ApiResponse<bool>> Handle(SendNewsViaBaleMessengerCommand request, CancellationToken cancellationToken)
            {
                try
                {
                    var news = await _newsRepository.GetAsync(request.newsId);
                    if (news is null)
                        return new ApiResponse<bool>(Domain.Enums.HttpStatusCodeEnum.NotFound, false, ApiMessage.NotFound);

                    await _sendMessage.SendTextMessage(news.Title, news.Link, news.Date.ToString("yyyy/MM/dd HH:mm"), request.chatId);
                    return new ApiResponse<bool>(Domain.Enums.HttpStatusCodeEnum.Success, true, ApiMessage.SendMessageSuccessful);
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
