using Google.Api.Gax.Grpc.Gcp;
using NewsReader.Domain.Models;
using Telegram.Bot;

namespace NewsReader.Domain.Helpers
{
    public class SendMessage
    {
        private readonly IConfiguration _configuration;
        public SendMessage(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task SendTextMessage(string title, string link, string date, long chatId)
        {
            string apiToken = _configuration.GetValue<string>("BaleApiKey");
            var textMessage = $"*Title:* {title}\n*Publish Date:* {date}\n\n*Link:* {link}";
            ITelegramBotClient BotClient = new TelegramBotClient(apiToken);
            await BotClient.SendTextMessageAsync(chatId: chatId, text: textMessage);
        }

        public async Task SendTextMessage(string title, string link, string date, long[] chatIds)
        {
            string apiToken = _configuration.GetValue<string>("BaleApiKey");
            var textMessage = $"*Title:* {title}\n*Publish Date:* {date}\n\n*Link:* {link}";
            ITelegramBotClient BotClient = new TelegramBotClient(apiToken);
            foreach (var chatId in chatIds)
                await BotClient.SendTextMessageAsync(chatId: chatId, text: textMessage);
        }
    }
}
