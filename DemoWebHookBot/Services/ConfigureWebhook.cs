using DemoWebHookBot.Models;
using Telegram.Bot;
using Telegram.Bot.Types.Enums;

namespace DemoWebHookBot.Services
{
    public class ConfigureWebhook : BackgroundService
    {
        private readonly ILogger<ConfigureWebhook> _logger;
        private readonly BotConfiguration _configuration;
        private readonly ITelegramBotClient _botClient;

        public ConfigureWebhook(
            ILogger<ConfigureWebhook> logger,
            IConfiguration configuration,
            ITelegramBotClient botClient)
        {
            _logger = logger;
            _configuration = configuration.GetSection("BotConfiguration").Get<BotConfiguration>();
            _botClient = botClient;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var webhookAddress = $"{_configuration.HostAddress}/bot/{_configuration.Token}";

            _logger.LogInformation("Setting webhook");

            await _botClient.SendTextMessageAsync(
                chatId: _configuration.MyChatId,
                text: "Webhook ishlashni boshladi");

            await _botClient.SetWebhookAsync(
                url: webhookAddress,
                allowedUpdates: Array.Empty<UpdateType>(),
                cancellationToken: stoppingToken);
        }
    }
}
