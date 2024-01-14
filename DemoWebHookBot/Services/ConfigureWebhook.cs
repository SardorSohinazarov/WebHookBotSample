using DemoWebHookBot.Models;
using Telegram.Bot;
using Telegram.Bot.Types.Enums;

namespace DemoWebHookBot.Services
{
    public class ConfigureWebhook : BackgroundService
    {
        private readonly ILogger<ConfigureWebhook> _logger;
        private readonly IServiceProvider _serviceProvider;
        private readonly BotConfiguration _configuration;

        public ConfigureWebhook(
            ILogger<ConfigureWebhook> logger,
            IServiceProvider serviceProvider,
            IConfiguration configuration)
        {
            _logger = logger;
            _serviceProvider = serviceProvider;
            _configuration = configuration.GetSection("BotConfiguration").Get<BotConfiguration>();
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            using var scope = _serviceProvider.CreateScope();
            var botClient = scope.ServiceProvider.GetRequiredService<ITelegramBotClient>();
            var webhookAddress = $"{_configuration.HostAddress}/bot/{_configuration.Token}";

            _logger.LogInformation("Setting webhook");

            await botClient.SendTextMessageAsync(
                chatId: _configuration.MyChatId,
                text: "Webhook ishlashni boshladi");

            await botClient.SetWebhookAsync(
                url: webhookAddress,
                allowedUpdates: Array.Empty<UpdateType>(),
                cancellationToken: stoppingToken);
        }
    }
}
