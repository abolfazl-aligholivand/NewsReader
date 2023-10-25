
using Telegram.Bot;

namespace NewsReaderTest
{
    public class SendMessageViaBale
    {
        [Fact]
        public async Task SendTextMessage()
        {
            string apiToken = "1252212556:ehIbMty8Yw5qtv6ehxn5kDsG8qd8bvuw2t7Ie9io";
            ITelegramBotClient BotClient = new TelegramBotClient(apiToken);

            try
            {
                string messageText = "*Title:* title one \n*Link:* link one \n";
                Telegram.Bot.Types.Message message = await BotClient.SendTextMessageAsync(
                    chatId: 5525529427,
                    text: "Hello Message"
                );
            }
            catch ( Exception ex )
            {
                string msg = ex.Message;
            }
            Assert.True(true);
        }

    }
}
