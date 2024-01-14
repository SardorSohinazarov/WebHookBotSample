using Telegram.Bot;
using Telegram.Bot.Types;

namespace DemoWebHookBot.Services
{
    public class HandleUpdateService
    {
        private readonly ILogger<HandleUpdateService> _logger;
        private readonly ITelegramBotClient _botClient;

        public HandleUpdateService(
            ILogger<HandleUpdateService> logger,
            ITelegramBotClient botClient)
        {
            _logger = logger;
            _botClient = botClient;
        }

        public async Task HandleUpdateAsync(Update update)
        {
            _logger.LogInformation("Botga message keldi");

            if (update.Message is not null)
            {
                await _botClient.SendTextMessageAsync(
                    chatId: update.Message.Chat.Id,
                    text: "Qales");
            }
        }
    }
}
